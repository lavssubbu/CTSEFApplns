using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSCodeFirstOnetoMany.Models
{
    internal class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        [ForeignKey("Author")]
        public int AuthId {  get; set; }
        public Author Author { get; set; }
    }
}
