using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Model;
using Controll;

namespace Prototyp
{
    public class BasicWindow : Window, IView
    {
        #region Fields

        public Window ParentWindow { get; set; }

        public DataModel Data { get; set; }
        public SettingsModel Settings { get; set; }

        public WindowController WindowCtrl { get; set; }
        
        #endregion

        #region Constructor

        public BasicWindow()
        {
            Data = DataModel.GetInstance();
            Settings = SettingsModel.GetInstance();

            WindowCtrl = WindowController.GetInstance();

            WindowCtrl.View = this;

            Closing += window_closing;
        }
        
        #endregion

        #region Events

        private void window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowCtrl.CloseApp();
        }
        
        #endregion

        #region IView

        public void UpdateView()
        {

        }

        public void UpdateView(String s)
        {
            Title = s;
        }

        public virtual void OpenScreen(EScreen screen)
        {
        }

        public IView GetParent()
        {
            return ParentWindow as IView;
        }
        
        #endregion
    }
}
