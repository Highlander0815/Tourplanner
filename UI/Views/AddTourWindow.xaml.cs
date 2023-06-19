﻿using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UI.ViewModels;

namespace UI.Views
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class AddTourWindow : Window
    {
        public AddTourWindow()
        {
            InitializeComponent();
            
            //mainWindow.AddEvent += (o, e) => this.DialogResult = true;
            //mainWindow.CancelEvent += (o, e) => this.DialogResult = false;
        }

        public void Init(Action<Tour> speichern)
        {
            var mainWindow = this.DataContext as AddTourViewModel;
            mainWindow.AddEvent += speichern;
            mainWindow.AddEvent += (o) => this.DialogResult = true;
            mainWindow.CancelEvent += (o, e) => this.DialogResult = false;
        }
    }
}
