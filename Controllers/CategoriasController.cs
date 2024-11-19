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
    public class CategoriasController : Controller
    {
        private readonly MiDbContext _context;

        public CategoriasController(MiDbContext context)
        {
            _context = context;
        }

        // GET: Categorias
        public async Task<IActionResult> Index(string buscar, string filtro)
        {
            var categorias = from Categoria in _context.Categorias select Categoria;

            if (!String.IsNullOrEmpty(buscar))
            {
                categorias = categorias.Where(s => s.CodigoCategoria.ToString()!.Contains(buscar));
            }
            
            ViewData["FiltroCodigoCategoria"] = String.IsNullOrEmpty(filtro) ? "CodigoCategoriaDescendente" : "";
            ViewData["FiltroNombre"] = filtro == "NombreAscendente" ? "NombreDescendente" : "NombreAscendente";
            switch (filtro)
            {
                case "CodigoCategoriaDescendente":
                    categorias = categorias.OrderByDescending(categoria => categoria.CodigoCategoria);
                    break;
                case "NombreDescendente":
                    categorias = categorias.OrderByDescending(categoria => categoria.Nombre);
                    break;
                default:
                    categorias = categorias.OrderByDescending(categoria => categoria.CodigoCategoria);
                    break;
            }
            return View(await _context.Categorias.ToListAsync());
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.CodigoCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoCategoria,Nombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodigoCategoria,Nombre")] Categoria categoria)
        {
            if (id != categoria.CodigoCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.CodigoCategoria))
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
            return View(categoria);
        }

        private bool CategoriaExists(int CodigoCategoria)
        {
            throw new NotImplementedException();
        }
        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.CodigoCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
        var categoria = await _context.Categorias.FindAsync(id);

        if (categoria == null)
        {
            return Json(new { success = false, errorMessage = "La categoría no fue encontrada." });
        }

        bool tieneRelacion = await _context.Libros.AnyAsync(l => l.CodigoCategoria == id);

        if (tieneRelacion)
        {
            return Json(new { success = false, errorMessage = "No se puede eliminar la categoría porque está relacionada con otros registros." });
        }

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return Json(new { success = true, message = "Categoría eliminada correctamente." });
        }
    }
}