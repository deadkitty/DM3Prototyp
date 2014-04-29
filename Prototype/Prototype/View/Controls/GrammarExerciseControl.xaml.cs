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
using Prototype.DataModel.Tables;
using Prototype.DataModel;
using Prototype.Speech;
using System;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Prototype.View.Controls
{
    /// <summary>
    /// Interaktionslogik für GrammarExerciseControl.xaml
    /// </summary>
    public partial class GrammarExerciseControl : UserControl, ISpeech, IDisposable
    {
        #region Fields

        Button[] answerButtons;

        Random rand;

        int correctButtonIndex = 0;
        int clickedButtonIndex = 0;

        WindowCtrl windowCtrl;
        SpeechCtrl speechCtrl;

        Data data;
        DataCtrl dataCtrl;

        Storyboard correctButtonStoryboard;
        Storyboard wrongButtonStoryboard;
        ColorAnimation correctButtonColorAnim;
        ColorAnimation wrongButtonColorAnim;

        bool skipUpdate = false;

        DispatcherTimer timer = null;

        #region Particle

#if japaneseVersion

        String[] particle = 
        {        
            "は",
            "も",
            "の",
            "を",
            "が",
            "に",
            "へ",
            "で",
            "と",
            "や",
            "よ",
            "か",
            "ね",
            "ら",
            "て",
            "から",
            "まで",
            "までに",
            "より",
            "でも",
            "とき",
            "くて",
            "たり",
            "たら",
            "ても",
            "てから",
        };

#else

        String[] particle = 
        {        
            "wa",
            "mo",
            "no",
            "o",
            "ga",
            "ni",
            "e",
            "de",
            "to",
            "ja",
            "jo",
            "ka",
            "ne",
            "ra",
            "te",
            "kara",
            "made",
            "madeni",
            "yori",
            "demo",
            "toki",
            "kute",
            "tari",
            "tara",
            "temo",
            "tekara",
        };

#endif
        #endregion

        #endregion

        public GrammarExerciseControl()
        {
            InitializeComponent();

            data = Data.GetInstance();

            dataCtrl = DataCtrl.GetInstance();
            windowCtrl = WindowCtrl.GetInstance();
            speechCtrl = SpeechCtrl.GetInstance();

            speechCtrl.LoadNextItemGrammar();
            speechCtrl.LoadShowAnswerGrammar();

            rand = new Random();

            correctButtonStoryboard = Resources["correctButtonStoryboard"] as Storyboard;
            wrongButtonStoryboard = Resources["wrongButtonStoryboard"] as Storyboard;

            correctButtonColorAnim = correctButtonStoryboard.Children[0] as ColorAnimation;
            wrongButtonColorAnim = wrongButtonStoryboard.Children[0] as ColorAnimation;

            answerButtons = new Button[]
            {
                answer1Button,
                answer2Button,
                answer3Button,
                answer4Button,
                answer5Button,
                answer6Button,
                answer7Button,
                answer8Button,
            };

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(2000);
            timer.Tick += new EventHandler(timer_Tick);
        }

        #region Answer 1 to 8 Button Clicks

        private void answer1Button_Click(object sender, RoutedEventArgs e)
        {
            AnswerButtonClick(0);
        }

        private void answer2Button_Click(object sender, RoutedEventArgs e)
        {
            AnswerButtonClick(1);
        }

        private void answer3Button_Click(object sender, RoutedEventArgs e)
        {
            AnswerButtonClick(2);
        }

        private void answer4Button_Click(object sender, RoutedEventArgs e)
        {
            AnswerButtonClick(3);
        }

        private void answer5Button_Click(object sender, RoutedEventArgs e)
        {
            AnswerButtonClick(4);
        }

        private void answer6Button_Click(object sender, RoutedEventArgs e)
        {
            AnswerButtonClick(5);
        }

        private void answer7Button_Click(object sender, RoutedEventArgs e)
        {
            AnswerButtonClick(6);
        }

        private void answer8Button_Click(object sender, RoutedEventArgs e)
        {
            AnswerButtonClick(7);
        }
        
        #endregion

        private void AnswerButtonClick(int clickedButtonIndex)
        {
            this.clickedButtonIndex = clickedButtonIndex;

            if (clickedButtonIndex != correctButtonIndex)
            {
                wrongButtonStoryboard.Stop();
                wrongButtonColorAnim.SetValue(Storyboard.TargetNameProperty, answerButtons[clickedButtonIndex].Name);
                wrongButtonStoryboard.Begin();
                correctButtonStoryboard.Stop();
                correctButtonColorAnim.SetValue(Storyboard.TargetNameProperty, answerButtons[correctButtonIndex].Name);
                correctButtonStoryboard.Begin();
            }
            else
            {
                correctButtonStoryboard.Stop();
                correctButtonColorAnim.SetValue(Storyboard.TargetNameProperty, answerButtons[correctButtonIndex].Name);
                correctButtonStoryboard.Begin();
                timer.Start();
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            dataCtrl.LoadNext();
            timer.Stop();
        }

        private void mainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            windowCtrl.ChangeWindowContent(EContentType.mainMenuContent);
        }

        private void prevButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            windowCtrl.GoBack();
        }
        
        private void skipSentenceButton_Click(object sender, RoutedEventArgs e)
        {
            dataCtrl.LoadNext();
        }

        public void Update()
        {
            if (skipUpdate)
            {
                skipUpdate = false;
                return;
            }

            wrongButtonStoryboard.Stop();
            correctButtonStoryboard.Stop();

            Sentence s = data.ActiveSentence;
            sentenceTextblock.Text = s.CreateInsertString();

            for (int i = 0; i < particle.Length; ++i)
            {
                int randomIndex = rand.Next(particle.Length);
                String hv = particle[i];
                particle[i] = particle[randomIndex];
                particle[randomIndex] = hv;
            }

            for (int i = 0; i < answerButtons.Length; ++i)
            {
                answerButtons[i].Content = particle[i];
            }

            for (int i = 0; i < answerButtons.Length; ++i)
            {
                if (answerButtons[i].Content.ToString() == data.ActiveSentence.insertParts[data.ActiveSentence.insertPosition])
                {
                    correctButtonIndex = i;
                    return;
                }
            }
            correctButtonIndex = rand.Next(answerButtons.Length);
            answerButtons[correctButtonIndex].Content = data.ActiveSentence.insertParts[data.ActiveSentence.insertPosition];

            String[] grammarParticle = new String[answerButtons.Length];
            for (int i = 0; i < answerButtons.Length; ++i)
            {
                grammarParticle[i] = answerButtons[i].Content as String;
            }
            speechCtrl.UnloadChooseParticleGrammar();
            speechCtrl.LoadChooseParticleGrammar(grammarParticle);
        }

        public void ExecuteCommand(ECommand command, object content = null)
        {
            switch (command)
            {
                case ECommand.skipItem: skipSentenceButton_Click(this, null); break;
                case ECommand.showAnswer: AnswerButtonClick((correctButtonIndex + 1) % answerButtons.Length); skipUpdate = true; break;
                case ECommand.setAnswer: ComputeAnswer((int)content); skipUpdate = true; break;
            }
        }

        private void ComputeAnswer(int buttonIndex)
        {
            AnswerButtonClick(buttonIndex);
            //for (int i = 0; i < answerButtons.Length; ++i)
            //{
            //    if (answerButtons[i].Content as String == text)
            //    {
            //        AnswerButtonClick(i);
            //        break;
            //    }
            //}
        }

        public void Dispose()
        {
            speechCtrl.UnloadNextItemGrammar();
            speechCtrl.UnloadShowAnswerGrammar();
            speechCtrl.UnloadChooseParticleGrammar();
        }
    }
}
