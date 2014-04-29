using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Prototype.Speech;
using Prototype.DataModel;

namespace Prototype
{
    public partial class App : Application
    {
        #region Fields

        Settings.Settings settings;
        Settings.SettingsCtrl settingsCtrl;
        
        DataCtrl dataCtrl;

        #endregion

        #region Singleton

        private static App sInstance;

        public static App GetInstance()
        {
            return sInstance;
        }

        #endregion

        #region Events

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            sInstance = this;
            ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
            
            settings     = Settings.Settings.GetInstance();
            settingsCtrl = Settings.SettingsCtrl.GetInstance();
            settingsCtrl.LoadSettings();
            
            dataCtrl = DataCtrl.GetInstance();

            if (settings.FirstStart)
            {
                dataCtrl.InitializeDatabase();
                
                settingsCtrl.FirstStart = false;
                settingsCtrl.SaveSettings();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            SpeechCtrl.GetInstance().DeInitialize();
        }

        #endregion
    }
}
