using System;
using System.ComponentModel;
using HeynsLibrary.Navigation;
using System.Collections.Generic;

namespace HeynsLibrary.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public INavigationWorkflow NavigationWorkflow { get; set; }

        public virtual void Navigate(Uri uri)
        {
            NavigationWorkflow.NavigateTo(uri.AbsolutePath);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion INotifyPropertyChanged Members
    }
}