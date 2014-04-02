using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    enum ESetType
    {
        wordsPractice,
        insertPractice,
    }

    class Learnset
    {
        public int ID { get; set; }

        public String Name { get; set; }
        public ESetType type { get; set; }
    }
}
