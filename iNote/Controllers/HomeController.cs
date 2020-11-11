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
            ViewBag.isVisible = 1;
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
            ViewBag.isCreating = 1;
            return View("Changing");
        }
        public async Task<IActionResult> Changing(int? id)
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
        [HttpPost]
        public IActionResult Creating(NoteInfo order)
        {
                order.LastChange = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                db.Note.Add(order); // добавляем в БД
                db.SaveChanges(); // сохраняем БД
                return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Changing(int id, NoteInfo noteInfo)
        {
            /*if (id != noteInfo.Id)
            {
                return NotFound();
            }*/

            if (ModelState.IsValid)
            {
                try
                {
                    noteInfo.LastChange = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                    db.Update(noteInfo);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteInfoExists(noteInfo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Home/Viewing/"+id);
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private bool NoteInfoExists(int id)
        {
            return db.Note.Any(e => e.Id == id);
        }
    }
}
