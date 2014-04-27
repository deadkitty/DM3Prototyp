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

        int commandCounter = 0;

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            data = Data.GetInstance();
            dataCtrl = DataCtrl.GetInstance();
            dataCtrl.View = this;

            windowCtrl = WindowCtrl.GetInstance();
            windowCtrl.View = this;
            windowCtrl.ChangeWindowContent(EContentType.mainMenuContent);

            speechCtrl = SpeechCtrl.GetInstance();
            speechCtrl.Initialize();
            speechCtrl.View = this;

            currentContentType = EContentType.mainMenuContent;
        }

        #endregion

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        // Beendet die Applikation

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
            windowCtrl.ChangeWindowContent(EContentType.optionsContent);
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
            currentContentType = newContentType;

            switch (newContentType)
            {
                case EContentType.mainMenuContent: contentControl.Content = new MainControl(); break;
                case EContentType.chooseWordSetsContent: contentControl.Content = new SelectSetsControl(EContentType.grammarExplanationContent); break;
                case EContentType.chooseSentenceSetsContent: contentControl.Content = new SelectSetsControl(EContentType.grammarExerciseContent); break;
                case EContentType.grammarExplanationContent: contentControl.Content = new GrammarExplanationControl();
                    mainMenuButton.Background = Brushes.Black;
                    break;
                case EContentType.grammarExerciseContent: contentControl.Content = new GrammarExerciseControl(); break;
                case EContentType.wordsPracticeContent: contentControl.Content = new WordsPracticeControl(); break;
                case EContentType.optionsContent: contentControl.Content = new SettingControl(); break;
            }
        }

        public void UpdateView()
        {
            Title = (++commandCounter) + data.CurrentComand;

            switch (currentContentType)
            {
                case EContentType.grammarExerciseContent: (contentControl.Content as GrammarExerciseControl).Update(); break;
                case EContentType.grammarExplanationContent: (contentControl.Content as GrammarExplanationControl).Update(); break;
            }
        }

        #endregion
    }
}