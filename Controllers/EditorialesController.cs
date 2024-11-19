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
    public class EditorialesController : Controller
    {
        private readonly MiDbContext _context;

        public EditorialesController(MiDbContext context)
        {
            _context = context;
        }

        // GET: Editoriales
        public async Task<IActionResult> Index(string buscar, string filtro)
        {
            var editoriales = from Editoriale in _context.Editoriales select Editoriale;

            if (!String.IsNullOrEmpty(buscar))
            {
                editoriales = editoriales.Where(s => s.Nombres!.Contains(buscar));
            }
            
            ViewData["FiltroNombres"] = String.IsNullOrEmpty(filtro) ? "NombresDescendente" : "";
            ViewData["FiltroNit"] = filtro == "NitAscendente" ? "NitDescendente" : "NitAscendente";
            switch (filtro)
            {
                case "NombresDescendente":
                    editoriales = editoriales.OrderByDescending(editoriale => editoriale.Nombres);
                    break;
                case "NitDescendente":
                    editoriales = editoriales.OrderByDescending(editoriale => editoriale.Nombres);
                    break;
                default:
                    editoriales = editoriales.OrderByDescending(editoriale => editoriale.Nit);
                    break;
            }
            return View(await _context.Editoriales.ToListAsync());
        }

        // GET: Editoriales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editoriale = await _context.Editoriales
                .FirstOrDefaultAsync(m => m.Nit == id);
            if (editoriale == null)
            {
                return NotFound();
            }

            return View(editoriale);
        }

        // GET: Editoriales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Editoriales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nit,Nombres,Telefono,Direccion,Email,Sitioweb")] Editoriale editoriale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(editoriale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(editoriale);
        }

        // GET: Editoriales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editoriale = await _context.Editoriales.FindAsync(id);
            if (editoriale == null)
            {
                return NotFound();
            }
            return View(editoriale);
        }

        
        // POST: Editoriales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nit,Nombres,Telefono,Direccion,Email,Sitioweb")] Editoriale editoriale)
        {
            if (id != editoriale.Nit)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editoriale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorialeExists(editoriale.Nit))
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
            return View(editoriale);
        }

        // GET: Editoriales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editoriale = await _context.Editoriales
                .FirstOrDefaultAsync(m => m.Nit == id);
            if (editoriale == null)
            {
                return NotFound();
            }

            return View(editoriale);
        }

        // POST: Editoriales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var editoriale = await _context.Editoriales.FindAsync(id);
            if (editoriale != null)
            {
             // Si no se encuentra el autor, devolver un mensaje de error
            return Json(new { success = false, errorMessage = "La editorial no fue encontrado." });
            }

            bool tieneRelacion = await _context.Libros.AnyAsync(la => la.NitEditorial == id);

            if (tieneRelacion)
            {
                // Si está relacionado, no se puede eliminar
                return Json(new { success = false, errorMessage = "No se puede eliminar la editorial porque está relacionado con otros registros." });
            }

            _context.Editoriales.Remove(editoriale);
            await _context.SaveChangesAsync();
        // Si la eliminación es exitosa, devolver un mensaje de éxito
        return Json(new { success = true, message = "Editorial eliminado correctamente." });
        }

        private bool EditorialeExists(int id)
        {
            return _context.Editoriales.Any(e => e.Nit == id);
        }
    }
}
