using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Grupal.Data;
using Proyecto_Grupal.Models;

namespace Proyecto_Grupal.Controllers
{
    public class AsignacionesController : Controller
    {
        private readonly AppDbContext _context;

        public AsignacionesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Asignaciones
        public async Task<IActionResult> Index()
        {
            var list = await _context.EmpleadoDepartamentos
                .Include(ed => ed.Empleado)
                .Include(ed => ed.Departamento)
                .AsNoTracking()
                .ToListAsync();
            return View(list);
        }

        // GET: Asignaciones/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.EmpleadoDepartamentos
                .Include(ed => ed.Empleado)
                .Include(ed => ed.Departamento)
                .AsNoTracking()
                .FirstOrDefaultAsync(ed => ed.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // GET: Asignaciones/Create
        public async Task<IActionResult> Create()
        {
            await PopulateSelectLists();
            return View();
        }

        // POST: Asignaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmpleadoDepartamento model)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .Where(m => !string.IsNullOrEmpty(m)));
                if (!string.IsNullOrEmpty(errors)) ModelState.AddModelError(string.Empty, "Errores de validación: " + errors);

                await PopulateSelectLists();
                return View(model);
            }

            var overlapExists = await _context.EmpleadoDepartamentos
                .Where(a => a.EmpleadoId == model.EmpleadoId && a.IsActive)
                .Where(a => (a.ToDate == null || model.FromDate <= a.ToDate) && (model.ToDate == null || a.FromDate <= model.ToDate))
                .AnyAsync();

            if (overlapExists)
            {
                ModelState.AddModelError(string.Empty, "La vigencia se solapa con otra asignación del mismo empleado.");
                await PopulateSelectLists();
                return View(model);
            }

            _context.EmpleadoDepartamentos.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error guardando en la base de datos: " + ex.Message);
                await PopulateSelectLists();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Asignaciones/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.EmpleadoDepartamentos.FindAsync(id);
            if (item == null) return NotFound();
            await PopulateSelectLists();
            return View(item);
        }

        // POST: Asignaciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmpleadoDepartamento model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .Where(m => !string.IsNullOrEmpty(m)));
                if (!string.IsNullOrEmpty(errors)) ModelState.AddModelError(string.Empty, "Errores de validación: " + errors);

                await PopulateSelectLists();
                return View(model);
            }

            var overlapExists = await _context.EmpleadoDepartamentos
                .Where(a => a.EmpleadoId == model.EmpleadoId && a.IsActive && a.Id != model.Id)
                .Where(a => (a.ToDate == null || model.FromDate <= a.ToDate) && (model.ToDate == null || a.FromDate <= model.ToDate))
                .AnyAsync();

            if (overlapExists)
            {
                ModelState.AddModelError(string.Empty, "La vigencia se solapa con otra asignación del mismo empleado.");
                await PopulateSelectLists();
                return View(model);
            }

            _context.Update(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error guardando en la base de datos: " + ex.Message);
                await PopulateSelectLists();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Asignaciones/ToggleActive/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var item = await _context.EmpleadoDepartamentos.FindAsync(id);
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
