using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype.Speech
{
    public enum EVerbs
    {
        close = 0,
        goTo = 1,
        open = 2,
        select = 3,
        undefined = -1,
    }

    public enum ENouns
    {
        back = 0,
        grammarExercise = 1,
        grammarExplanation = 2,
        menu = 3,
        options = 4,
        program = 5,
        thisWindow = 6,
        wordsExercise = 7,
        lesson = 8,
        lessonFromTo = 9,
        all = 10,
        nothing = 11,
        undefined = -1,
    }

    struct ResourceStrings
    {
        public const String actionKey = "action";
        public const String targetKey = "target";
        public const String targetLessonKey = "targetLesson";

#if japaneseVersion
        
        //verbs
        public const String closeMasu = "閉めます";
        public const String closeRu = "閉める";
        public const String closeTe = "閉めて";
        public const String closeTekudasai = "閉めてください";
        public const String goRu = "行く";
        public const String goMasu = "行きます";
        public const String goTe = "行って";
        public const String goTekudasai = "行ってください";
        public const String openMasu = "開けます";
        public const String openRu = "開ける";
        public const String openTe = "開けて";
        public const String openTekudasai = "開けてください";
        public const String selectRu = "選ぶ";
        public const String selectMasu = "選びます";
        public const String selectTe = "選んで";
        public const String selectTekudasai = "選んでください";

        //particle
        public const String he = "へ";
        public const String wo = "を";
        public const String mo = "も";
        public const String to = "と";
        public const String kara = "から";
        public const String made = "まで";

        //nouns
        public const String back = "バック";
        public const String grammarExercise = "文法の練習";
        public const String grammarExplanation = "文法の説明";
        public const String menu = "メニュー";
        public const String options = "オプション";
        public const String program = "プログラム";
        public const String thisWindow = "この窓";
        public const String wordsExercise = "ことばの練習";
        public const String lesson1 = "第";
        public const String lesson2 = "課";
        public const String all = "全部";
        public const String nothing = "何も";

        public const String cultureIdentifier = "ja-JP";
        
#else
        
        //verbs
        public const String closeMasu = "schliessen";
        public const String closeRu = "zumachen";
        public const String closeTe = "beenden";
        public const String closeTekudasai = "schliessen bitte";
        public const String goRu = "gehen";
        public const String goMasu = "gehen";
        public const String goTe = "gehen";
        public const String goTekudasai = "gehen bitte";
        public const String openMasu = "öffnen";
        public const String openRu = "anzeigen";
        public const String openTe = "starten";
        public const String openTekudasai = "öffnen bitte";
        public const String selectRu = "auswählen";
        public const String selectMasu = "selektieren";
        public const String selectTe = "markieren";
        public const String selectTekudasai = "auswählen bitte";
        
        //particle
        public const String he = "";
        public const String wo = "";
        public const String mo = "auch";
        
        //nouns
        public const String back = "zurück";
        public const String grammarExercise = "Grammatik Übungen";
        public const String grammarExplanation = "Grammatik Erklärungen";
        public const String menu = "Menü";
        public const String options = "Optionen";
        public const String program = "Programm";
        public const String thisWindow = "dieses fenster";
        public const String wordsExercise = "Vokabelübungen";
        public const String lesson1 = "Lektion";
        public const String lesson2 = "";
        public const String all = "alles";

        public const String cultureIdentifier = "de-DE";

#endif
    }
}
