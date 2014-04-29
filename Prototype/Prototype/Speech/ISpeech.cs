using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype.Speech
{
    public enum ECommand
    {
        beginExercise,
        showAnswer,
        skipItem,
        logAnswer,
        setAnswer,
    }

    public interface ISpeech
    {
        void ExecuteCommand(ECommand command, object content = null);
    }
}
