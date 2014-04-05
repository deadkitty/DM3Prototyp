using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prototyp;
using System.Windows;

namespace Controll
{
    class WindowController
    {
        #region Fields

        App application;

        IView view;

        #endregion

        #region Properties
        
        public App Application
        {
            get { return application; }
            set { application = value; }
        }

        public IView View
        {
            get { return view; }
            set { view = value; }
        }

        #endregion

        #region Singleton

        private static WindowController sInstance;

        public static WindowController GetInstance()
        {
            if (sInstance == null)
            {
                sInstance = new WindowController();
            }
            return sInstance;
        }

        #endregion

        #region Constructor

        private WindowController()
        {

        }

        #endregion

        #region Public Methods

        public void CloseApp()
        {
            application.Shutdown();
        }

        public void CloseWindow()
        {

        }

        #endregion
    }
}
