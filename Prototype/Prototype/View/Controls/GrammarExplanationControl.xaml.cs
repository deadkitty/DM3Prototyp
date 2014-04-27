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


namespace Prototype.View.Controls
{
    /// <summary>
    /// Interaktionslogik für GrammarExplanationControl.xaml
    /// </summary>
    public partial class GrammarExplanationControl : UserControl
    {
        WindowCtrl ctrl;
        public static Lesson selectedLesson;
        Data data;
        Word word;
        int count; // anzahl der Wörter
        int[] setIds; // Set Id der aktuellen Lesson
        Word[] words;
        public GrammarExplanationControl()
        {
            InitializeComponent();
            ctrl = WindowCtrl.GetInstance();
            data = Data.GetInstance();
            LessonNumber.Content = "Lesson";
            LessonContent.Content = selectedLesson.Name;

            
            setIds = new int[1];
            setIds[0] = selectedLesson.ID;
            count = data.GetWords(setIds).Length;
            words = data.GetWords(setIds);
            // erstes Word einfügen
            word = words.First<Word>();
            germanWord.Text = word.JWord;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            ctrl.GoBack();
        }

        private void mainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            ctrl.ChangeWindowContent(EContentType.mainMenuContent);
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            bool isLesson = false;


            foreach (Word w in data.GetWords(setIds))
            {
                /*if (isLesson.Equals(true))
                {
                    // nächste Lesson anzeigen
                    selectedLesson = l;
                    LessonNumber.Content = "Lesson";
                    LessonContent.Content = selectedLesson.Name;
                    
                }
                if (l.ID.Equals(selectedLesson.ID))
                {
                    isLesson = true;
                }*/
               
                
            }
           
        }

        public void Update()
        {

        }      
    }
}
