using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Prototype.Data.Tables
{
    [Table]
    public class Lesson
    {
        public enum EType
        {
            wordsPractice = 0,
            grammarPractice = 1,
        }
        
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID;

        [Column(CanBeNull = false)]
        public String Name;

        [Column(CanBeNull = false)]
        public int type;

        public Lesson()
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
