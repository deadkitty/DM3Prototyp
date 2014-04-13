using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Model
{
    [Table]
    public class Sentence
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID;

        [Column(CanBeNull = false)]
        public int SetID;

        [Column(CanBeNull = false)]
        public String Text;

        [Column(CanBeNull = false)]
        public String InsertPositions;
    }
}
