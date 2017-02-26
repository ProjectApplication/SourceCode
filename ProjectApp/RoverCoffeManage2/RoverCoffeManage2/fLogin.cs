﻿using CustomForm;
using RoverCoffeManage2.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoverCoffeManage2
{
    public partial class fLogin : MyForm
    {
        public fLogin()
        {
            InitializeComponent();
            load_UserName();// hàm này để load dữ liệu của username lên combo box
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String username = cbUserName.Text;
            String password = tbPassWord.Text;
            if (Login(username, password))
            {
                fAdmin fadmin = new fAdmin();
                this.Hide();//ẩn form login
                fadmin.ShowDialog();//hiện form admin
                this.Show();
            }
            else

                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
        }
        private bool Login(String username, String password)
        {
            // hàm này để kiểm tra xem username và password có đúng hay k
            return DAO.AccountDAO.Instance.Login(username, password);
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            // hàm này dùng để xác nhận bạn có muốn thoát hay không
            if (MessageBox.Show("Bạn có muốn thoát chương trình ?", "Thông Báo", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                e.Cancel = true;
        }

        private void load_UserName()
        {
            // Thay đổi giá trị của combo box username tại đây
            // hàm này lấy giá trị từng hàng của table 
            foreach (DataRow row in DAO.DataProvider.Instance.ExecuteQuery("SELECT dbo.Account.UserName FROM dbo.Account").Rows)
                //hàm này để add giá trị từng hàng của cột username 
                cbUserName.Items.Add((string)row["UserName"]);
            //
        }
    }
}
