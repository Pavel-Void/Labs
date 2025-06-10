using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IKM.Data;
using IKM.Models;

namespace IKM.Controllers
{
    public class CarController : Controller
    {
        private readonly ApplicationContext _context;

        public CarController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var car = _context.Car.ToList();
            if (!car.Any())
            {
                Console.WriteLine("Машин нету");

                return View();
            }
            return View(car);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            if ((car.Name != null) || (car.Name_Model != null))
            {
                _context.Car.Add(car);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = _context.Car.FirstOrDefault(p => p.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var car = _context.Car.Find(id);
            if (car != null)
            {
                _context.Car.Remove(car);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = _context.Car.Find(id);

            if (car == null)
            {
                return NotFound();
            }
            return View(car);

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Name_Model")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if ((car.Name != null) || (car.Name_Model != null))
            {
                try
                {
                    _context.Update(car);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Car.Any(p => p.Id == car.Id))
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
            return View(car);
        }
    }
}
