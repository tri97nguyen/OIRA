using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace parser.Models
{
    [Table("Schools")]
    public class School
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
