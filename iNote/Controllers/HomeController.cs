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
    }
}
