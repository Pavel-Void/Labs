using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarDelivery.Data;
using CarDelivery.Models;

namespace CarDelivery.Controllers{
    /// <summary>
    /// Контроллер для управления пользователями.
    /// </summary>
    public class UsersController : Controller{
        private readonly CarDeliveryContext _context;

        /// <summary>
        /// Конструктор контроллера пользователей.
        /// </summary>
        public UsersController(CarDeliveryContext context){
            _context = context;
        }

        /// <summary>
        /// Отображает список пользователей.
        /// </summary>
        public async Task<IActionResult> Index(){
            return View(await _context.Users.ToListAsync());
        }

        /// <summary>
        /// Отображает детали пользователя.
        /// </summary>
        public async Task<IActionResult> Details(int? id){
            if (id == null) return NotFound();

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (users == null){
                return NotFound();
            }

            return View(users);
        }

        /// <summary>
        /// Отображает форму создания пользователя.
        /// </summary>
        public IActionResult Create(){
            return View();
        }

        /// <summary>
        /// Создает нового пользователя.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Name,Lastname,Email")] Users users){
            // Ручная проверка данных
            if (string.IsNullOrWhiteSpace(users.Name))
                ModelState.AddModelError(nameof(users.Name), "Имя обязательно для заполнения.");
            if (string.IsNullOrWhiteSpace(users.Lastname))
                ModelState.AddModelError(nameof(users.Lastname), "Фамилия обязательна для заполнения.");
            if (string.IsNullOrWhiteSpace(users.Email))
                ModelState.AddModelError(nameof(users.Email), "Email обязателен для заполнения.");
            else if (!users.Email.Contains("@"))
                ModelState.AddModelError(nameof(users.Email), "Email должен содержать символ '@'.");

            if (ModelState.IsValid){
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        /// <summary>
        /// Отображает форму редактирования пользователя.
        /// </summary>
        public async Task<IActionResult> Edit(int? id){
            if (id == null){
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null){
                return NotFound();
            }
            return View(users);
        }

        /// <summary>
        /// Редактирует пользователя.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Userid,Name,Lastname,Email")] Users users)
        {
            if (id != users.Userid){
                return NotFound();
            }
                
            // Ручная проверка данных
            if (string.IsNullOrWhiteSpace(users.Name))
                ModelState.AddModelError(nameof(users.Name), "Имя обязательно для заполнения.");
            if (string.IsNullOrWhiteSpace(users.Lastname))
                ModelState.AddModelError(nameof(users.Lastname), "Фамилия обязательна для заполнения.");
            if (string.IsNullOrWhiteSpace(users.Email))
                ModelState.AddModelError(nameof(users.Email), "Email обязателен для заполнения.");
            else if (!users.Email.Contains("@"))
                ModelState.AddModelError(nameof(users.Email), "Email должен содержать символ '@'.");

            if (ModelState.IsValid){
                try{
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException){
                    if (!UsersExists(users.Userid))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        /// <summary>
        /// Отображает форму удаления пользователя.
        /// </summary>
        public async Task<IActionResult> Delete(int? id){
            if (id == null){
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (users == null){
                return NotFound();
            }

            return View(users);
        }

        /// <summary>
        /// Удаляет пользователя.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id){
            var users = await _context.Users.FindAsync(id);
            if (users != null){
                _context.Users.Remove(users);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверяет, существует ли пользователь.
        /// </summary>
        private bool UsersExists(int id){
            return _context.Users.Any(e => e.Userid == id);
        }
    }
}
