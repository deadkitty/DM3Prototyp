using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    class DataModel
    {
        #region Fields

        Learnset[] wordSets;
        Learnset[] sentenceSets;

        Word[] words;
        Sentence[] sentences;

        DBContext db;

        #endregion

        #region Properties

        public Learnset[] WordSets
        {
            get { return wordSets; }
            set { wordSets = value; }
        }

        public Learnset[] SentenceSets
        {
            get { return sentenceSets; }
            set { sentenceSets = value; }
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

        public DBContext Db
        {
            get { return db; }
            set { db = value; }
        }

        #endregion

        #region Singleton

        private static DataModel sInstance;

        public static DataModel GetInstance()
        {
            if (sInstance == null)
            {
                sInstance = new DataModel();
            }
            return sInstance;
        }

        #endregion

        #region Constructor

        private DataModel()
         {
             using (db = new DBContext(DBContext.connectionString))
             {
                 if (!db.DatabaseExists())
                 {
                     db.CreateDatabase();
                 }
             }
        }

        #endregion

        #region public Methods

        public Learnset[] GetLearnsets(ESetType setType)
        {
            IQueryable<Learnset> setsQuery;
            using (db = new DBContext(DBContext.connectionString))
            {
                setsQuery = from c in db.Learnsets where c.type == (int)setType select c;    
            }            
            return setsQuery.ToArray();
        }

        public Word[] GetWords(int[] setIDs)
        {
            IQueryable<Word> setsQuery;
            using (db = new DBContext(DBContext.connectionString))
            {
                setsQuery = from c in db.Words where setIDs.Contains(c.SetID) select c;
            }
            return setsQuery.ToArray();
        }

        public Sentence[] GetSentences(int[] setIDs)
        {
            IQueryable<Sentence> setsQuery;
            using (db = new DBContext(DBContext.connectionString))
            {
                setsQuery = from c in db.Sentences where setIDs.Contains(c.SetID) select c;
            }
            return setsQuery.ToArray();
        }

        #endregion
    }
}
