﻿using System;
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
using Prototype.Speech;
using Prototype.DataModel;
using Prototype.DataModel.Tables;
using System.Threading;
using System.Windows.Threading;

namespace Prototype.View.Controls
{
    /// <summary>
    /// Interaktionslogik für WordsPracticeControl.xaml
    /// </summary>
    public partial class WordsPracticeControl : UserControl, ISpeech, IDisposable
    {
        WindowCtrl windowCtrl;
        SpeechCtrl speechCtrl;

        Data data;
        DataCtrl dataCtrl;

        DispatcherTimer timer = null;

        bool skipUpdate = false;
        public bool wordCorrect;

        public WordsPracticeControl()
        {
            InitializeComponent();

            data = Data.GetInstance();

            dataCtrl = DataCtrl.GetInstance();
            windowCtrl = WindowCtrl.GetInstance();
            speechCtrl = SpeechCtrl.GetInstance();

            speechCtrl.LoadNextItemGrammar();
            speechCtrl.LoadShowAnswerGrammar();
            speechCtrl.LoadLogWordGrammar();
            speechCtrl.LoadWordsGrammar();
        }

        private void selectLessonsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            windowCtrl.ChangeWindowContent(EContentType.chooseWordSetsContent);
        }

        private void skipWordButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            dataCtrl.LoadNext();
        }

        private void wordTextbox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CheckAnswer(wordTextbox.Text);
                e.Handled = true;
            }
        }

        private void CheckAnswer(String text)
        {
            if (dataCtrl.CheckWord(text))
            {
                wordTextbox.Background = new SolidColorBrush(Colors.GreenYellow);
                wordCorrect = true;

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(2000);
                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();
            }
            else
            {
				textBlockRightAnswer.Text = "Richtige Antwort ist ";
                correctAnswerTextblock.Text = data.ActiveWord.JWord;
                wordTextbox.Background = new SolidColorBrush(Colors.Red);
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            dataCtrl.LoadNext();
            timer.Stop();
        }

        public void Update()
        {
            if (skipUpdate)
            {
                skipUpdate = false;
                return;
            }

            LoadNextWord();
            if (wordCorrect)
            {                
                wordCorrect = false;
            }
        }

        public void LoadNextWord()
        {
            Word w = data.ActiveWord;
			textBlockRightAnswer.Text = "";
            wordTextbox.Background = new SolidColorBrush(Colors.White);
            wordTextbox.Text = "";
            correctAnswerTextblock.Text = "";

            if (w.IsImagePath)
            {
                imageBox.Visibility = System.Windows.Visibility.Visible;
                imageBox.Source = new BitmapImage(new Uri(@"\Prototype;component\Resources\Images\" + w.Translation, UriKind.Relative));

                translationTextblock.Text = "";
            }
            else
            {
                imageBox.Visibility = System.Windows.Visibility.Hidden;
                translationTextblock.Text = w.Translation;
                
            }
        }

        public void ExecuteCommand(ECommand command, object content = null)
        {
            switch (command)
            {
                case ECommand.skipItem: skipWordButton_Click(this, null); break;
                case ECommand.showAnswer: CheckAnswer(""); skipUpdate = true; break;
                case ECommand.logAnswer: LogWord(content as String); skipUpdate = true; break;
                case ECommand.unlogAnswer: UnlogWord(content as String); skipUpdate = true; break;
                case ECommand.setAnswer: wordTextbox.Text = content as String; skipUpdate = true; break;
            }
        }

        public void LogWord(String text)
        {
            if (wordTextbox.Text.Length == 0)
            {
                wordTextbox.Text = text;
            }
            else
            {
                CheckAnswer(wordTextbox.Text);
            }
        }

        public void UnlogWord(String text)
        {
            if (wordTextbox.Text.Length == 0)
            {
                wordTextbox.Text = text;
            }
            else
            {
                wordTextbox.Text = "";
            }
        }
                
        public void Dispose()
        {
            speechCtrl.UnloadNextItemGrammar();
            speechCtrl.UnloadShowAnswerGrammar();
            speechCtrl.UnloadLogWordGrammar();
            speechCtrl.UnloadWordsGrammar();
        }
    }
}
