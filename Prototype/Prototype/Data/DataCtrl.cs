using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prototype.DataModel.Tables;
using System.IO;

namespace Prototype.DataModel
{
    public class DataCtrl
    {
        #region Delegates

        public delegate void RandomizeLessonDel();
        public delegate void LoadNextDel();
        public delegate void SkipThisDel();
        public delegate bool CheckCurrrentDel(String text);

        #endregion

        #region Fields

        Data data;

        int indexOfCurrent;
        int skipIndex;

        public RandomizeLessonDel RandomizeLesson;
        public LoadNextDel LoadNext;
        public SkipThisDel SkipThis;
        public CheckCurrrentDel CheckCurrent;

        #endregion

        #region Properties
        
        public Data Data
        {
            get { return data; }
            set { data = value; }
        }

        #endregion

        #region Singleton

        private static DataCtrl sInstance;

        public static DataCtrl GetInstance()
        {
            if (sInstance == null)
            {
                sInstance = new DataCtrl();
            }
            return sInstance;
        }

        #endregion

        #region Constructor

        private DataCtrl()
        {
            data = Data.GetInstance();
        }

        #endregion

        #region Methods

        #region Initialize/Deinitialize

        /// <summary>
        /// Creates database and Loads data in it
        /// Should only be used to create a database with content, not for the actual app
        /// </summary>
        public void InitializeDatabase()
        {
            using (data.DB = new Database(Database.connectionString))
            {
                if (!data.DB.DatabaseExists())
                {
                    data.DB.CreateDatabase();

                    using (StreamReader sr = new StreamReader("Resources\\content.txt"))
                    {
                        #region Insert Lessons

                        String line = sr.ReadLine();
                        int setsCount = Convert.ToInt32(line);

                        Lesson[] lessons = new Lesson[setsCount];
                        for (int i = 0; i < setsCount; ++i)
                        {
                            lessons[i] = new Lesson(sr.ReadLine());
                        }
                        //insert Lessons and Save Changes
                        data.DB.Lessons.InsertAllOnSubmit(lessons);
                        data.DB.SubmitChanges();

                        //get Lessons with Correct Id´s
                        IQueryable<Lesson> Lessons = from c in data.DB.Lessons select c;
                        lessons = Lessons.ToArray();

                        #endregion

                        #region Insert Words, Sentences

                        Word[] words = null;
                        Sentence[] sentences = null;

                        foreach (Lesson l in lessons)
                        {
                            switch (l.type)
                            {
                                case 0: words = new Word[l.itemCount]; break;
                                case 1: sentences = new Sentence[l.itemCount]; break;
                            }

                            for (int i = 0; i < l.itemCount; ++i)
                            {
                                switch (l.type)
                                {
                                    case 0: words[i] = new Word(sr.ReadLine(), l.ID); break;
                                    case 1: sentences[i] = new Sentence(sr.ReadLine(), l.ID); break;
                                }
                            }

                            switch (l.type)
                            {
                                case 0: data.DB.Words.InsertAllOnSubmit(words); break;
                                case 1: data.DB.Sentences.InsertAllOnSubmit(sentences); break;
                            }
                        }

                        #endregion

                        data.DB.SubmitChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Loads lessons on startup 
        /// </summary>
        public void Initialize()
        {
            using (data.DB = new Database(Database.connectionString))
            {
                data.WordSets = data.GetLessons(Lesson.EType.wordsPractice);
                data.SentenceSets = data.GetLessons(Lesson.EType.grammarPractice);
            }
        }

        public void Deinitialize()
        {
            data.Words = null;
            data.Sentences = null;

            data.ItemsWrongList.Clear();
        }

        /// <summary>
        /// loads all words/Sentences in data by the given lessons
        /// </summary>
        public void Load(Lesson[] lessons)
        {
            //unload previous content
            Deinitialize();

            //extract id´s from lessons
            int[] setIDs = new int[lessons.Length];
            for (int i = 0; i < setIDs.Length; ++i)
            {
                setIDs[i] = lessons[i].ID;
            }

            //connect to database and load words/sentences
            using (data.DB = new Database(Database.connectionString))
            {
                if (lessons[0].type == (int)Lesson.EType.grammarPractice)
                {
                    data.Sentences = data.GetSentences(setIDs);

                    data.ItemsLeft = data.Sentences.Length;

                    //set delegates
                    RandomizeLesson = RandomizeSentences;
                    LoadNext = LoadNextSentence;
                    SkipThis = SkipSentence;
                    CheckCurrent = CheckSentence;
                }
                else
                {
                    data.Words = data.GetWords(setIDs);

                    data.ItemsLeft = data.Words.Length;
        
                    RandomizeLesson = RandomizeWords;
                    LoadNext = LoadNextWord;
                    SkipThis = SkipWord;
                    CheckCurrent = CheckWord;
                }
            }

            //set some over stuff
            data.ItemsCorrect = 0;
            data.ItemsWrong = 0;

            indexOfCurrent = 0;
            skipIndex = 0;

            //randomize loaded items and set first item
            RandomizeLesson();
            LoadNext();
        }
        
        #endregion

        #region Randomize Lessons

        private void RandomizeWords()
        {
            object[] items = null;
            items = data.Words;
            Randomize(items);
        }

        private void RandomizeSentences()
        {
            object[] items = null;
            items = data.Sentences;
            Randomize(items);
        }

        private void Randomize(object[] items)
        {
            Random r = new Random();
            for (int i = 0; i < items.Length; ++i)
            {
                int randomIndex = r.Next(items.Length);
                object hv = items[i];
                items[i] = items[randomIndex];
                items[randomIndex] = hv;
            }
        }

        #endregion

        #region Load Next Word

        private void LoadNextWord()
        {
            data.ActiveWord = data.Words[indexOfCurrent++ % data.Words.Length];
        }

        private void LoadNextSentence()
        {
            data.ActiveSentence = data.Sentences[indexOfCurrent++ % data.Sentences.Length];
        }

        #endregion

        #region Skip Words

        public void SkipWord()
        {
            skipIndex++;

            if (indexOfCurrent == skipIndex)
            {
                skipIndex = 1;
            }

            Word hv = data.Words[indexOfCurrent];
            data.Words[indexOfCurrent] = data.Words[data.Words.Length - skipIndex];
            data.Words[data.Words.Length - skipIndex] = hv;
        }

        public void SkipSentence()
        {
            skipIndex++;

            if (indexOfCurrent == skipIndex)
            {
                skipIndex = 1;
            }

            Sentence hv = data.Sentences[indexOfCurrent];
            data.Sentences[indexOfCurrent] = data.Sentences[data.Sentences.Length - skipIndex];
            data.Sentences[data.Sentences.Length - skipIndex] = hv;
        }
        
        #endregion

        #region Check Against Userinput

        private bool CheckWord(String text)
        {
            return text == data.ActiveWord.JWord;
        }

        private bool CheckSentence(String text)
        {
            return text == data.ActiveSentence.Text.Replace("　","");
        }

        #endregion

        #endregion
    }
}
