﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using Microsoft.VisualBasic;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.HIS.BusRule.Classes;


namespace VNS.HIS.UI.Baocao
{
    public partial class frm_baocaotiepdonbenhnhan : Form
    {
        public DataTable _dtData = new DataTable();
        bool m_blnhasLoaded = false;
        string tieude = "", reportname = "";
        decimal tong_tien = 0m;
        public frm_baocaotiepdonbenhnhan()
        {
            InitializeComponent();
            Initevents();
           
         
          
            
            dtNgayInPhieu.Value = globalVariables.SysDate;
        
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
        }
        void Initevents()
        {
            this.KeyDown += new KeyEventHandler(frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_KeyDown);
            this.cmdExit.Click += new EventHandler(cmdExit_Click);
            chkByDate.CheckedChanged += new EventHandler(chkByDate_CheckedChanged);
            this.Load += new EventHandler(frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load);
            chkChitiet.CheckedChanged+=new EventHandler(chkChitiet_CheckedChanged);
            ShowGrid();
        }
        void chkChitiet_CheckedChanged(object sender, EventArgs e)
        {
            ShowGrid();
        }
        void ShowGrid()
        {
            if (chkChitiet.Checked)
            {
                grdChitiet.BringToFront();
                baocaO_TIEUDE1.Init("baocao_tiepdonbenhnhan_chitiet");
            }
            else
            {
                grdList.BringToFront();
                baocaO_TIEUDE1.Init("baocao_tiepdonbenhnhan_tonghop");
            }
        }
        DataTable m_dtKhoathucHien=new DataTable();
        private void frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load(object sender, EventArgs eventArgs)
        {
            try
            {
                Autocompletenhanvien();
                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                           DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "Chọn đối tượng KCB", true);
                
                m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                DataBinding.BindDataCombobox(cboKhoa, m_dtKhoathucHien,
                                     DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "Chọn khoa KCB", true);
                var query = from khoa in m_dtKhoathucHien.AsEnumerable()
                            where Utility.sDbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) == globalVariables.MA_KHOA_THIEN
                            select khoa;
                if (query.Count() > 0)
                {
                    cboKhoa.SelectedValue = globalVariables.MA_KHOA_THIEN;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi load chức năng!", ex);
            }

        }
        private void Autocompletenhanvien()
        {
            DataTable _nhanvien = SPs.DmucLaydanhsachNhanvienTiepdon().GetDataSet().Tables[0];
            try
            {

                if (_nhanvien == null) return;
                if (!_nhanvien.Columns.Contains("ShortCut"))
                    _nhanvien.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in _nhanvien.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucNhanvien.Columns.TenNhanvien].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucNhanvien.Columns.TenNhanvien].ToString().Trim());
                    shortcut = dr[DmucNhanvien.Columns.UserName].ToString().Trim();
                    string[] arrWords = realName.ToLower().Split(' ');
                    string _space = "";
                    string _Nospace = "";
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                        {
                            _space += word + " ";
                            //_Nospace += word;
                        }
                    }
                    shortcut += _space; // +_Nospace;
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                            shortcut += word.Substring(0, 1);
                    }
                    dr["ShortCut"] = shortcut;
                }
            }
            catch
            {
            }
            finally
            {
                var source = new List<string>();
                var query = from p in _nhanvien.AsEnumerable()
                            select p[DmucNhanvien.Columns.IdNhanvien].ToString() + "#" + Utility.sDbnull( p[DmucNhanvien.Columns.UserName],"")+ "@" + p.Field<string>(DmucNhanvien.Columns.TenNhanvien).ToString() + "@" + p.Field<string>("shortcut").ToString();
                source = query.ToList();
                this.txtNhanvientiepdon.AutoCompleteList = source;
                this.txtNhanvientiepdon.TextAlign = HorizontalAlignment.Center;
                this.txtNhanvientiepdon.CaseSensitive = false;
                this.txtNhanvientiepdon.MinTypedCharacters = 1;

            }
        }
        /// <summary>
        /// trạng thái của tìm kiếm từ ngày tới ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin của form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInPhieuXN.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
            //  if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }
        /// <summary>
        /// hàm thực hiên việc thoát form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        MoneyByLetter _moneyByLetter = new MoneyByLetter();
        /// <summary>
        /// hàm thực hiện việc export excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
               
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls",tieude);
                //saveFileDialog1.ShowDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string sPath = saveFileDialog1.FileName;
                    FileStream fs = new FileStream(sPath, FileMode.Create);
                    fs.CanWrite.CompareTo(true);
                    fs.CanRead.CompareTo(true);
                    gridEXExporter1.Export(fs);
                    fs.Dispose();
                }
                saveFileDialog1.Dispose();
                saveFileDialog1.Reset();

            }
            catch (Exception exception)
            {

            }
        }
        /// <summary>
        /// hàm thực hiện việc in phiếu báo cáo tổng hợp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            if (chkChitiet.Checked)
            {
                _dtData =
                   BAOCAO_NGOAITRU.BaocaoTiepdonbenhnhanChitiet(Utility.Int32Dbnull(cboDoituongKCB.SelectedValue, -1),
                    chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                    chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate,txtNhanvientiepdon.MyCode, Utility.sDbnull(cboKhoa.SelectedValue, -1),txtLoaikham.myCode=="-1"?"ALL":txtLoaikham.myCode);

                Utility.SetDataSourceForDataGridEx(grdChitiet, _dtData, false, true, "1=1", "");
               
            }
            else
            {
                _dtData =
                  BAOCAO_NGOAITRU.BaocaoTiepdonbenhnhanTonghop(
                    chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                    chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate,
                    Utility.Int32Dbnull(cboDoituongKCB.SelectedValue, -1), txtNhanvientiepdon.MyCode, Utility.sDbnull(cboKhoa.SelectedValue, -1), txtLoaikham.myCode == "-1" ? "ALL" : txtLoaikham.myCode);

                Utility.SetDataSourceForDataGridEx(grdList, _dtData, false, true, "1=1", "");
              
            }

            if (_dtData.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo theo điều kiện bạn chọn", "Thông báo", MessageBoxIcon.Information);
                return;
            }
            Utility.UpdateLogotoDatatable(ref _dtData);
           
           
            string Condition = string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Khoa KCB :{3} - Người tiếp đón: {4}", dtFromDate.Text, dtToDate.Text,
                                          cboDoituongKCB.SelectedIndex >= 0
                                              ? Utility.sDbnull(cboDoituongKCB.Text)
                                              : "Tất cả",
                                          cboKhoa.SelectedIndex > 0
                                              ? Utility.sDbnull(cboKhoa.Text)
                                              : "Tất cả",txtNhanvientiepdon.MyCode=="-1"?"Tất cả":txtNhanvientiepdon.Text);
            var crpt = Utility.GetReport(chkChitiet.Checked ? "baocao_tiepdonbenhnhan_chitiet" : "baocao_tiepdonbenhnhan_tonghop", ref tieude, ref reportname);
            if (crpt == null) return;

            string StaffName = globalVariables.gv_strTenNhanvien;
            if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
            try
            {
                frmPrintPreview objForm = new frmPrintPreview(tieude, crpt, true, _dtData.Rows.Count <= 0 ? false : true);
                //try
                //{
                crpt.SetDataSource(_dtData);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = chkChitiet.Checked ? "baocao_tiepdonbenhnhan_chitiet" : "baocao_tiepdonbenhnhan_tonghop";
                crpt.SetParameterValue("StaffName", StaffName);
                crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
                crpt.SetParameterValue("Address", globalVariables.Branch_Address);
                crpt.SetParameterValue("Phone", globalVariables.Branch_Phone);
                crpt.SetParameterValue("FromDateToDate", Condition);
                crpt.SetParameterValue("sTitleReport", tieude);
                crpt.SetParameterValue("sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                crpt.SetParameterValue("BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {


            }
        }
        

        private void cboKhoa_ThucHien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
