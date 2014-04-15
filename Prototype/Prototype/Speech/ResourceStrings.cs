using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype.Speech
{
    class ResourceStrings
    {
#if japaneseVersion

        public const String cultureIdentifier = "ja-JP";
        
        public const String back = "バック";
        public const String closeMasu = "閉めます";
        public const String closeRu = "閉める";
        public const String closeTe = "閉めて";
        public const String closeTekudasai = "閉めてください";
        public const String goRu = "行く";
        public const String goMasu = "行きます";
        public const String grammarExercise = "文法の練習";
        public const String grammarExplanation = "文法の説明";
        public const String he = "へ";
        public const String menu = "メニュー";
        public const String openMasu = "開けます";
        public const String openRu = "開ける";
        public const String openTe = "開けて";
        public const String openTekudasai = "開けてください";
        public const String options = "オプション";
        public const String program = "プログラム";
        public const String thisWindow = "この窓";
        public const String wo = "を";
        public const String wordsExercise = "ことばの練習";

#else

        public const String cultureIdentifier = "de-DE";

        public const String back = "zurück";
        public const String closeMasu = "schliessen";
        public const String closeRu = "schliessen";
        public const String closeTe = "beenden";
        public const String closeTekudasai = "schliessen bitte";
        public const String goRu = "gehen";
        public const String goMasu = "gehen";
        public const String grammarExercise = "Grammatikübungen";
        public const String grammarExplanation = "Grammatikerklärungen";
        public const String he = "";
        public const String menu = "Menü";
        public const String openMasu = "öffnen";
        public const String openRu = "anzeigen";
        public const String openTe = "starten";
        public const String openTekudasai = "öffnen bitte";
        public const String options = "Optionen";
        public const String program = "Programm";
        public const String thisWindow = "dieses Fenster";
        public const String wo = "";
        public const String wordsExercise = "Vokabel Übungen";

#endif
    }
}
