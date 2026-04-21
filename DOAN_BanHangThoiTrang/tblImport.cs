using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOAN_BanHangThoiTrang
{
    [Table("Imports")]
    internal class tblImport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImportID { get; set; }

        public int EmployeeID { get; set; }

        public DateTime ImportDate { get; set; }

        [Column(TypeName = "decimal")]
        public decimal TotalAmount { get; set; }
        
        
    }
}
