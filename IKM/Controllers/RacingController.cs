using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IKM.Data;
using IKM.Models;
using System;

namespace IKM.Controllers
{
    public class RacingController : Controller
    {
        private readonly ApplicationContext _context;

        public RacingController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var racings = _context.Racing
                            .Include(r => r.Driver)
                            .Include(r => r.Car)
                            .ToList();
            return View(racings);
        }

        public IActionResult Create()
        {
            ViewBag.Drivers = new SelectList(_context.Driver, "Id", "Name");
            ViewBag.Cars = new SelectList(_context.Car, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Racing racing)
        {
            if (racing.Finished > 0)
            {
                _context.Racing.Add(racing);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Drivers = new SelectList(_context.Driver, "Id", "Name");
            ViewBag.Cars = new SelectList(_context.Car, "Id", "Name");
            return View(racing);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Racing == null)
            {
                return NotFound();
            }

            var racing = _context.Racing
                .Include(r => r.Driver)
                .Include(r => r.Car)
                .FirstOrDefault(r => r.Id == id);

            if (racing == null)
            {
                return NotFound();
            }
            return View(racing);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var racing = _context.Racing.Find(id);
            if (racing != null)
            {
                _context.Racing.Remove(racing);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Racing == null)
            {
                return NotFound();
            }

            var racing = _context.Racing.Find(id);

            if (racing == null)
            {
                return NotFound();
            }
            ViewBag.Drivers = new SelectList(_context.Driver, "Id", "Name", racing.Driver_ID);
            ViewBag.Cars = new SelectList(_context.Car, "Id", "Name", racing.Car_ID);

            return View(racing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Driver_ID,Car_ID,Finished")] Racing racing)
        {
            if (id != racing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(racing);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Racing.Any(p => p.Id == racing.Id))
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
            ViewBag.Drivers = new SelectList(_context.Driver, "Id", "Name", racing.Driver_ID);
            ViewBag.Cars = new SelectList(_context.Car, "Id", "Name", racing.Car_ID);
            return View(racing);
        }
    }
}