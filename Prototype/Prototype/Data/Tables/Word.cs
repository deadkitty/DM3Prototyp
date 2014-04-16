using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Prototype.DataModel.Tables
{
    [Table]
    public class Word
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID;

        [Column(CanBeNull = false)]
        public int SetID;

        [Column(CanBeNull = false)]
        public String JWord;

        [Column(CanBeNull = false)]
        public String Translation;

        [Column(CanBeNull = false)]
        public bool IsImagePath;

        public Word()
        {

        }

        //public Word(String text, int setID)
        //{
        //    String[] wordLineFragments = text.Split('|');

        //    JWord = wordLineFragments[0];
        //    Translation = wordLineFragments[1];

        //    IsImagePath = false;

        //    this.SetID = setID;
        //}

        public override string ToString()
        {
            return JWord;
        }
    }
}
