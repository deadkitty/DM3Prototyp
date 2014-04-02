using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Prototyp.Properties;

namespace Controll
{
    class SettingsController
    {
        #region Fields

        private SettingsModel settingsModel;

        #endregion

        #region Properties

        public bool FirstStart
        {
            set
            {
                settingsModel.FirstStart = value;
                Settings.Default.FirstStart = value;
            }
        }

        #endregion

        #region Singleton

        private static SettingsController sInstance;

        public static SettingsController GetInstance()
        {
            if (sInstance == null)
            {
                sInstance = new SettingsController();
            }
            return sInstance;
        }

        #endregion

        #region Constructor

        private SettingsController()
        {
            settingsModel = SettingsModel.GetInstance();

            LoadSettings();
        }

        #endregion

        #region Public Methods

        public void LoadSettings()
        {
            settingsModel.FirstStart = Settings.Default.FirstStart;
        }

        public void SaveSettings()
        {
            Settings.Default.Save();
        }

        #endregion
    }
}
