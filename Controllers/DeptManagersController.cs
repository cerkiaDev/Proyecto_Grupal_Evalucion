using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Grupal.Data;
using Proyecto_Grupal.Models;

namespace Proyecto_Grupal.Controllers
{
    public class DeptManagersController : Controller
    {
        private readonly AppDbContext _context;

        public DeptManagersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DeptManagers
        public async Task<IActionResult> Index()
        {
            var list = await _context.DeptManagers
                .Include(d => d.Empleado)
                .Include(d => d.Departamento)
                .AsNoTracking()
                .ToListAsync();
            return View(list);
        }

        // GET: DeptManagers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.DeptManagers
                .Include(d => d.Empleado)
                .Include(d => d.Departamento)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // GET: DeptManagers/Create
        public async Task<IActionResult> Create()
        {
            await PopulateSelectLists();
            return View();
        }

        // POST: DeptManagers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeptManager model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateSelectLists();
                return View(model);
            }

            var overlapExists = await _context.DeptManagers
                .Where(m => m.DepartamentoId == model.DepartamentoId && m.IsActive)
                .Where(m => (m.ToDate == null || model.FromDate <= m.ToDate) && (model.ToDate == null || m.FromDate <= model.ToDate))
                .AnyAsync();

            if (overlapExists)
            {
                ModelState.AddModelError(string.Empty, "La vigencia se solapa con otro manager activo para este departamento.");
                await PopulateSelectLists();
                return View(model);
            }

            _context.DeptManagers.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: DeptManagers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.DeptManagers.FindAsync(id);
            if (item == null) return NotFound();
            await PopulateSelectLists();
            return View(item);
        }

        // POST: DeptManagers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DeptManager model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                await PopulateSelectLists();
                return View(model);
            }

            var overlapExists = await _context.DeptManagers
                .Where(m => m.DepartamentoId == model.DepartamentoId && m.IsActive && m.Id != model.Id)
                .Where(m => (m.ToDate == null || model.FromDate <= m.ToDate) && (model.ToDate == null || m.FromDate <= model.ToDate))
                .AnyAsync();

            if (overlapExists)
            {
                ModelState.AddModelError(string.Empty, "La vigencia se solapa con otro manager activo para este departamento.");
                await PopulateSelectLists();
                return View(model);
            }

            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: DeptManagers/ToggleActive/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var item = await _context.DeptManagers.FindAsync(id);
            if (item == null) return NotFound();
            item.IsActive = !item.IsActive;
            _context.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateSelectLists()
        {
            var empleados = await _context.Empleados
                .Where(e => e.IsActive)
                .Select(e => new { e.Id, FullName = e.FirstName + " " + e.LastName + " (" + e.EmpNo + ")" })
                .ToListAsync();
            ViewBag.Empleados = new SelectList(empleados, "Id", "FullName");

            var departamentos = await _context.Departamentos
                .Where(d => d.IsActive)
                .Select(d => new { d.Id, d.DeptName })
                .ToListAsync();
            ViewBag.Departamentos = new SelectList(departamentos, "Id", "DeptName");
        }
    }
}
