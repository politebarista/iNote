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
        public async Task<IActionResult> View(int? id)
        {
            ViewBag.isVisible = 1;
            if (id == null)
            {
                return NotFound();
            }

            var Note = await db.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Note == null)
            {
                return NotFound();
            }

            return View(Note);
        }
        public IActionResult Creat()
        {
            ViewBag.isCreating = 1;
            return View("Change");
        }
        public async Task<IActionResult> Change(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Note = await db.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Note == null)
            {
                return NotFound();
            }

            return View(Note);
        }
        [HttpPost]
        public IActionResult Creat(Note order)
        {
                order.LastChange = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                db.Note.Add(order); // добавляем в БД
                db.SaveChanges(); // сохраняем БД
                return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Change(int id, Note Note)
        {
            /*if (id != Note.Id)
            {
                return NotFound();
            }*/

            if (ModelState.IsValid)
            {
                try
                {
                    Note.LastChange = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                    db.Update(Note);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteInfoExists(Note.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Home/View/"+id);
            }
            return RedirectToAction("Index");
        }
        
        private bool NoteInfoExists(int id)
        {
            return db.Note.Any(e => e.Id == id);
        }
    }
}
