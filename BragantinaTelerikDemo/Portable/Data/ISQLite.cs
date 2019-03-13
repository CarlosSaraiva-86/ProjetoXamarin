using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BragantinaTelerikDemo.Portable.Data
{
    public interface ISQLite
    {
        SQLiteConnection PegarConexao();
    }
}
