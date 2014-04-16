using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Prototype.DataModel.Tables
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

        public int itemCount;

        public Lesson()
        {

        }

        public Lesson(String text)
        {
            String[] textFragments = text.Split('|');

            Name = textFragments[0];
            type = Convert.ToInt32(textFragments[1]);
            itemCount = Convert.ToInt32(textFragments[2]);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
