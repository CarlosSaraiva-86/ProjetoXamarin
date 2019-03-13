using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BragantinaTelerikDemo.Android;
using BragantinaTelerikDemo.Portable.Data;
using SQLite;
using Environment = Android.OS.Environment;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_android))]
namespace BragantinaTelerikDemo.Android
{
    class SQLite_android : ISQLite
    {
        private const string nomeArquivoDB = "Evolucao.db3";

        public SQLiteConnection PegarConexao()
        {
            var caminhoDB = Path.Combine(Environment.ExternalStorageDirectory.Path, nomeArquivoDB);
            return new SQLiteConnection(caminhoDB);
        }
    }
}