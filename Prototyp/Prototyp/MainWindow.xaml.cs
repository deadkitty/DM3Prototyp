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
using Model;
using System.Speech.Recognition;
using Controll;

namespace Prototyp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : BasicWindow
    {
        DataModel data;

        LanguageController languageCtrl;

        public MainWindow()
            : base()
        {
            InitializeComponent();
            data = DataModel.GetInstance();

        }

        void recognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            UpdateView(e.Result.Text);
        }

        private void selectLessonsButton_Click(object sender, RoutedEventArgs e)
        {
            SelectLessonsWindow window = new SelectLessonsWindow();
            window.ParentWindow = this;
            window.Show();
            this.Hide();
        }

        private void grammarPracticeButton_Click(object sender, RoutedEventArgs e)
        {
            GrammarPracticeWindow window = new GrammarPracticeWindow();
            window.ParentWindow = this;
            window.Show();
            this.Hide();
        }

        private void wordsPracticeButton_Click(object sender, RoutedEventArgs e)
        {
            WordsPracticeWindow window = new WordsPracticeWindow();
            window.ParentWindow = this;
            window.Show();
            this.Hide();
        }

        private void grammarExplanationButton_Click(object sender, RoutedEventArgs e)
        {
            GrammarExplanationWindow window = new GrammarExplanationWindow();
            window.ParentWindow = this;
            window.Show();
            this.Hide();
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow();
            window.ParentWindow = this;
            window.Show();
            this.Hide();
        }

        public override void OpenScreen(EScreen screen)
        {
            switch (screen)
            {
                case EScreen.chooseLearnsetsScreen   :      selectLessonsButton_Click(this, null); break;
                case EScreen.insertPracticeScreen    :    grammarPracticeButton_Click(this, null); break;
                case EScreen.grammarExplanationScreen: grammarExplanationButton_Click(this, null); break;
                case EScreen.settingsScreen          :           settingsButton_Click(this, null); break;
                case EScreen.wordsPracticeScreen     :      wordsPracticeButton_Click(this, null); break;
                case EScreen.mainMenuScreen          :                                             break;
                case EScreen.firstStartScreen        :                                             break;
            }
        }

        private void BasicWindow_Loaded(object sender, RoutedEventArgs e)
        {
            languageCtrl = LanguageController.GetInstance();
            languageCtrl.View = this;
            languageCtrl.Initialize();

            languageCtrl.RecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognitionEngine_SpeechRecognized);
        }
    }
}
