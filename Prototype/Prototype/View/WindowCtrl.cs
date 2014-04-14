using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype.View
{
    public class WindowCtrl
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

        private static WindowCtrl sInstance;

        public static WindowCtrl GetInstance()
        {
            if (sInstance == null)
            {
                sInstance = new WindowCtrl();
            }
            return sInstance;
        }

        #endregion

        #region Constructor

        private WindowCtrl()
        {
            application = App.GetInstance();
        }

        #endregion

        #region Public Methods

        public void ChangeWindowContent(EContentType newContentType)
        {
            view.ChangeWindowContent(newContentType);
        }

        public void CloseApp()
        {
            application.Shutdown();
        }

        #endregion
    }
}
