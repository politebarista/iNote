using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNote.Models
{
    public class NoteInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public int Color { get; set; }
        public string LastChange { get; set; }
    }
}
