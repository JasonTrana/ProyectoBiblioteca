using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaVirtual.Models
{
    public enum Rol { Usuario, Bibliotecario }

    [Table("User", Schema = "dbo")]
    public class User
    {
        [Key]
        public int Id_user { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Rol Roles { get; set; } = Rol.Usuario;

        [NotMapped]
        public string ConfirmPassword { get; set; }

        //Relationships
        public virtual ICollection<Loan> Prestamos { get; set; }
    }
}   