using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Model;
using Controll;

namespace Prototyp
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private static App sInstance;

        public static App GetInstance()
        {
            return sInstance;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            sInstance = this;

            DataModel.GetInstance();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            (LanguageController.GetInstance()).DeInitialize();
        }
    }
}
