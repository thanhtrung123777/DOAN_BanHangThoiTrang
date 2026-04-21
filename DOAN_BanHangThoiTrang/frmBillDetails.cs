using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace DOAN_BanHangThoiTrang
{
    public partial class frmBillDetails : Form
    {
        DataContext db = new DataContext();
        bool AddNew = false;
        public frmBillDetails()
        {
            InitializeComponent();
        }
        private void setContol(bool check)
        {
            txtBillDetailID.Enabled = false;
            txtBillID.Enabled = check;
            txtProductID.Enabled = check;
            txtQuantity.Enabled = check;
            txtPrice.Enabled = check;
            txtTotal.Enabled = check;

            btnSave.Enabled = check;
            btnCancel.Enabled = check;
            btnAdd.Enabled = !check;
            btnEdit.Enabled = !check;
            btnDelete.Enabled = !check;
            btnExcel.Enabled = !check;
            btnExit.Enabled = !check;
            dgvBillDetail.Enabled = !check;
        }
        private void LoadGridData()
        {
            var data = from i in db.BillDetails
                       select i;
            dgvBillDetail.DataSource = data.ToList();
            setContol(false);
        }
        private void frmBillDetails_Load(object sender, EventArgs e)
        {
            dgvBillDetail.AutoGenerateColumns = false;
            dgvBillDetail.AllowUserToAddRows = false;
            LoadGridData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            AddNew = false;
            setContol(true);
            txtBillID.Enabled = false;
            txtBillID.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBillDetail.CurrentRow == null) return;
            if (MessageBox.Show("Bạn muốn xóa bản ghi này không?", "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                int id = (int)dgvBillDetail.CurrentRow.Cells["BillDetailID"].Value;
                var userDelete = db.BillDetails.SingleOrDefault(u => u.BillDetailID== id);

                if (userDelete != null)
                {
                    db.BillDetails.Remove(userDelete);
                    db.SaveChanges();
                    LoadGridData();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            setContol(false);
            if (dgvBillDetail.CurrentRow != null)
            {
                int rowIndex = dgvBillDetail.CurrentRow.Index;
                int colIndex = dgvBillDetail.CurrentCell.ColumnIndex;
                DataGridViewCellEventArgs args = new DataGridViewCellEventArgs(colIndex, rowIndex);
                dgvBillDetail_CellEnter(dgvBillDetail, args);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát không?", "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dgvBillDetail_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i < 0) return;

            txtBillDetailID.Text = dgvBillDetail.Rows[i].Cells["BillDetailID"].Value.ToString();
            txtBillID.Text = dgvBillDetail.Rows[i].Cells["BillID"].Value.ToString();
            txtProductID.Text = dgvBillDetail.Rows[i].Cells["ProductID"].Value.ToString();
            txtQuantity.Text = dgvBillDetail.Rows[i].Cells["Quantity"].Value.ToString();
            txtPrice.Text = dgvBillDetail.Rows[i].Cells["Price"].Value.ToString();
            txtTotal.Text = dgvBillDetail.Rows[i].Cells["Total"].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNew = true;
            setContol(true);
            txtBillDetailID.Clear();
            txtBillID.Clear();
            txtProductID.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            txtTotal.Clear();

            txtBillID.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBillID.Text.Trim() == "")
            {
                MessageBox.Show("Thông tin mã phiếu xuất không được để trống!", "Thông báo");
                txtBillID.Focus();
                return;
            }


            if (AddNew)
            {
                //Kiểm tra trùng EmployeeID
                //int inputEmployeeID = int.Parse(txtEmployeeID.Text.Trim());
                // bool isExisted = db.Imports.Any(i => i.ImportID == inputEmployeeID);
                // if (isExisted)
                //  {
                // MessageBox.Show("Mã nhân viên này đã tồn tại! Vui lòng chọn mã khác.",
                //   "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //   txtImportID.Focus();
                //   return;
                //   }

                //Nếu không trùng thì tiến hành thêm mới
                tblBillDetail newBillDetail = new tblBillDetail
                {
                    

                    BillID = int.Parse(txtBillID.Text.Trim()),
                    ProductID = int.Parse(txtProductID.Text.Trim()),
                    Quantity = int.Parse(txtQuantity.Text.Trim()),
                    Price = decimal.Parse(txtPrice.Text.Trim()),
                    Total = decimal.Parse(txtTotal.Text.Trim())
                };

                db.BillDetails.Add(newBillDetail);
                db.SaveChanges();
                LoadGridData();
            }
            else //Nếu trước đó ấn vào nút sửa thì đoạn này sẽ thực hiện
            {
                if (dgvBillDetail.CurrentRow == null) return;

                int id = int.Parse(txtBillDetailID.Text);
                // Tìm đối tượng cần sửa bằng LINQ
                tblBillDetail importUpdate = db.BillDetails.SingleOrDefault(i => i.BillDetailID == id);

                if (importUpdate != null)
                {
                    importUpdate.BillID = int.Parse(txtBillID.Text.Trim());
                    importUpdate.ProductID = int.Parse(txtProductID.Text.Trim());
                    importUpdate.Quantity = int.Parse(txtQuantity.Text.Trim());
                    importUpdate.Price = decimal.Parse(txtPrice.Text.Trim());
                    importUpdate.Total = decimal.Parse(txtTotal.Text.Trim());

                    db.SaveChanges();
                    LoadGridData();
                }
            }
        }
    }
}
