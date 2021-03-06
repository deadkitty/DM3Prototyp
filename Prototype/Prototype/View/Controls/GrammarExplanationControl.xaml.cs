﻿using System.Collections.Generic;
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
    public partial class GrammarExplanationControl : UserControl, ISpeech
    {
        Data data;
        WindowCtrl ctrl;

        public GrammarExplanationControl()
        {
            InitializeComponent();

            data = Data.GetInstance();
            ctrl = WindowCtrl.GetInstance();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            ctrl.GoBack();
        }

        private void mainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            ctrl.ChangeWindowContent(EContentType.mainMenuContent);
        }

        public void ExecuteCommand(ECommand command, object content = null)
        {

        }
    }
}
