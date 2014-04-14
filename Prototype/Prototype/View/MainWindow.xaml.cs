﻿using System;
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
using Prototype.View.Controls;
using Prototype.Speech;

namespace Prototype.View
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        WindowCtrl windowCtrl;
        SpeechCtrl speechCtrl;

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            windowCtrl = WindowCtrl.GetInstance();
            windowCtrl.View = this;

            speechCtrl = SpeechCtrl.GetInstance();
            speechCtrl.Initialize();

            ChangeWindowContent(EContentType.mainMenuContent);
        }
        
        #endregion

        #region IView

        public void ChangeWindowContent(EContentType newContentType)
        {
            switch (newContentType)
            {
                case EContentType.grammarExplanationContent: contentControl.Content = new GrammarExplanationControl(); break;
                case EContentType.          mainMenuContent: contentControl.Content = new               MainControl(); break;
            }
        }

        public void UpdateView()
        {

        }
        
        #endregion
    }
}