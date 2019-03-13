﻿using BragantinaTelerikDemo.Portable.Models;
using BragantinaTelerikDemo.Portable.ViewModels;
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
    public partial class CadastroUsuarioView : ContentPage
    {
        public CadastroUsuarioViewModel ViewModel {get; set;}
		public CadastroUsuarioView (UsuarioApi usuario)
		{
			InitializeComponent ();
            this.ViewModel = new CadastroUsuarioViewModel(usuario);
            this.BindingContext = this.ViewModel;
        }
	}
}