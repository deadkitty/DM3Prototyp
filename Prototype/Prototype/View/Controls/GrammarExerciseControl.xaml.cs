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
    /// Interaktionslogik für GrammarExerciseControl.xaml
    /// </summary>
    public partial class GrammarExerciseControl : UserControl
    {
        WindowCtrl ctrl;
        Data data;
               
        public GrammarExerciseControl()
        {
            InitializeComponent();
            ctrl = WindowCtrl.GetInstance();
            data = Data.GetInstance();           
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            ctrl.GoBack();
        }

        private void mainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            ctrl.ChangeWindowContent(EContentType.mainMenuContent);
        }

        private void PrevButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
			ctrl.GoBack();
        }

        public void Update()
        {

        }
    }
}
