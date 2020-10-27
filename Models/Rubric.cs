using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace parser.Models
{
    public class Rubric
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RubricId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
    }
}