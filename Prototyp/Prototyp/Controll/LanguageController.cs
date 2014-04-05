using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controll
{
    class LanguageController
    {
        #region Fields
        
        IView view;

        #endregion

        #region Properties

        public IView View
        {
            get { return view; }
            set { view = value; }
        }

        #endregion

        #region Singleton

        private static LanguageController sInstance;

        public static LanguageController GetInstance()
        {
            if (sInstance == null)
            {
                sInstance = new LanguageController();
            }
            return sInstance;
        }

        #endregion

        #region Constructor

        private LanguageController()
        {

        }

        #endregion

        #region Public Methods

        public void ComputeSpeechRecognition(String speechText)
        {

        }

        #endregion
    }
}
