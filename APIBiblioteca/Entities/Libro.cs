using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIBiblioteca.Entities
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public int AutorId { get; set; }

        public Autor Autor { get; set; }
    }


    public class LibroDTO
    {
        public string Titulo { get; set; }
        public int AutorId { get; set; }
    }
}
