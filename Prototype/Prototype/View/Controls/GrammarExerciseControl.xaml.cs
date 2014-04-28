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

namespace Prototype.View.Controls
{
    /// <summary>
    /// Interaktionslogik für GrammarExerciseControl.xaml
    /// </summary>
    public partial class GrammarExerciseControl : UserControl
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


        #region Particle

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

        #endregion

        #endregion
               
        public GrammarExerciseControl()
        {
            InitializeComponent();

            data = Data.GetInstance();

            dataCtrl = DataCtrl.GetInstance();
            windowCtrl = WindowCtrl.GetInstance();
            speechCtrl = SpeechCtrl.GetInstance();

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
        }

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
            dataCtrl.CheckSentence(clickedButtonIndex, correctButtonIndex);
        }

        private void PlayCorrectButtonAnimation()
        {
            ColorAnimation ca = new ColorAnimation(Colors.Green, TimeSpan.FromMilliseconds(800));
                

        }

        private void PlayWrongButtonAnimation()
        {

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

        }

        public void Update()
        {
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
        }
    }
}
