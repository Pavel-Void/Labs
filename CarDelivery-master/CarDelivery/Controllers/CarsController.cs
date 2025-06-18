using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarDelivery.Data;
using CarDelivery.Models;

namespace CarDelivery.Controllers
{
    /// <summary>
    /// Контроллер для управления автомобилями.
    /// </summary>
    public class CarsController : Controller
    {
        private readonly CarDeliveryContext _context;

        /// <summary>
        /// Конструктор контроллера автомобилей.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public CarsController(CarDeliveryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отображает список автомобилей.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var carDeliveryContext = _context.Cars.Include(c => c.Complectations);
            return View(await carDeliveryContext.ToListAsync());
        }

        /// <summary>
        /// Отображает детали автомобиля по идентификатору.
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cars = await _context.Cars
                .Include(c => c.Complectations)
                .FirstOrDefaultAsync(m => m.Carid == id);
            if (cars == null)
            {
                return NotFound();
            }

            return View(cars);
        }

        /// <summary>
        /// Отображает форму создания автомобиля.
        /// </summary>
        public IActionResult Create()
        {
            ViewData["Complectationid"] = new SelectList(_context.Complectations, "Complectationid", "Name");
            return View();
        }

        /// <summary>
        /// Создает новый автомобиль.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Carid,Make,Model,Complectationid,Price")] Cars cars)
        {
            // Ручная проверка данных
            if (string.IsNullOrWhiteSpace(cars.Make))
                ModelState.AddModelError(nameof(cars.Make), "Марка автомобиля обязательна для заполнения.");
            if (string.IsNullOrWhiteSpace(cars.Model))
                ModelState.AddModelError(nameof(cars.Model), "Модель автомобиля обязательна для заполнения.");
            if (cars.Price <= 0)
                ModelState.AddModelError(nameof(cars.Price), "Цена должна быть больше нуля.");

            if (ModelState.IsValid)
            {
                _context.Add(cars);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Complectationid"] = new SelectList(_context.Complectations, "Complectationid", "Name", cars.Complectationid);
            return View(cars);
        }

        /// <summary>
        /// Отображает форму редактирования автомобиля.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cars = await _context.Cars.FindAsync(id);
            if (cars == null)
            {
                return NotFound();
            }
            ViewData["Complectationid"] = new SelectList(_context.Complectations, "Complectationid", "Complectationid", cars.Complectationid);
            return View(cars);
        }

        /// <summary>
        /// Редактирует автомобиль.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Carid,Make,Model,Complectationid,Price")] Cars cars)
        {
            if (id != cars.Carid)
                return NotFound();

            // Ручная проверка данных
            if (string.IsNullOrWhiteSpace(cars.Make))
                ModelState.AddModelError(nameof(cars.Make), "Марка автомобиля обязательна для заполнения.");
            if (string.IsNullOrWhiteSpace(cars.Model))
                ModelState.AddModelError(nameof(cars.Model), "Модель автомобиля обязательна для заполнения.");
            if (cars.Price <= 0)
                ModelState.AddModelError(nameof(cars.Price), "Цена должна быть больше нуля.");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cars);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarsExists(cars.Carid))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Complectationid"] = new SelectList(_context.Complectations, "Complectationid", "Complectationid", cars.Complectationid);
            return View(cars);
        }

        /// <summary>
        /// Отображает форму удаления автомобиля.
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cars = await _context.Cars
                .Include(c => c.Complectations)
                .FirstOrDefaultAsync(m => m.Carid == id);
            if (cars == null)
            {
                return NotFound();
            }

            return View(cars);
        }

        /// <summary>
        /// Удаляет автомобиль.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cars = await _context.Cars.FindAsync(id);
            if (cars != null)
            {
                _context.Cars.Remove(cars);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверяет, существует ли автомобиль.
        /// </summary>
        private bool CarsExists(int id)
        {
            return _context.Cars.Any(e => e.Carid == id);
        }
    }
}
