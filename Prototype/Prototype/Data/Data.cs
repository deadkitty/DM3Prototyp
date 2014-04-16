using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prototype.DataModel.Tables;

namespace Prototype.DataModel
{
    public class Data
    {        
        #region Fields

        Lesson[] wordLessons;
        Lesson[] sentenceLessons;

        Word[] words;
        Sentence[] sentences;

        Word activeWord;
        Sentence activeSentence;

        Database db;

        String currentComand;

        #endregion

        #region Properties

        public Lesson[] WordSets
        {
            get { return wordLessons; }
            set { wordLessons = value; }
        }

        public Lesson[] SentenceSets
        {
            get { return sentenceLessons; }
            set { sentenceLessons = value; }
        }

        public Word[] Words
        {
            get { return words; }
            set { words = value; }
        }

        public Sentence[] Sentences
        {
            get { return sentences; }
            set { sentences = value; }
        }

        public Word ActiveWord
        {
            get { return activeWord; }
            set { activeWord = value; }
        }

        public Sentence ActiveSentence
        {
            get { return activeSentence; }
            set { activeSentence = value; }
        }

        public Database DB
        {
            get { return db; }
            set { db = value; }
        }

        public String CurrentComand
        {
            get { return currentComand; }
            set { currentComand = value; }
        }

        #endregion

        #region Singleton

        private static Data sInstance;

        public static Data GetInstance()
        {
            if (sInstance == null)
            {
                sInstance = new Data();
            }
            return sInstance;
        }

        #endregion

        #region Constructor

        private Data()
        {

        }

        #endregion

        #region public Methods

        public Lesson[] GetLessons(Lesson.EType type)
        {
            IQueryable<Lesson> setsQuery = from c in db.Lessons where c.type == (int)type select c;
            return setsQuery.ToArray();
        }

        public Word[] GetWords(int[] setIDs)
        {
            IQueryable<Word> setsQuery = from c in db.Words where setIDs.Contains(c.SetID) select c;
            return setsQuery.ToArray();
        }

        public Sentence[] GetSentences(int[] setIDs)
        {
            IQueryable<Sentence> setsQuery = from c in db.Sentences where setIDs.Contains(c.SetID) select c;
            return setsQuery.ToArray();
        }

        #endregion
    }
}
