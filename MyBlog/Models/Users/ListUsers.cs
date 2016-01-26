using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using BLL.Users;

namespace MyBlog.Models.Users
{
    public class ListUsers
    {
        public List<User> AllUsers { get; set; }
    }
}