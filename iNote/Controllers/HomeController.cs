using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using iNote.Models;
using Microsoft.EntityFrameworkCore;

namespace iNote.Controllers
{
    public class HomeController : Controller
    {
        NoteContext db;
        public HomeController(NoteContext context)
        {
            db = context; // тут получаем контекст данных из стартапа (какая-то там зависимость)
        }

        public IActionResult Index()
        {
            return View(db.Note.ToList());
        }
        public async Task<IActionResult> Viewing(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteInfo = await db.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noteInfo == null)
            {
                return NotFound();
            }

            return View(noteInfo);
        }
        public IActionResult Creating()
        {
            ViewBag.isChanging = 1;
            return View("Changing");
        }
        public IActionResult Changing()
        {
            return View();
        }
        [HttpPost]
        public string Creating(NoteInfo order)
        {
            try
            {
                order.LastChange = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                db.Note.Add(order); // добавляем в БД
                db.SaveChanges(); // сохраняем БД
                return "Записка " + order.Title + " успешно добавлена.";
            }
            catch (Exception ex)
            {
                return "Ошибка при заполнении. (HomeController)" + ex;
            }
        }
        /*public string Changing(NoteInfo order)
        {
            try
            {
                db.Note.Add(order); // добавляем в БД
                db.SaveChanges(); // сохраняем БД
                return "Записка " + order.Title + " успешно добавлена.";
            }
            catch (Exception ex)
            {
                return "Ошибка при заполнении. (HomeController)" + ex;
            }
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
