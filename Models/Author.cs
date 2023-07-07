using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BibliotecaVirtual.Models
{
    [Table("Author",Schema = "dbo")]
    public class Author
    {
        
        [Key]
        public int Id_author { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}