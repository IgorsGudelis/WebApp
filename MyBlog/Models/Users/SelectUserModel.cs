using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlog.Models.Users
{
    public class SelectUserModel
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "Enter FirstName please")]
        //[RegularExpression("[a-z].+", ErrorMessage = "Only characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter LastName please")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter Email please")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Incorrect email")]
        public string Email { get; set; }

        //public HttpPostedFileBase File { get; set; }
        public SelectUserModel() { }

        public SelectUserModel(int id, string firstName, string lastName, string eMail)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Email = eMail;
        }
    }
}