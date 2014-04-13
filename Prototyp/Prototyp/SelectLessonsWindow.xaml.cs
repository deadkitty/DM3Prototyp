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
using System.Windows.Shapes;

namespace Prototyp
{
    /// <summary>
    /// Interaktionslogik für SelectLessonsWindow.xaml
    /// </summary>
    public partial class SelectLessonsWindow : BasicWindow
    {
        public SelectLessonsWindow()
            : base()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            WindowCtrl.CloseWindow();
            ParentWindow.Show();
            this.Close();
        }
    }
}
