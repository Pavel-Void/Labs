using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarDelivery.Data;
using CarDelivery.Models;

namespace CarDelivery.Controllers
{
    /// <summary>
    /// Контроллер для управления заказами.
    /// </summary>
    public class OrdersController : Controller
    {
        private readonly CarDeliveryContext _context;

        /// <summary>
        /// Конструктор контроллера заказов.
        /// </summary>
        public OrdersController(CarDeliveryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отображает список заказов.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var carDeliveryContext = _context.Orders.Include(o => o.Cars).Include(o => o.Users);
            return View(await carDeliveryContext.ToListAsync());
        }

        /// <summary>
        /// Отображает детали заказа.
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Cars)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        /// <summary>
        /// Отображает форму создания заказа.
        /// </summary>
        public IActionResult Create()
        {
            ViewData["Carid"] = new SelectList(_context.Cars, "Carid", "Carid");
            ViewData["Userid"] = new SelectList(_context.Users, "Userid", "Userid");
            return View();
        }

        /// <summary>
        /// Создает новый заказ.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Orderid,Userid,Carid,Quantity")] Orders orders)
        {
            // Ручная проверка данных
            if (orders.Quantity <= 0)
                ModelState.AddModelError(nameof(orders.Quantity), "Количество должно быть больше нуля.");

            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Carid"] = new SelectList(_context.Cars, "Carid", "Carid", orders.Carid);
            ViewData["Userid"] = new SelectList(_context.Users, "Userid", "Userid", orders.Userid);
            return View(orders);
        }

        /// <summary>
        /// Отображает форму редактирования заказа.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["Carid"] = new SelectList(_context.Cars, "Carid", "Carid", orders.Carid);
            ViewData["Userid"] = new SelectList(_context.Users, "Userid", "Userid", orders.Userid);
            return View(orders);
        }

        /// <summary>
        /// Редактирует заказ.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Orderid,Userid,Carid,Quantity")] Orders orders)
        {
            if (id != orders.Orderid)
                return NotFound();

            // Ручная проверка данных
            if (orders.Quantity <= 0)
                ModelState.AddModelError(nameof(orders.Quantity), "Количество должно быть больше нуля.");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.Orderid))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Carid"] = new SelectList(_context.Cars, "Carid", "Carid", orders.Carid);
            ViewData["Userid"] = new SelectList(_context.Users, "Userid", "Userid", orders.Userid);
            return View(orders);
        }

        /// <summary>
        /// Отображает форму удаления заказа.
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Cars)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        /// <summary>
        /// Удаляет заказ.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders != null)
            {
                _context.Orders.Remove(orders);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверяет, существует ли заказ.
        /// </summary>
        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.Orderid == id);
        }
    }
}
