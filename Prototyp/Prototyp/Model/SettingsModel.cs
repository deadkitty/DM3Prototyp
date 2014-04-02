using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SettingsModel
    {
        #region Fields

        private bool firstStart;

        #endregion

        #region Properties

        public bool FirstStart
        {
            get { return firstStart; }
            set { firstStart = value; }
        }

        #endregion

        #region Singleton

        private static SettingsModel sInstance;

        public static SettingsModel GetInstance()
        {
            if (sInstance == null)
            {
                sInstance = new SettingsModel();
            }
            return sInstance;
        }

        #endregion

        #region Constructor

        private SettingsModel()
        {

        }

        #endregion
    }
}
