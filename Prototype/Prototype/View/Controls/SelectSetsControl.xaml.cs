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
        DataCtrl dataCtrl;
		SpeechCtrl speech;
       
        EContentType eContentType;

        public SelectSetsControl(EContentType e)
        {
            InitializeComponent();

            ctrl = WindowCtrl.GetInstance();

			data = Data.GetInstance();
            dataCtrl = DataCtrl.GetInstance();

            speech = SpeechCtrl.GetInstance();
            speech.LoadChooseLessonGrammar();
          
            eContentType = e;

            printTitelListBox(eContentType);

            int i = 1;
			foreach (Lesson l in data.Lessons)
			{
                l.listIndex = i++;
				setsListbox.Items.Add(l);                       
			}         
        }

        private void printTitelListBox(EContentType eContentTyp)
        {
            if (eContentType.Equals(EContentType.grammarPracticeContent) == true)
            {
                TitelListBox.Text = "Grammar Exercise Lesson";
            }

            if (eContentType.Equals(EContentType.grammarExplanationContent) == true)
            {
                TitelListBox.Text = "Word Exercise Lesson";
            }
        }

        private void practiceButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (setsListbox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Keine Lektion ausgewählt!");
                return;
            }

            int setsCount = setsListbox.SelectedItems.Count;

            Lesson[] lessons = new Lesson[setsCount];

            for (int i = 0; i < setsCount; ++i)
            {
                lessons[i] = setsListbox.SelectedItems[i] as Lesson;
            }

            dataCtrl.Load(lessons);

            if (lessons[0].type == (int)Lesson.EType.wordsPractice)
            {
                ctrl.ChangeWindowContent(EContentType.wordsPracticeContent);
            }
            else
            {
                ctrl.ChangeWindowContent(EContentType.grammarPracticeContent);
            }
        }
    }
}
