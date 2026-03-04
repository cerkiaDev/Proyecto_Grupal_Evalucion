using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Grupal.Data;
using Proyecto_Grupal.Models;

namespace Proyecto_Grupal.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly AppDbContext _context;

        public EmpleadosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var list = await _context.Empleados.AsNoTracking().ToListAsync();
            return View(list);
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var emp = await _context.Empleados.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empleado empleado)
        {
            if (!ModelState.IsValid) return View(empleado);

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var emp = await _context.Empleados.FindAsync(id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        // POST: Empleados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Empleado model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Empleados/ToggleActive/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var emp = await _context.Empleados.FindAsync(id);
            if (emp == null) return NotFound();
            emp.IsActive = !emp.IsActive;
            _context.Update(emp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
