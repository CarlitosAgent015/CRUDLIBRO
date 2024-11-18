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
    public class LibrosautoresController : Controller
    {
        private readonly MiDbContext _context;

        public LibrosautoresController(MiDbContext context)
        {
            _context = context;
        }

        // GET: Librosautores
        public async Task<IActionResult> Index(string buscar, string filtro)
        {
            var libroautores = from LibrosAutor in _context.LibrosAutors select LibrosAutor;

            if (!String.IsNullOrEmpty(buscar))
            {
                libroautores = libroautores.Where(s => s.Idautor!.Contains(buscar));
            }
            
            ViewData["FiltroIdAutor"] = String.IsNullOrEmpty(filtro) ? "IdAutorDescendente" : "";
            ViewData["FiltroIsbn"] = filtro == "IsbnAscendente" ? "IsbnDescendente" : "IsbnAscendente";
            switch (filtro)
            {
                case "IdAutorDescendente":
                    libroautores = libroautores.OrderByDescending(libroautor => libroautor.Idautor);
                    break;
                case "IsbnDescendente":
                    libroautores = libroautores.OrderByDescending(libroautor => libroautor.Isbn);
                    break;
                default:
                    libroautores = libroautores.OrderByDescending(libroautor => libroautor.Idautor);
                    break;
            }
            
            var miDbContext = _context.LibrosAutors.Include(l => l.IdautorNavigation).Include(l => l.IsbnNavigation);
            return View(await miDbContext.ToListAsync());
        }

        // GET: Librosautores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librosAutor = await _context.LibrosAutors
                .Include(l => l.IdautorNavigation)
                .Include(l => l.IsbnNavigation)
                .FirstOrDefaultAsync(m => m.Idlibroautor == id);
            if (librosAutor == null)
            {
                return NotFound();
            }

            return View(librosAutor);
        }

        // GET: Librosautores/Create
        public IActionResult Create()
        {
            ViewData["Idautor"] = new SelectList(_context.Autors, "Idautor", "Idautor");
            ViewData["Isbn"] = new SelectList(_context.Libros, "Isbn", "Isbn");
            return View();
        }

        // POST: Librosautores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idlibroautor,Idautor,Isbn")] LibrosAutor librosAutor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(librosAutor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idautor"] = new SelectList(_context.Autors, "Idautor", "Idautor", librosAutor.Idautor);
            ViewData["Isbn"] = new SelectList(_context.Libros, "Isbn", "Isbn", librosAutor.Isbn);
            return View(librosAutor);
        }

        // GET: Librosautores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librosAutor = await _context.LibrosAutors.FindAsync(id);
            if (librosAutor == null)
            {
                return NotFound();
            }
            ViewData["Idautor"] = new SelectList(_context.Autors, "Idautor", "Idautor", librosAutor.Idautor);
            ViewData["Isbn"] = new SelectList(_context.Libros, "Isbn", "Isbn", librosAutor.Isbn);
            return View(librosAutor);
        }

        // POST: Librosautores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idlibroautor,Idautor,Isbn")] LibrosAutor librosAutor)
        {
            if (id != librosAutor.Idlibroautor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(librosAutor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibrosAutorExists(librosAutor.Idlibroautor))
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
            ViewData["Idautor"] = new SelectList(_context.Autors, "Idautor", "Idautor", librosAutor.Idautor);
            ViewData["Isbn"] = new SelectList(_context.Libros, "Isbn", "Isbn", librosAutor.Isbn);
            return View(librosAutor);
        }

        private bool LibrosAutorExists(int Idlibroautor)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        // GET: Librosautores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librosAutor = await _context.LibrosAutors
                .Include(l => l.IdautorNavigation)
                .Include(l => l.IsbnNavigation)
                .FirstOrDefaultAsync(m => m.Idlibroautor == id);
            if (librosAutor == null)
            {
                return NotFound();
            }

            return View(librosAutor);
        }

        // POST: Librosautores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var librosAutor = await _context.LibrosAutors.FindAsync(id);
            if (librosAutor != null)
            {
             // Si no se encuentra el autor, devolver un mensaje de error
            return Json(new { success = false, errorMessage = "El libro autor no fue encontrado." });
            }

            bool tieneRelacion = await _context.LibrosAutors.AnyAsync(la => la.Idlibroautor == id);

            if (tieneRelacion)
            {
            // Si está relacionado, no se puede eliminar
            return Json(new { success = false, errorMessage = "No se puede eliminar el libro autor porque está relacionado con otros registros." });
            }

            _context.LibrosAutors.Remove(librosAutor);
            await _context.SaveChangesAsync();
        // Si la eliminación es exitosa, devolver un mensaje de éxito
        return Json(new { success = true, message = "Libro Autor eliminado correctamente." });
        }
    }
}
