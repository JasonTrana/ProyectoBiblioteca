using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BibliotecaVirtual.Data
{
    public class BibliotecaContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BibliotecaContext() : base("name=BibliotecaContext")
        {
        }

        public System.Data.Entity.DbSet<BibliotecaVirtual.Models.Author> Authors { get; set; }
        public System.Data.Entity.DbSet<BibliotecaVirtual.Models.Book> Books { get; set; }
        public System.Data.Entity.DbSet<BibliotecaVirtual.Models.Loan> Loans { get; set; }
        public System.Data.Entity.DbSet<BibliotecaVirtual.Models.User> Users { get; set; }
    }
}
