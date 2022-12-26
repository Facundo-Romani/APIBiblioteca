using APIBiblioteca.Context;
using APIBiblioteca.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public LibroController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Libro>> GetAll()
        {
            return _context.Libro.Include(l => l.Autor).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Libro> GetId(int id)
        {
            var libroId = _context.Libro.Select(l => l.Id == id).FirstOrDefault();

            return Ok(libroId);
        }

        [HttpPost]
        public ActionResult AddLibro([FromBody] LibroDTO libroDTO)
        {

            if (libroDTO != null)
            {
                var libroNuevo = new Libro()
                {
                    Titulo = libroDTO.Titulo,
                    AutorId = libroDTO.AutorId,
                };

                _context.Libro.Add(libroNuevo);

                _context.SaveChanges();

                return Ok(libroNuevo);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(LibroDTO libroDTO, int id)
        {
            var libroSeleccionado = _context.Libro.Where(x => x.Id == id).Select(x => x).FirstOrDefault();

            if (id != 0)
            {
                libroSeleccionado.Titulo = libroDTO.Titulo;

                _context.Libro.Update(libroSeleccionado);
                _context.SaveChanges();

                return Ok();
            }
            else
            {
                return BadRequest("Libro no Existe");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var libroSeleccionado = _context.Libro.Where(x => x.Id == id).Select(x => x).FirstOrDefault();

            if (id == libroSeleccionado.Id)
            {
                _context.Remove(libroSeleccionado);
                _context.SaveChanges();

                return Ok();
            }
            else
            {
                return BadRequest("No existe");
            }
        }
    }
}
