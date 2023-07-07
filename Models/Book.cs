using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaVirtual.Models
{
    public enum CDU { Generalidades, filosofia, Religión, Ciencias_sociales, Vacante, Ciencia_pura, Ciencia_aplicada, Arte,_Deporte, Geografia,_historia,_biografias }

    [Table("Book", Schema = "dbo")]
    public class Book
    {
        [Key]
        public int Id_book { get; set; }
        public string Title { get; set; }

        [ForeignKey("Author")]
        public int Author_id { get; set; }
        public string Editorial { get; set; }
        public CDU Type { get; set; }
        public int QuantityAvailable { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
        public virtual Author Author { get; set; }
    }
}
