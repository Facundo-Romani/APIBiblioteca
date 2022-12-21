﻿using APIBiblioteca.Context;
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
        public ActionResult<Libro> GetId([FromBody] LibroDTO libroDTO, int id)
        {
            var libroId = _context.Libro.Select(l => l.Id == id).FirstOrDefault();

            return Ok(libroDTO);
        }

        [HttpPost]
        public ActionResult AddLibro([FromBody] LibroDTO libroDTO)
        {
            try
            {   
                if(libroDTO == null)
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
                    return BadRequest("is not null");
                }     
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
