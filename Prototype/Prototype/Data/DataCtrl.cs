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
        #region Fields

        Data data;
        
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

        #region Public Methods

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

        /// <summary>
        /// loads all words/Sentences in data by the given lessons
        /// </summary>
        /// <param name="lessons"></param>
        public void Load(Lesson[] lessons)
        {
            int[] setIDs = new int[lessons.Length];

            for (int i = 0; i < setIDs.Length; ++i)
            {
                setIDs[i] = lessons[i].ID;
            }

            using (data.DB = new Database(Database.connectionString))
            {
                if (lessons[0].type == (int)Lesson.EType.grammarPractice)
                {
                    data.Sentences = data.GetSentences(setIDs);
                }
                else
                {
                    data.Words = data.GetWords(setIDs);
                }
            }
        }


        /// <summary>
        /// loads next active word in data
        /// </summary>
        public void LoadNextWord()
        {

        }

        /// <summary>
        /// skips active word and asks again for the word at the end of the lesson
        /// </summary>
        public void SkipWord()
        {

        }

        #endregion
    }
}
