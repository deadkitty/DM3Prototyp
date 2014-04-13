using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Controll
{    
    public enum EScreen
    {
        firstStartScreen,
        mainMenuScreen,
        chooseLearnsetsScreen,
        wordsPracticeScreen,
        insertPracticeScreen,
        grammarExplanationScreen,
        settingsScreen,
    }

    public interface IView
    {
        void UpdateView();
        void UpdateView(String s);
        void OpenScreen(EScreen screen);
        IView GetParent();
    }
}
