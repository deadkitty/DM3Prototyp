using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prototype.Data.Tables;

namespace Prototype.Data
{
    class Data
    {        
        #region Fields

        Lesson[] wordLessons;
        Lesson[] sentenceLessons;

        Word[] words;
        Sentence[] sentences;

        Database db;

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

        public Database DB
        {
            get { return db; }
            set { db = value; }
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
            using (db = new Database(Database.connectionString))
            {
                if (!db.DatabaseExists())
                {
                    db.CreateDatabase();
                }

                wordLessons = GetLessons(Lesson.EType.wordsPractice);
                sentenceLessons = GetLessons(Lesson.EType.grammarPractice);
            }
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
