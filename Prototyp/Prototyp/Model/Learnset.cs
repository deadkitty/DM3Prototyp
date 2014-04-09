using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Model
{
    enum ESetType
    {
        wordsPractice = 0,
        insertPractice = 1,
    }

    [Table]
    class Learnset
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID;

        [Column(CanBeNull = false)]
        public String Name;

        [Column(CanBeNull = false)]
        public int type;
    }
}
