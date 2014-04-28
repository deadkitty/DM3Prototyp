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

namespace Prototype.View.Controls
{
    /// <summary>
    /// Interaktionslogik für WordsPracticeControl.xaml
    /// </summary>
    public partial class WordsPracticeControl : UserControl
    {
        WindowCtrl windowCtrl;
        SpeechCtrl speechCtrl;

        Data data;
        DataCtrl dataCtrl;

        public WordsPracticeControl()
        {
            InitializeComponent();

            data = Data.GetInstance();

            dataCtrl = DataCtrl.GetInstance();
            windowCtrl = WindowCtrl.GetInstance();
            speechCtrl = SpeechCtrl.GetInstance();
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
                if (dataCtrl.CheckWord(wordTextbox.Text))
                {
                    dataCtrl.LoadNext();
                }
                else
                {
                    correctAnswerTextblock.Text = data.ActiveWord.JWord;
                }
                e.Handled = true;
            }
        }

        public void Update()
        {
            Word w = data.ActiveWord;

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
    }
}
