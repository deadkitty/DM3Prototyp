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
using Prototype.DataModel;
using Prototype.DataModel.Tables;

namespace Prototype.View.Controls
{
    /// <summary>
    /// Interaktionslogik für MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        WindowCtrl ctrl;

        Data data;

        public MainControl()
        {
            InitializeComponent();
            ctrl = WindowCtrl.GetInstance();
            data = Data.GetInstance();

            for (int i = 0; i < data.Lessons.Length; ++i)
            {
                data.Lessons[i].listIndex = i + 1;
                lessonsListbox.Items.Add(data.Lessons[i]);
            }
            lessonsListbox.SelectedItems.Clear();

        }

        private void grammarExplanationButton_Click(object sender, RoutedEventArgs e)
        {
            ctrl.ChangeWindowContent(EContentType.grammarExplanationContent);
        }

        private void grammarExerciseButton_Click(object sender, RoutedEventArgs e)
        {
            ctrl.ChangeWindowContent(EContentType.grammarExerciseContent);
        }

        private void closeAppButton_Click(object sender, RoutedEventArgs e)
        {
            ctrl.CloseApp();
        }

        public void SelectEntries()
        {
            if (data.SelectedLessons == null)
            {
                lessonsListbox.SelectedItems.Clear();
                return;
            }

            if (data.SelectedLessons == data.Lessons)
            {
                lessonsListbox.SelectAll();
                return;
            }

            lessonsListbox.SelectedItems.Clear();
            foreach (object o in data.SelectedLessons)
            {
                lessonsListbox.SelectedItems.Add(o);
            }
        }
    }
}
