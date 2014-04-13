using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prototyp;
using System.Windows;

namespace Controll
{
    public class WindowController
    {
        #region Fields

        App application;

        IView view;

        bool closeWindow;

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
            application = App.GetInstance();
        }

        #endregion

        #region Public Methods

        public void CloseApp()
        {
            if (closeWindow)
            {
                closeWindow = false;
            }
            else
            {
                application.Shutdown();
            }
        }

        public void OpenWindow()
        {
            EScreen screen = EScreen.chooseLearnsetsScreen;

            view.OpenScreen(screen);
            view.UpdateView();
        }

        public void CloseWindow()
        {
            closeWindow = true;
            
            view.UpdateView();
        }

        #endregion
    }
}
