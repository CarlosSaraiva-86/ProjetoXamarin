using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    class PerfilUsuarioViewModel
    {
        public PerfilUsuarioViewModel()
        {
            //this.Avatar = "Border_User.png";
            this.Avatar = "https://scontent.fbjp1-1.fna.fbcdn.net/v/t1.0-9/33923600_1395086363925833_547282230553083904_n.jpg?_nc_cat=111&_nc_ht=scontent.fbjp1-1.fna&oh=e812948c16474113a5d63428adaa65eb&oe=5CA7269C";
            //this.Person1Avatar = "Border_Person_1.png";
            //this.Person2Avatar = "Border_Person_2.png";
            //this.Person3Avatar = "Border_Person_3.png";
            //this.Person4Avatar = "Border_Person_4.png";
        }

        //public List<Comment> Comments { get; private set; }

        public string Avatar { get; set; }

        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Device.OpenUri(new System.Uri(url));
        });

        public ICommand ClickCommand1 => new Command<string>((url) =>
        {
            Device.OpenUri(new System.Uri(url));
        });

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
