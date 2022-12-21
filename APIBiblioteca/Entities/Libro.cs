using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIBiblioteca.Entities
{
    public class Libro
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public int AutorId { get; set; }
        
        [ForeignKey("AutorId")]
        public Autor Autor { get; set; }
    }
}
