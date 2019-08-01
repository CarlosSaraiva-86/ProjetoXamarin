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
	public partial class RetiradaItensPendentesView : ContentPage
	{
		public RetiradaItensPendentesView (string titulo)
		{
			InitializeComponent ();
            this.BindingContext = new RetiradaItensPendentesViewModel(titulo);
        }
	}
}