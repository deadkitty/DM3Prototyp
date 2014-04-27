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
    /// Interaktionslogik für GrammarExcerciseControlLessonMenue.xaml
    /// </summary>
    public partial class SelectSetsControl : UserControl
    {
        WindowCtrl ctrl;
		Data data;
		SpeechCtrl speech;
       
        EContentType eContentType;


        public SelectSetsControl(EContentType e)
        {
            InitializeComponent();
            ctrl = WindowCtrl.GetInstance();
			data = Data.GetInstance();
			speech = SpeechCtrl.GetInstance();
          
            eContentType = e;

            printTitelListBox(eContentType);

			speech.LoadChooseLessonGrammar();
           
			foreach (Lesson l in data.Lessons)
			{
				setsListbox.Items.Add(l);                       
			}         
        }
     
        private void excerciseButtonClick(object sender, RoutedEventArgs e)
        {
            if (eContentType.Equals(EContentType.grammarExplanationContent))
                GrammarExplanationControl.selectedLesson = (Lesson)setsListbox.SelectedItem;
            if (eContentType.Equals(EContentType.grammarExerciseContent))
                GrammarExerciseControl.selectedLesson = (Lesson)setsListbox.SelectedItem;
            ctrl.ChangeWindowContent(eContentType);
        }

        private void printTitelListBox(EContentType eContentTyp)
        {
            if (eContentType.Equals(EContentType.grammarExerciseContent) == true)
            {

                TitelListBox.Text = "Grammar Exercise Lesson";
            }

            if (eContentType.Equals(EContentType.grammarExplanationContent) == true)
            {
                TitelListBox.Text = "Word Exercise Lesson";
            }
        }
    }
}
