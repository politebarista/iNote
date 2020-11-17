using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iNote.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iNote.Controllers
{
    public class NotesController : Controller
    {
        NoteContext db;
        public NotesController(NoteContext context)
        {
            db = context; // тут получаем контекст данных из стартапа (какая-то там зависимость)
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
        public IActionResult Create()
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
        public IActionResult Create(Note order)
        {
            order.LastChange = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            db.Note.Add(order); // добавляем в БД
            db.SaveChanges(); // сохраняем БД
            return Redirect("/");
        }
        [HttpPost]
        public async Task<IActionResult> Change(int id, Note Note)
        {
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
                return Redirect("~/Notes/View/" + id);
            }
            return Redirect("/");
        }

        private bool NoteInfoExists(int id)
        {
            return db.Note.Any(e => e.Id == id);
        }
    }
}
