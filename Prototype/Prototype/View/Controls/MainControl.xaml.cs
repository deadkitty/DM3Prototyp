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


namespace Prototype.View.Controls
{
    /// <summary>
    /// Interaktionslogik für MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        WindowCtrl ctrl;
      
        public MainControl()
        {
            InitializeComponent();
            ctrl = WindowCtrl.GetInstance();
        }

        private void grammarExplanationButton_Click(object sender, RoutedEventArgs e)
        {
			ctrl.ChangeWindowContent(EContentType.grammarExplanationContent);
        }

        private void grammarExerciseButton_Click(object sender, RoutedEventArgs e)
		{
			ctrl.ChangeWindowContent(EContentType.chooseSentenceSetsContent);
        }

		private void wordsExercisesButton_click(object sender, RoutedEventArgs e)
		{
			ctrl.ChangeWindowContent(EContentType.chooseWordSetsContent);
		}

        private void wordsPracticeButton_Click(object sender, RoutedEventArgs e)
        {
            ctrl.ChangeWindowContent(EContentType.wordsPracticeContent);
        }

        private void GrammarExerciseStackPanelMouseEnter(object sender, MouseEventArgs e)
        {
            GrammarExerciseStackPanel.Background = Brushes.BlanchedAlmond;
            GrammarExerciseTitel.Foreground = Brushes.Gray;
            GrammarExerciseText.Foreground = Brushes.Gray;
        }

        private void GrammarExerciseStackPanelMouseLeave(object sender, MouseEventArgs e)
        {
            GrammarExerciseStackPanel.Background = Brushes.Gray;
            GrammarExerciseTitel.Foreground = Brushes.WhiteSmoke;
            GrammarExerciseText.Foreground = Brushes.WhiteSmoke;
        }

        private void SpeechControlMouseEnter(object sender, MouseEventArgs e)
        {
            SpeechControlStackPanel.Background = Brushes.BlanchedAlmond;
            SpeechControlTitel.Foreground = Brushes.Gray;
            SpeechControlText.Foreground = Brushes.Gray;
        }

        private void SpeechControlMouseLeave(object sender, MouseEventArgs e)
        {
            SpeechControlStackPanel.Background = Brushes.Gray;
            SpeechControlTitel.Foreground = Brushes.WhiteSmoke;
            SpeechControlText.Foreground = Brushes.WhiteSmoke;
        }

        private void SettingMouseEnter(object sender, MouseEventArgs e)
        {
            SettingStackPanel.Background = Brushes.BlanchedAlmond;
            SettingTitel.Foreground = Brushes.Gray;
            SettingText.Foreground = Brushes.Gray;
        }

        private void SettingMouseLeave(object sender, MouseEventArgs e)
        {
            SettingStackPanel.Background = Brushes.Gray;
            SettingTitel.Foreground = Brushes.WhiteSmoke;
            SettingText.Foreground = Brushes.WhiteSmoke;
        }

		//public void SelectEntries()
		//{
		//    if (data.SelectedLessons == null)
		//    {
		//        lessonsListbox.SelectedItems.Clear();
		//        return;
		//    }

		//    if (data.SelectedLessons == data.Lessons)
		//    {
		//        lessonsListbox.SelectAll();
		//        return;
		//    }

		//    lessonsListbox.SelectedItems.Clear();
		//    foreach (object o in data.SelectedLessons)
		//    {
		//        lessonsListbox.SelectedItems.Add(o);
		//    }
		//}
    }
}
