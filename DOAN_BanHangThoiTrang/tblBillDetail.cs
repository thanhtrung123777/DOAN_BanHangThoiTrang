using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOAN_BanHangThoiTrang
{
    [Table("BillDetails")]
    internal class tblBillDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillDetailID { get; set; }

        public int BillID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Total { get; set; }
    }
}
