using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IKM.Data;
using IKM.Models;

namespace IKM.Controllers
{
    public class DriverPropertyController : Controller
    {
        private readonly ApplicationContext _context;

        public DriverPropertyController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var driverProperties = _context.DriverProperty.Include(dp => dp.Driver).ToList();
            return View(driverProperties);
        }

        public IActionResult Create()
        {
            ViewBag.Drivers = _context.Driver.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DriverProperty driverProperty)
        {
            if (ModelState.IsValid)
            {
                _context.DriverProperty.Add(driverProperty);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Drivers = _context.Driver.ToList();
            return View(driverProperty);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.DriverProperty == null)
            {
                return NotFound();
            }

            var driverProperty = _context.DriverProperty
                .Include(dp => dp.Driver)
                .FirstOrDefault(dp => dp.Id == id);

            if (driverProperty == null)
            {
                return NotFound();
            }

            return View(driverProperty);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var driverProperty = _context.DriverProperty.Find(id);
            if (driverProperty != null)
            {
                _context.DriverProperty.Remove(driverProperty);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.DriverProperty == null)
            {
                return NotFound();
            }
            var driverProperty = _context.DriverProperty
                .Include(dp => dp.Driver)
                .FirstOrDefault(dp => dp.Id == id);
            if (driverProperty == null)
            {
                return NotFound();
            }
            ViewBag.Drivers = _context.Driver.ToList();
            return View(driverProperty);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DriverProperty driverProperty)
        {
            if (id != driverProperty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driverProperty);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DriverProperty.Any(p => p.Id == driverProperty.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.Drivers = _context.Driver.ToList();
            return View(driverProperty);
        }
    }
}