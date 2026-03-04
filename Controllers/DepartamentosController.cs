using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Grupal.Data;
using Proyecto_Grupal.Models;

namespace Proyecto_Grupal.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly AppDbContext _context;

        public DepartamentosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Departamentos
        public async Task<IActionResult> Index()
        {
            var list = await _context.Departamentos.AsNoTracking().ToListAsync();
            return View(list);
        }

        // GET: Departamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departamentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Departamento dept)
        {
            if (!ModelState.IsValid) return View(dept);

            _context.Departamentos.Add(dept);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Departamentos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var dept = await _context.Departamentos.FindAsync(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        // POST: Departamentos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Departamento model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Departamentos/ToggleActive/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var dept = await _context.Departamentos.FindAsync(id);
            if (dept == null) return NotFound();
            dept.IsActive = !dept.IsActive;
            _context.Update(dept);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
