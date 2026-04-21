using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DOAN_BanHangThoiTrang
{
    internal class DataContext : DbContext
    {
        public DataContext() : base("name=MyConnectionString")
        {
           // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
        }

        public DbSet<tblImport> Imports { get; set; }
        public DbSet<tblImportDetails> ImportDetails { get; set; }
        public DbSet<tblBillDetail> BillDetails { get; set; }

    }
}
