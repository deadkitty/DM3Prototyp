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
        chooseLearnsetsContent,
        wordsExerciseContent,
        grammarExerciseContent,
        grammarExplanationContent,
        optionsContent,
        undefined,
    }

    public interface IView
    {
        void ChangeWindowContent(EContentType newContentType);
        void UpdateView();
    }
}
