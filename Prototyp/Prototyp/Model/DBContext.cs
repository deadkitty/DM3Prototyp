using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Model
{
    class DBContext : DataContext
    {
        #region Fields

        public static String connectionString = "Data Source = 'Resources/db.sdf';";

        public Table<Learnset> Learnsets;
        public Table<Word> Words;
        public Table<Sentence> Sentences;
        
        #endregion

        #region Constructor

        public DBContext(String connectionString)
            : base(connectionString)
        {

        }
        
        #endregion
    }
}
