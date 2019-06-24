﻿using BragantinaTelerikDemo.Portable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BragantinaTelerikDemo.Portable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilUsuario : ContentPage
    {
        public PerfilUsuario()
        {
            InitializeComponent();
            this.BindingContext = new PerfilUsuarioViewModel();
        }
    }
}