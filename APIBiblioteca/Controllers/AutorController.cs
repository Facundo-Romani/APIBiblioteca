using APIBiblioteca.Context;
using APIBiblioteca.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace APIBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public AutorController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Autor>> GetAll()
        {
            return _context.Autor.Include(a => a.Libros).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Autor> GetId(int id)
        {
            var autorId = _context.Autor.Select(x => x.Id == id).FirstOrDefault();

            return Ok(autorId);
        }

        [HttpPost]
        public ActionResult Post([FromBody] AutorDTO autorDTO)
        {
            var nuevoAutor = new Autor()
            {
                Nombre = autorDTO.Nombre
            };

            _context.Autor.Add(nuevoAutor);
            _context.SaveChanges();

            return Ok(nuevoAutor);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] AutorDTO autorDTO, int id)
        {
            var autor = _context.Autor.Where(x => x.Id == id).Select(x => x).FirstOrDefault();

            if (id != 0)
            {
                autor.Nombre = autorDTO.Nombre;

                _context.Autor.Update(autor);
                _context.SaveChanges();

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var autor = _context.Autor.Where(x => x.Id == id).Select(x => x).FirstOrDefault();

            if (id == autor.Id)
            {
                _context.Autor.Remove(autor);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
