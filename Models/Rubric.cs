using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace parser.Models
{
    public class Rubric {
        public int Id { get; set; }
        
        public virtual School School { get; set; }
        public virtual int SchoolId { get; set; }

        [StringLength(2)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Name { get; set; }


        public string Data { get; set; }

        public byte[] File { get; set; }


        public virtual ICollection<RubricCriteria> RubricCriteria { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
    // public class Rubric
    // {   
    //     [Key]
    //     [DatabaseGenerated(DatabaseGeneratedOption.None)]
    //     public string RubricId { get; set; }
    //     public string Name { get; set; }
    // }
}