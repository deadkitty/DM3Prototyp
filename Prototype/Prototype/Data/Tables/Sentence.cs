using System;
using System.Windows.Documents;
using System.Data.Linq.Mapping;
using System.Text;

namespace Prototype.DataModel.Tables
{
    [Table]
    public class Sentence
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int id;

        [Column(CanBeNull = false)]
        public String text;

        [Column(CanBeNull = false)]
        public String inserts;

        [Column(CanBeNull = false)]
        public String translation;

        [Column(CanBeNull = false)]
        public int setID;

        public int insertPosition;

        public String[] insertParts;

        public Sentence()
        {

        }

        public Sentence(String text, int setID)
        {
            String[] sentenceLineFragments = text.Split('|');

            this.text = sentenceLineFragments[0];
            this.inserts = sentenceLineFragments[1];
            this.translation = sentenceLineFragments[2];

            this.setID = setID;
        }

        public String CreateInsertString()
        {
            String[] parts = text.Split('　');
            int position = 0;

            StringBuilder sb = new StringBuilder();

            foreach (String s in parts)
            {
                if (s == "＿")
                {
                    if (position == insertPosition)
                    {
                        sb.Append(" ... ");
                    }
                    else
                    {
                        sb.Append(insertParts[position]);
                    }
                    position++;
                }
                else
                {
                    sb.Append(s);
                }
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return text + "|" + inserts + "|" + translation;
        }
    }
}

