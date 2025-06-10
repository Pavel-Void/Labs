using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IKM.Data;
using IKM.Models;

namespace IKM.Controllers
{
    public class DriverController : Controller
    {
        private readonly ApplicationContext _context;

        public DriverController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var driver = _context.Driver.ToList();
            if (!driver.Any())
            {
                Console.WriteLine("Водителей нету");

                return View();
            }
            return View(driver);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Driver driver)
        {
            using (var transaction = _context.Database.BeginTransaction()) {
                if (driver.Age > 0)
                {
                    try
                    {
                        transaction.CreateSavepoint("savepoint1");

                        _context.Driver.Add(driver);

                        _context.SaveChanges();

                        //transaction.RollbackToSavepoint("savepoint1");

                        transaction.Commit();

                        return RedirectToAction("Index");

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return View();
                    }
                }
                else
                {
                    return View();
                } 
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Driver == null)
            {
                return NotFound();
            }

            var driver = _context.Driver.FirstOrDefault(p => p.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var driver = _context.Driver.Find(id);
            if (driver != null)
            {
                _context.Driver.Remove(driver);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Driver == null)
            {
                return NotFound();
            }

            var driver = _context.Driver.Find(id);

            if (driver == null)
            {
                return NotFound();
            }
            return View(driver);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Age")] Driver driver)
        {
            if (id != driver.Id)
            {
                return NotFound();
            }

            if (driver.Age > 0)
            {
                try
                {
                    _context.Update(driver);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Driver.Any(p => p.Id == driver.Id))
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
            return View(driver);
        }
    }
}
