using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    class Word
    {
        public int ID { get; set; }
        public int SetID { get; set; }

        public int JWord { get; set; }
        public int Translation { get; set; }
        public bool IsImagePath { get; set; }        
    }
}
