using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype.Speech
{
    public enum EVerbs
    {
        close = 3,
        goTo = 7,
        open = 11,
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
        undefined = -1,
    }

    struct ResourceStrings
    {
        public static String[] verbs = new String[12];
        public static String[] particle = new String[3]; 
        public static String[] nouns = new String[8];       

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

        //particle
        public const String he = "へ";
        public const String wo = "を";
        public const String mo = "も";

        //nouns
        public const String back = "バック";
        public const String grammarExercise = "文法の練習";
        public const String grammarExplanation = "文法の説明";
        public const String menu = "メニュー";
        public const String options = "オプション";
        public const String program = "プログラム";
        public const String thisWindow = "この窓";
        public const String wordsExercise = "ことばの練習";

        public const String cultureIdentifier = "ja-JP";
        
#else
        
        //verbs
        public const String closeMasu = "schliessen";
        public const String closeRu = "schliessen";
        public const String closeTe = "beenden";
        public const String closeTekudasai = "schliessenbitte";
        public const String goRu = "gehen";
        public const String goMasu = "gehen";
        public const String goTe = "gehen";
        public const String goTekudasai = "gehen";
        public const String openMasu = "öffnen";
        public const String openRu = "anzeigen";
        public const String openTe = "starten";
        public const String openTekudasai = "öffnenbitte";
        
        //particle
        public const String he = "";
        public const String wo = "";
        public const String mo = "";
        
        //nouns
        public const String back = "zurück";
        public const String grammarExercise = "Grammatikübungen";
        public const String grammarExplanation = "Grammatikerklärungen";
        public const String menu = "Menü";
        public const String options = "Optionen";
        public const String program = "Programm";
        public const String thisWindow = "diesesfenster";
        public const String wordsExercise = "Vokabelübungen";

        public const String cultureIdentifier = "de-DE";

#endif

        public static void Initialize()
        {
            verbs[0] = closeMasu;
            verbs[1] = closeRu;
            verbs[2] = closeTe;
            verbs[3] = closeTekudasai;
            verbs[4] = goMasu;
            verbs[5] = goRu;
            verbs[6] = goTe;
            verbs[7] = goTekudasai;
            verbs[8] = openMasu;
            verbs[9] = openRu;
            verbs[10] = openTe;
            verbs[11] = openTekudasai;

            particle[0] = he;
            particle[1] = wo;
            particle[2] = mo;

            nouns[0] = back;
            nouns[1] = grammarExercise;
            nouns[2] = grammarExplanation;
            nouns[3] = menu;
            nouns[4] = options;
            nouns[5] = program;
            nouns[6] = thisWindow;
            nouns[7] = wordsExercise;
        }
    }
}
