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
        settingsScreen,
    }

    public interface IView
    {
        void UpdateView();
        void OpenScreen(EScreen screen);
        IView GetParent();
    }
}
