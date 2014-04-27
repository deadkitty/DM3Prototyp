using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prototype.DataModel;

namespace Prototype.View
{
    public class WindowCtrl
    {
        #region Fields

        App application;

        IView view;

        List<EContentType> contentTrace;

        EContentType currentContentType;

        DataCtrl dataCtrl;

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

        public EContentType CurrentContentType
        {
            get { return currentContentType; }
            set { currentContentType = value; }
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
            contentTrace = new List<EContentType>();
            currentContentType = EContentType.undefined;
            dataCtrl = DataCtrl.GetInstance();
        }

        #endregion

        #region Public Methods

        public void ChangeWindowContent(EContentType newContentType)
        {
            if (newContentType == currentContentType)
                return;

            switch (newContentType)
            {
                case EContentType.chooseWordSetsContent: dataCtrl.Initialize(newContentType); break;
                case EContentType.chooseSentenceSetsContent: dataCtrl.Initialize(newContentType); break;
            }
            view.ChangeWindowContent(newContentType);

            contentTrace.Add(currentContentType);
            currentContentType = newContentType;

            view.UpdateView();
        }

        public void CloseApp()
        {
            application.Shutdown();
        }

        public void GoBack()
        {
            if (contentTrace.Count == 0)
                return;

            ChangeWindowContent(contentTrace[contentTrace.Count - 1]);
            contentTrace.RemoveAt(contentTrace.Count - 1);
        }

        #endregion
    }
}
