﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UI.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChangedEvent([CallerMemberName]string propertyName = "")
        {
            ValidatePropertyName(propertyName);
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        protected void ValidatePropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null) {
                throw new ArgumentException("Invalid property name: " + propertyName);
            }
        }
    }
}
