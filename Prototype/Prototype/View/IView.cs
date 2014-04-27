using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype.View
{
    public enum EContentType
    {
        firstStartContent,
        mainMenuContent,
        chooseWordSetsContent, // Auswahl Vokabelübungen
        chooseSentenceSetsContent, // Auswahl Text Einsetzungsübungen
        wordsPracticeContent, // Bilder Vokabel lernen -> WordsPracticeControl.xaml
        grammarPracticeContent, // Grammatik Text Einsetzung
        grammarExplanationContent, // Gramatik Erklärung
        settingsContent,
        undefined,
    }

    public interface IView
    {
        void ChangeWindowContent(EContentType newContentType);
        void UpdateView();
    }
}