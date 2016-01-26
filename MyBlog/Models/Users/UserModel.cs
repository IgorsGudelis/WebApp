using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlog.Models.Users
{
    public class UserModel
    {
        public SelectUserModel SelectUserModel { get; set; }
        public PageInfo PageInfo { get; set; }
        public ListUsers ListUsers { get; set; }  
 
        public UserModel()
        {
            SelectUserModel = new SelectUserModel();
            PageInfo = new PageInfo();
            ListUsers = new ListUsers();
        }
    }
}