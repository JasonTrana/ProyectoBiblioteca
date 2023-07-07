using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaVirtual.Models
{
    [Table("Loan", Schema = "dbo")]
    public class Loan
    {
        [Key]
        public int Id_loan { get; set; }

        [ForeignKey("Book")]
        public int Book_id { get; set; }

        [ForeignKey("User")]
        public int User_id { get; set; }
        public DateTime LoanDate { get; set; } = default;
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}