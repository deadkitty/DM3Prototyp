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
        DataCtrl dataCtrl;

        EContentType currentContentType;
        
        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            data = Data.GetInstance();
            dataCtrl = DataCtrl.GetInstance();
            dataCtrl.View = this;

            speechCtrl = SpeechCtrl.GetInstance();
            speechCtrl.Initialize();
            speechCtrl.View = this;

            windowCtrl = WindowCtrl.GetInstance();
            windowCtrl.View = this;
            windowCtrl.ChangeWindowContent(EContentType.mainMenuContent);

            currentContentType = EContentType.mainMenuContent;
        }

        #endregion

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        
        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
            windowCtrl.ChangeWindowContent(EContentType.settingsContent);
        }

        // Zeigt das Menue an
        private void mainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            windowCtrl.ChangeWindowContent(EContentType.mainMenuContent);
        }

        #endregion

        #region IView

        public void ChangeWindowContent(EContentType newContentType)
        {
            switch (currentContentType)
            {
                case EContentType.mainMenuContent           :  break;
                case EContentType.chooseWordSetsContent     : (contentControl.Content as SelectSetsControl).Dispose(); break;
                case EContentType.chooseSentenceSetsContent : (contentControl.Content as SelectSetsControl).Dispose(); break;
                case EContentType.wordsPracticeContent      : (contentControl.Content as WordsPracticeControl).Dispose(); break;
                case EContentType.grammarPracticeContent    : (contentControl.Content as GrammarExerciseControl).Dispose(); break;
                case EContentType.grammarExplanationContent : break;
                case EContentType.settingsContent           : break;
            }

            currentContentType = newContentType;

            switch (newContentType)
            {
                case EContentType.mainMenuContent           : contentControl.Content = new MainControl(); break;
                case EContentType.chooseWordSetsContent     : contentControl.Content = new SelectSetsControl(EContentType.chooseWordSetsContent); break;
                case EContentType.chooseSentenceSetsContent : contentControl.Content = new SelectSetsControl(EContentType.chooseSentenceSetsContent); break;
                case EContentType.wordsPracticeContent      : contentControl.Content = new WordsPracticeControl(); break;
                case EContentType.grammarPracticeContent    : contentControl.Content = new GrammarExerciseControl(); break;
                case EContentType.grammarExplanationContent : contentControl.Content = new GrammarExplanationControl(); break;
                case EContentType.settingsContent           : contentControl.Content = new SettingControl(); break;
            }

            speechCtrl.SpeechCommandExecuter = contentControl.Content as ISpeech;
        }

        public void UpdateView()
        {
            Title = data.CurrentComand;

            switch (currentContentType)
            {
                case EContentType.chooseSentenceSetsContent:(contentControl.Content as SelectSetsControl).Update(); break;
                case EContentType.chooseWordSetsContent:(contentControl.Content as SelectSetsControl).Update(); break;
                case EContentType.grammarPracticeContent: (contentControl.Content as GrammarExerciseControl).Update(); break;
                case EContentType.wordsPracticeContent: (contentControl.Content as WordsPracticeControl).Update(); break;
            }
        }

        #endregion
    }
}