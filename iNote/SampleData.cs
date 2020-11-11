using iNote.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace iNote
{
    public class SampleData
    {
        public static void Initialize(NoteContext context)
        {
            if (!context.Note.Any())
            {
                context.Note.AddRange(
                    new NoteInfo
                    { 
                        Title = "Приветствуем вас в iNote!",
                        Desc = "Это тестовая заметка, она создается автоматически. Попробуйте ее изменить",
                        Color = "secondary",
                        LastChange = DateTime.Now.ToString("dd.MM.yyyy hh:mm")
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
