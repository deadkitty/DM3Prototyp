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

namespace Prototype.View.Controls
{
    /// <summary>
    /// Interaktionslogik für SettingControl.xaml
    /// </summary>
    public partial class SettingControl : UserControl, ISpeech
    {
        public SettingControl()
        {
            InitializeComponent();
        }

         private void masterNameClick(object sender, RoutedEventArgs e)
        {
            printNameBox.Text = "Vielen Dank, dein Name ist jetzt Master.";
        }

        public void ExecuteCommand(ECommand command, object content = null)
        {
            throw new NotImplementedException();
        }
    }
}
