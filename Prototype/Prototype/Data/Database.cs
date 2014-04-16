using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Prototype.DataModel
{
    using Tables;

    public class Database : DataContext
    {
        #region Fields

        public static String connectionString = "Data Source = 'Resources/db.sdf';";

        public Table<Lesson> Lessons;
        public Table<Word> Words;
        public Table<Sentence> Sentences;

        #endregion

        #region Constructor

        public Database(String connectionString)
            : base(connectionString)
        {

        }

        #endregion
    }
}
