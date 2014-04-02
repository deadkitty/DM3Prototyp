using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    class Sentence
    {
        public int ID { get; set; }
        public int SetID { get; set; }

        public String Text { get; set; }
        public int[] InsertPositions { get; set; }
    }
}
