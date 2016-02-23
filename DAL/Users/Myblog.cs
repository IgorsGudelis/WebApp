using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;

namespace DAL.Users
{
    public class Myblog: DbContext
    {
        public Myblog() : base("DefaultConnection") { }

        public DbSet<UserAuthDto> UsersAuth { get; set; }
    }
}
