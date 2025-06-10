using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IKM.Data;
using IKM.Models;
using System.Linq;
using System.Net;

namespace IKM.Controllers
{
    public class DriverCarController : Controller
    {
        private readonly ApplicationContext _context;

        public DriverCarController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var driverCar = _context.DriverCar.Include(dc => dc.Driver).Include(dc => dc.Car).ToList();
            if (!driverCar.Any())
            {
                Console.WriteLine("Связей водителей и автомобилей не найдено");
                return View();
            }
            return View(driverCar);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DriverCar driverCar)
        {
            if ((driverCar.Car_ID > 0) && (driverCar.Driver_ID > 0))
            {
                if (_context.DriverCar.Any(c => c.Driver_ID == driverCar.Driver_ID && c.Car_ID == driverCar.Car_ID))
                {
                    return View(driverCar);
                }
                try
                {
                    _context.DriverCar.Add(driverCar);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    return View(driverCar);
                }
            }
            return View(driverCar);
        }


        [HttpGet]
        public IActionResult Delete(int? driverId, int? carId)
        {
            if (driverId == null || carId == null || _context.DriverCar == null)
            {
                return NotFound();
            }
            var driverCar = _context.DriverCar
                    .FirstOrDefault(dc => dc.Driver_ID == driverId && dc.Car_ID == carId);

            if (driverCar == null)
            {
                return NotFound();
            }
            return View(driverCar);

        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int driverId, int carId)
        {
            var driverCar = _context.DriverCar
                  .FirstOrDefault(dc => dc.Driver_ID == driverId && dc.Car_ID == carId);

            if (driverCar != null)
            {
                _context.DriverCar.Remove(driverCar);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Edit(int? driverId, int? carId)
        {
            if (driverId == null || carId == null || _context.DriverCar == null)
            {
                return NotFound();
            }

            var driverCar = _context.DriverCar.FirstOrDefault(dc => dc.Driver_ID == driverId && dc.Car_ID == carId);

            if (driverCar == null)
            {
                return NotFound();
            }
            return View(driverCar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DriverCar driverCar)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driverCar);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DriverCar.Any(dc => dc.Driver_ID == driverCar.Driver_ID && dc.Car_ID == driverCar.Car_ID))
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

            return View(driverCar);
        }
    }
}