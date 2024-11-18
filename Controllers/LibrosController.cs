using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDROLES.Models;

namespace CRUDROLES.Controllers
{
    public class LibrosController : Controller
    {
        private readonly MiDbContext _context;

        public LibrosController(MiDbContext context)
        {
            _context = context;
        }

        // GET: Libros
        public async Task<IActionResult> Index(string buscar, string filtro)
        {
            var libros = from Libro in _context.Libros select Libro;

            if (!String.IsNullOrEmpty(buscar))
            {
                libros = libros.Where(s => s.Isbn!.Contains(buscar));
            }
            
            ViewData["FiltroIsbn"] = String.IsNullOrEmpty(filtro) ? "IsbnDescendente" : "";
            ViewData["FiltroTitulo"] = filtro == "TituloAscendente" ? "TituloDescendente" : "TituloAscendente";
            switch (filtro)
            {
                case "IsbnDescendente":
                    libros = libros.OrderByDescending(libro => libro.Isbn);
                    break;
                case "TituloDescendente":
                    libros= libros.OrderByDescending(libro => libro.Titulo);
                    break;
                default:
                    libros = libros.OrderByDescending(libro => libro.Isbn);
                    break;
            }
            
            var miDbContext = _context.Libros.Include(l => l.CodigoCategoriaNavigation).Include(l => l.NitEditorialNavigation);
            return View(await miDbContext.ToListAsync());
        }

        // GET: Libros/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.CodigoCategoriaNavigation)
                .Include(l => l.NitEditorialNavigation)
                .FirstOrDefaultAsync(m => m.Isbn == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libros/Create
        public IActionResult Create()
        {
            ViewData["CodigoCategoria"] = new SelectList(_context.Categorias, "CodigoCategoria", "CodigoCategoria");
            ViewData["NitEditorial"] = new SelectList(_context.Editoriales, "Nit", "Nit");
            return View();
        }

        // POST: Libros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Isbn,Titulo,Descripcion,NombreAutor,Publicacion,FechaRegistro,CodigoCategoria,NitEditorial")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoCategoria"] = new SelectList(_context.Categorias, "CodigoCategoria", "CodigoCategoria", libro.CodigoCategoria);
            ViewData["NitEditorial"] = new SelectList(_context.Editoriales, "Nit", "Nit", libro.NitEditorial);
            return View(libro);
        }

        // GET: Libros/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            ViewData["CodigoCategoria"] = new SelectList(_context.Categorias, "CodigoCategoria", "CodigoCategoria", libro.CodigoCategoria);
            ViewData["NitEditorial"] = new SelectList(_context.Editoriales, "Nit", "Nit", libro.NitEditorial);
            return View(libro);
        }

        // POST: Libros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Isbn,Titulo,Descripcion,NombreAutor,Publicacion,FechaRegistro,CodigoCategoria,NitEditorial")] Libro libro)
        {
            if (id != libro.Isbn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.Isbn))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoCategoria"] = new SelectList(_context.Categorias, "CodigoCategoria", "CodigoCategoria", libro.CodigoCategoria);
            ViewData["NitEditorial"] = new SelectList(_context.Editoriales, "Nit", "Nit", libro.NitEditorial);
            return View(libro);
        }


        private bool LibroExists(string Isbn)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.CodigoCategoriaNavigation)
                .Include(l => l.NitEditorialNavigation)
                .FirstOrDefaultAsync(m => m.Isbn == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
             // Si no se encuentra el autor, devolver un mensaje de error
            return Json(new { success = false, errorMessage = "El libro no fue encontrado." });
            }

            bool tieneRelacion = await _context.LibrosAutors.AnyAsync(la => la.Isbn == id);

            if( tieneRelacion)
            {
            // Si está relacionado, no se puede eliminar
            return Json(new { success = false, errorMessage = "No se puede eliminar el libro porque está relacionado con otros registros." });
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
            // Si la eliminación es exitosa, devolver un mensaje de éxito
            return Json(new { success = true, message = "Libro eliminado correctamente." });
        }
    }
}
