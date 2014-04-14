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

        private void closeAppButton_Click(object sender, RoutedEventArgs e)
        {
            ctrl.CloseApp();
        }
    }
}
