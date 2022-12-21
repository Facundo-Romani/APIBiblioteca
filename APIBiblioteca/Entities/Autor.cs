using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        
        [Required]
        public string Nombre { get; set; }
        
        public List<Libro> Libros { get; set; }
    }

    public class AutorDTO
    {
       
        [Required]
        public string Nombre { get; set; }

    }
}
