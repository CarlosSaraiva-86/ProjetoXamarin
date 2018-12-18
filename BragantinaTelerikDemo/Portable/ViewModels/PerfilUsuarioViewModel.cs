using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    class PerfilUsuarioViewModel
    {
        public PerfilUsuarioViewModel()
        {
            //this.Avatar = "Border_User.png";
            this.Avatar = "https://instagram.fbjp1-1.fna.fbcdn.net/vp/39613770b926f44a56c11558f656ada2/5CD749FC/t51.2885-19/11850309_1674349799447611_206178162_a.jpg?_nc_ht=instagram.fbjp1-1.fna.fbcdn.net";
            //this.Person1Avatar = "Border_Person_1.png";
            //this.Person2Avatar = "Border_Person_2.png";
            //this.Person3Avatar = "Border_Person_3.png";
            //this.Person4Avatar = "Border_Person_4.png";
        }

        //public List<Comment> Comments { get; private set; }

        public string Avatar { get; set; }

        //public string Person1Avatar { get; set; }
        //public string Person2Avatar { get; set; }
        //public string Person3Avatar { get; set; }
        //public string Person4Avatar { get; set; }

            
    }

    //public class Comment
    //{
    //    public string UserName { get; set; }
    //    public string UserAvatar { get; set; }
    //    public string Text { get; set; }
    //}

}
