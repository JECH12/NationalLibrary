using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Editorial
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Headquarters { get; set; } = null!;

        public ICollection<Book>? Books { get; set; }
    }
}
