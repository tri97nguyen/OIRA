using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace parser.Models {
    
    // TODO: finish XML comments
    /// <summary>
    /// 
    /// </summary>
    public class Section {
        public int Id { get; set; }

        [StringLength(10)]
        public int CRN { get; set; }


        public virtual Session Session { get; set; }
        public virtual int SessionId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }


    }

}