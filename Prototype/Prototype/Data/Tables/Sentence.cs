using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Prototype.DataModel.Tables
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

        [Column(CanBeNull = false)]
        public String Translation;

        public Sentence()
        {

        }

        public Sentence(String text, int setID)
        {
            String[] sentenceLineFragments = text.Split('|');

            this.Text = sentenceLineFragments[0];
            this.InsertPositions = sentenceLineFragments[1];
            this.Translation = sentenceLineFragments[2];

            this.SetID = setID;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
