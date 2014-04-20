using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prototype.View.Controls;
using Prototype.Speech;
using Prototype.DataModel;

namespace Prototype.View
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        WindowCtrl windowCtrl;
        SpeechCtrl speechCtrl;

        Data data;

        int commandCounter = 0;

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            data = Data.GetInstance();

            windowCtrl = WindowCtrl.GetInstance();
            windowCtrl.View = this;
            windowCtrl.ChangeWindowContent(EContentType.mainMenuContent);

            speechCtrl = SpeechCtrl.GetInstance();
            speechCtrl.Initialize();
            speechCtrl.View = this;
        }
        
        #endregion

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region IView

        public void ChangeWindowContent(EContentType newContentType)
        {
            switch (newContentType)
            {
                case EContentType.grammarExplanationContent: contentControl.Content = new GrammarExplanationControl(); break;
                case EContentType.   grammarExerciseContent: contentControl.Content = new    GrammarExerciseControl(); break;
                case EContentType.          mainMenuContent: contentControl.Content = new               MainControl(); break;
            }
        }

        public void UpdateView()
        {
            Title = (++commandCounter) + data.CurrentComand;
            if (windowCtrl.CurrentContentType == EContentType.mainMenuContent)
                (contentControl.Content as MainControl).SelectEntries();
        }
        
        #endregion
    }
}
