using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype.Settings
{
    class SettingsCtrl
    {
        #region Fields

        private Settings settings;

        #endregion

        #region Properties

        public Settings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public bool FirstStart
        {
            set
            {
                settings.FirstStart = value;
                Properties.Settings.Default.FirstStart = value;
            }
        }

        #endregion

        #region Singleton

        private static SettingsCtrl sInstance;

        public static SettingsCtrl GetInstance()
        {
            if (sInstance == null)
            {
                sInstance = new SettingsCtrl();
            }
            return sInstance;
        }

        #endregion

        #region Constructor

        private SettingsCtrl()
        {
            settings = Settings.GetInstance();
        }

        #endregion

        #region Public Methods

        public void LoadSettings()
        {
            settings.FirstStart = Properties.Settings.Default.FirstStart;
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }

        #endregion
    }
}
