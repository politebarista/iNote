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
            if (!context.note.Any())
            {
                context.note.AddRange(
                    new NoteInfo
                    { 
                        Title = "Приветствуем вас в iNote!",
                        Desc = "Это тестовая заметка, она создается автоматически. Попробуйте ее изменить",
                        Color = 1
                    }
                );
            }
        }
    }
}
