using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarDelivery.Data;
using CarDelivery.Models;

namespace CarDelivery.Controllers
{
    /// <summary>
    /// Контроллер для управления комплектациями.
    /// </summary>
    public class ComplectationsController : Controller
    {
        private readonly CarDeliveryContext _context;

        /// <summary>
        /// Конструктор контроллера комплектаций.
        /// </summary>
        public ComplectationsController(CarDeliveryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отображает список комплектаций.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Complectations.ToListAsync());
        }

        /// <summary>
        /// Отображает детали комплектации.
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complectations = await _context.Complectations
                .FirstOrDefaultAsync(m => m.Complectationid == id);
            if (complectations == null)
            {
                return NotFound();
            }

            return View(complectations);
        }

        /// <summary>
        /// Отображает форму создания комплектации.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Создает новую комплектацию.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Complectationid,Name,Equipment,Engine")] Complectations complectations)
        {
            // Ручная проверка данных
            if (string.IsNullOrWhiteSpace(complectations.Name))
                ModelState.AddModelError(nameof(complectations.Name), "Название обязательно для заполнения.");
            if (string.IsNullOrWhiteSpace(complectations.Equipment))
                ModelState.AddModelError(nameof(complectations.Equipment), "Оборудование обязательно для заполнения.");
            if (string.IsNullOrWhiteSpace(complectations.Engine))
                ModelState.AddModelError(nameof(complectations.Engine), "Двигатель обязателен для заполнения.");

            if (ModelState.IsValid)
            {
                _context.Add(complectations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complectations);
        }

        /// <summary>
        /// Отображает форму редактирования комплектации.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complectations = await _context.Complectations.FindAsync(id);
            if (complectations == null)
            {
                return NotFound();
            }
            return View(complectations);
        }

        /// <summary>
        /// Редактирует комплектацию.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Complectationid,Name,Equipment,Engine")] Complectations complectations)
        {
            if (id != complectations.Complectationid)
                return NotFound();

            // Ручная проверка данных
            if (string.IsNullOrWhiteSpace(complectations.Name))
                ModelState.AddModelError(nameof(complectations.Name), "Название обязательно для заполнения.");
            if (string.IsNullOrWhiteSpace(complectations.Equipment))
                ModelState.AddModelError(nameof(complectations.Equipment), "Оборудование обязательно для заполнения.");
            if (string.IsNullOrWhiteSpace(complectations.Engine))
                ModelState.AddModelError(nameof(complectations.Engine), "Двигатель обязателен для заполнения.");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complectations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplectationsExists(complectations.Complectationid))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(complectations);
        }

        /// <summary>
        /// Отображает форму удаления комплектации.
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complectations = await _context.Complectations
                .FirstOrDefaultAsync(m => m.Complectationid == id);
            if (complectations == null)
            {
                return NotFound();
            }

            return View(complectations);
        }

        /// <summary>
        /// Удаляет комплектацию.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complectations = await _context.Complectations.FindAsync(id);
            if (complectations != null)
            {
                _context.Complectations.Remove(complectations);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверяет, существует ли комплектация.
        /// </summary>
        private bool ComplectationsExists(int id)
        {
            return _context.Complectations.Any(e => e.Complectationid == id);
        }
    }
}
