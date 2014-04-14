using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype.Settings
{
    class Settings
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

        private static Settings sInstance;

        public static Settings GetInstance()
        {
            if (sInstance == null)
            {
                sInstance = new Settings();
            }
            return sInstance;
        }

        #endregion

        #region Constructor

        private Settings()
        {

        }

        #endregion
    }
}
