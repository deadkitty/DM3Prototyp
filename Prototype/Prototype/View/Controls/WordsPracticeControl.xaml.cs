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
                correctAnswerTextblock.Text = data.ActiveWord.JWord;
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
                //nextWordTimer = new System.Threading.Timer(obj => { LoadNextWord(); }, null, 1000, System.Threading.Timeout.Infinite);
                wordCorrect = false;
            }
            else
            {
            }
        }

        public void LoadNextWord()
        {
            Word w = data.ActiveWord;

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
            }
        }

        public void Dispose()
        {
            speechCtrl.UnloadNextItemGrammar();
            speechCtrl.UnloadShowAnswerGrammar();
        }
    }
}
