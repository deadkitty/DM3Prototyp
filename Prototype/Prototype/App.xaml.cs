using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Prototype
{
    public partial class App : Application
    {
        #region Fields

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
        }

        protected override void OnExit(ExitEventArgs e)
        {

        }

        #endregion
    }
}
