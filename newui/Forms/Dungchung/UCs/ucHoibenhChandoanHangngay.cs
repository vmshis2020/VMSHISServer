using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;
using Janus.Windows.GridEX;
using VNS.HIS.BusRule.Classes;
namespace VNS.HIS.UI.Forms.Dungchung.UCs
{
    public partial class ucHoibenhChandoanHangngay : UserControl
    {
        private DataTable m_dtChandoanKCB = new DataTable();
        action m_enActChandoan = action.FirstOrFinished;
        KcbLuotkham objLuotkham = null;
        NoitruPhieudieutri objPhieudieutri = null;
        private DataTable dt_ICD_PHU = new DataTable();
        KCB_KEDONTHUOC _KCB_KEDONTHUOC = new KCB_KEDONTHUOC();
        public ucHoibenhChandoanHangngay()
        {
            InitializeComponent();
            InitEvents();
        }
        void InitEvents()
        {
            cmdThemchandoan.Click += cmdThemchandoan_Click;
            cmdSuachandoan.Click += cmdSuachandoan_Click;
            cmdXoachandoan.Click += cmdXoachandoan_Click;
            cmdHuychandoan.Click += cmdHuychandoan_Click;
            cmdGhichandoan.Click += cmdGhichandoan_Click;
            txtMaBenhphu._OnEnterMe += txtMaBenhphu__OnEnterMe;
            grdChandoan.SelectionChanged += grdChandoan_SelectionChanged;
            foreach (Control ctrl in pnlMain.Controls)
                ctrl.KeyDown += ctrl_KeyDown;
        }

        void txtMaBenhphu__OnEnterMe()
        {
            if (txtMaBenhphu.MyCode != "-1")
            {
                EnumerableRowCollection<DataRow> query = from benh in dt_ICD_PHU.AsEnumerable()
                                                         where Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == txtMaBenhphu.MyCode
                                                         select benh;


                if (!query.Any())
                    AddMaBenh(txtMaBenhphu.MyCode, txtMaBenhphu.Text);
            }

            txtMaBenhphu.SetCode("-1");
            txtMaBenhphu.Focus();
            txtMaBenhphu.SelectAll();
        }
        private void AddMaBenh(string MaBenh, string TenBenh)
        {
            EnumerableRowCollection<DataRow> query = from benh in dt_ICD_PHU.AsEnumerable()
                                                     where Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == MaBenh
                                                     select benh;
            if (!query.Any())
            {
                DataRow drv = dt_ICD_PHU.NewRow();
                drv[DmucBenh.Columns.MaBenh] = MaBenh;
                EnumerableRowCollection<string> query1 = from benh in globalVariables.gv_dtDmucBenh.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) ==
                                                             MaBenh
                                                         select Utility.sDbnull(benh[DmucBenh.Columns.TenBenh]);
                if (query1.Any())
                {
                    drv[DmucBenh.Columns.TenBenh] = Utility.sDbnull(query1.FirstOrDefault());
                }

                dt_ICD_PHU.Rows.Add(drv);
                dt_ICD_PHU.AcceptChanges();
                grd_ICD.AutoSizeColumns();
            }
        }
        void ctrl_KeyDown(object sender, KeyEventArgs e)
        {
            Control _ctrl = sender as Control;
            if (e.KeyCode == Keys.Enter)
                SelectNextControl(_ctrl, true, true, true, true);
        }
        public void Init(KcbLuotkham objLuotkham, NoitruPhieudieutri objPhieudieutri)
        {
            this.objLuotkham = objLuotkham;
            this.objPhieudieutri = objPhieudieutri;
            LaydanhsachbacsiChidinh();
            LaydanhSachKhoaNoitru();
            txtMaBenhChinh.Init(globalVariables.gv_dtDmucBenh, new List<string>() { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
            txtMaBenhphu.Init(globalVariables.gv_dtDmucBenh, new List<string>() { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
        }
        void LaydanhSachKhoaNoitru()
        {
            try
            {
                DataTable dtKhoa = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
                txtKhoanoitru.Init(dtKhoa, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
                if (objLuotkham!=null)
                {
                    txtKhoanoitru.SetId(objLuotkham.IdKhoanoitru);
                }
            }
            catch (Exception exception)
            {
            }

        }
        private void LaydanhsachbacsiChidinh()
        {
            try
            {
                DataTable dtBS = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, 0);
                txtBacsi.Init(dtBS, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    txtBacsi.SetId(-1);
                }
                else
                {
                    txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                }

            }
            catch (Exception exception)
            {
                // throw;
            }

        }

        bool IsValidCommon()
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn Bệnh nhân!");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để ra viện nên bạn không thể thao tác");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú để ra viện nên bạn không thể thao tácn");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã kết thúc điều trị nội trú(Đã thanh toán xong) nên bạn không thể thao tác");
                return false;
            }
            return true;
        }
        /// <summary>
        /// hàm thực hiện trạng thái của nút
        /// </summary>
        private void ModifyCommmands()
        {
            string NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI", "1", false);
            try
            {

                cmdXoachandoan.Enabled =
                cmdSuachandoan.Enabled = objLuotkham != null && IsValidCommon() && Utility.isValidGrid(grdChandoan) && (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "0" || (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "1" && objPhieudieutri != null));
                //0=Ngoại trú;1=Nội trú;2=Đã điều trị(Lập phiếu);3=Đã tổng hợp chờ ra viện;4=Ra viện
                if (objLuotkham.TrangthaiNoitru > 3)
                {
                    cmdThemchandoan.Enabled = false;
                }
                else
                {
                    cmdThemchandoan.Enabled = objLuotkham != null && (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "0" || (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "1" && objPhieudieutri != null));
                }
            }
            catch (Exception exception)
            {
            }
            finally
            {
            }
        }
        void grdChandoan_SelectionChanged(object sender, EventArgs e)
        {
            if (objLuotkham == null || !Utility.isValidGrid(grdChandoan) )
            {
                ClearChandoan();
                EnableChandoan(false);
                ModifyCommmands();
                return;
            }
            else
            {
                txtIdChandoan.Text = Utility.getValueOfGridCell(grdChandoan, KcbChandoanKetluan.Columns.IdChandoan).ToString();
                KcbChandoanKetluan objKcbChandoanKetluan = KcbChandoanKetluan.FetchByID(Utility.Int32Dbnull(txtIdChandoan.Text, -1));
                if (objKcbChandoanKetluan != null)
                {
                    txtMach.Text = objKcbChandoanKetluan.Mach;
                    txtNhietDo.Text = objKcbChandoanKetluan.Nhietdo;
                    txtHa.Text = objKcbChandoanKetluan.Huyetap;
                    txtNhipTho.Text = objKcbChandoanKetluan.Nhiptho;
                    txtNhipTim.Text = objKcbChandoanKetluan.Nhiptim;
                    txtChanDoan._Text = objKcbChandoanKetluan.Chandoan;
                    txtChanDoanKemTheo._Text = objKcbChandoanKetluan.ChandoanKemtheo;
                    dtpNgaychandoan.Value = objKcbChandoanKetluan.NgayChandoan;
                    txtMaBenhChinh.SetCode( objKcbChandoanKetluan.MabenhChinh);
                    dt_ICD_PHU.Clear();
                    string dataString = objKcbChandoanKetluan.MabenhPhu;
                    if (!string.IsNullOrEmpty(dataString))
                    {
                        string[] rows = dataString.Split(',');
                        foreach (string row in rows)
                        {
                            if (!string.IsNullOrEmpty(row))
                            {
                                DataRow newDr = dt_ICD_PHU.NewRow();
                                newDr[DmucBenh.Columns.MaBenh] = row;
                                newDr[DmucBenh.Columns.TenBenh] = GetTenBenh(row);
                                dt_ICD_PHU.Rows.Add(newDr);
                                dt_ICD_PHU.AcceptChanges();
                            }
                        }
                        grd_ICD.DataSource = dt_ICD_PHU;
                    }
                    cmdXoachandoan.Enabled = cmdSuachandoan.Enabled = Utility.Byte2Bool(objKcbChandoanKetluan.Noitru);
                }
                else
                {
                    ClearChandoan();
                    EnableChandoan(false);
                }
            }
        }
        void ClearChandoan()
        {
            txtChanDoan.ResetText();
            txtChanDoanKemTheo.ResetText();
            txtMach.Clear();
            txtChieucao.Clear();
            txtCannang.Clear();
            txtNhommau.SetCode("-1");
            txtNhietDo.Clear();
            txtHa.Clear();
            txtNhipTho.Clear();
            txtNhipTim.Clear();
            dtpNgaychandoan.Value = globalVariables.SysDate;
            txtMaBenhphu.SetCode("-1");
            txtMaBenhChinh.SetCode("-1"); 
            if (dt_ICD_PHU != null) dt_ICD_PHU.Clear();
        }
        void EnableChandoan(bool _enable)
        {
            txtChieucao.Enabled = _enable;
            txtCannang.Enabled = _enable;
            txtNhommau.Enabled = _enable;

            txtMach.Enabled = _enable;
            txtNhietDo.Enabled = _enable;
            txtChanDoan.Enabled = _enable;
            txtChanDoanKemTheo.Enabled = _enable;
            txtHa.Enabled = _enable;
            txtNhipTim.Enabled = _enable;
            txtNhipTho.Enabled = _enable;
            dtpNgaychandoan.Enabled = _enable;
            txtMaBenhphu.Enabled = _enable;
            txtMaBenhChinh.Enabled = _enable;

        }
        private string GetTenBenh(string MaBenh)
        {
            string TenBenh = "";
            DataRow[] arrMaBenh = globalVariables.gv_dtDmucBenh.Select(string.Format(DmucBenh.Columns.MaBenh + "='{0}'", MaBenh));
            if (arrMaBenh.GetLength(0) > 0) TenBenh = Utility.sDbnull(arrMaBenh[0][DmucBenh.Columns.TenBenh], "");
            return TenBenh;
        }

        bool isValidChandoan()
        {
            if (Utility.Int32Dbnull(txtKhoanoitru.MyID, -1) <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập khoa nội trú", true);
                txtKhoanoitru.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(txtBacsi.MyID, -1) <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập bác sĩ khám", true);
                txtBacsi.Focus();
                return false;
            }
            if (objLuotkham != null && Utility.DoTrim(txtMaBenhChinh.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập ít nhất Mã bệnh chính để tạo dữ liệu chẩn đoán", true);
                txtMaBenhChinh.Focus();
                return false;
            }
            return true;

        }
        void cmdGhichandoan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidChandoan()) return;
                KcbChandoanKetluan objKcbChandoanKetluan = new KcbChandoanKetluan();
                if (m_enActChandoan == action.Update)
                {
                    objKcbChandoanKetluan = KcbChandoanKetluan.FetchByID(Utility.Int32Dbnull(txtIdChandoan.Text, -1));
                    objKcbChandoanKetluan.MarkOld();
                    objKcbChandoanKetluan.IsNew = false;
                }
                else
                {
                    objKcbChandoanKetluan = new KcbChandoanKetluan();
                    objKcbChandoanKetluan.IsNew = true;
                }
                objKcbChandoanKetluan.MaLuotkham = objLuotkham.MaLuotkham;
                objKcbChandoanKetluan.IdBenhnhan = objLuotkham.IdBenhnhan;
                objKcbChandoanKetluan.MabenhChinh = Utility.sDbnull(txtMaBenhChinh.Text, "");
                objKcbChandoanKetluan.Nhommau = txtNhommau.Text;
                objKcbChandoanKetluan.Nhietdo = Utility.sDbnull(txtNhietDo.Text);
                objKcbChandoanKetluan.Huyetap = txtHa.Text;
                objKcbChandoanKetluan.Mach = txtMach.Text;
                objKcbChandoanKetluan.Nhiptim = Utility.sDbnull(txtNhipTim.Text);
                objKcbChandoanKetluan.Nhiptho = Utility.sDbnull(txtNhipTho.Text);
                objKcbChandoanKetluan.Chieucao = Utility.sDbnull(txtChieucao.Text);
                objKcbChandoanKetluan.Cannang = Utility.sDbnull(txtCannang.Text);
                objKcbChandoanKetluan.HuongDieutri = "";
                objKcbChandoanKetluan.SongayDieutri = 0;

                if (Utility.Int16Dbnull(txtBacsi.MyID, -1) > 0)
                    objKcbChandoanKetluan.IdBacsikham = Utility.Int16Dbnull(txtBacsi.MyID, -1);
                else
                {
                    objKcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                }
                string sMaICDPHU = GetDanhsachBenhphu();
                objKcbChandoanKetluan.MabenhPhu = Utility.sDbnull(sMaICDPHU.ToString(), "");
                objKcbChandoanKetluan.IdKhoanoitru = objLuotkham.IdKhoanoitru;
                objKcbChandoanKetluan.IdBuong = objLuotkham.IdBuong;
                objKcbChandoanKetluan.IdGiuong = objLuotkham.IdGiuong;
                objKcbChandoanKetluan.IdBuonggiuong = objLuotkham.IdRavien;

                objKcbChandoanKetluan.IdKham = objPhieudieutri == null ? -1 : objPhieudieutri.IdPhieudieutri;
                objKcbChandoanKetluan.NgayTao = globalVariables.SysDate;
                objKcbChandoanKetluan.NguoiTao = globalVariables.UserName;
                objKcbChandoanKetluan.NgayChandoan = dtpNgaychandoan.Value;
                objKcbChandoanKetluan.Ketluan = "";
                objKcbChandoanKetluan.Chandoan = Utility.ReplaceString(txtChanDoan.Text);
                objKcbChandoanKetluan.ChandoanKemtheo = Utility.sDbnull(txtChanDoanKemTheo.Text);
                objKcbChandoanKetluan.IdPhieudieutri = objPhieudieutri == null ? -1 : objPhieudieutri.IdPhieudieutri;
                objKcbChandoanKetluan.Noitru = 1;
                objKcbChandoanKetluan.Save();
                DataRow[] arrDr = m_dtChandoanKCB.Select(KcbChandoanKetluan.Columns.IdChandoan + "=" + objKcbChandoanKetluan.IdChandoan.ToString());
                if (arrDr.Length > 0)
                {
                    Utility.FromObjectToDatarow(objKcbChandoanKetluan, ref arrDr[0]);
                    arrDr[0]["sNgay_chandoan"] = dtpNgaychandoan.Text;
                    Utility.GotoNewRowJanus(grdChandoan, KcbChandoanKetluan.Columns.IdChandoan, objKcbChandoanKetluan.IdChandoan.ToString());
                    m_dtChandoanKCB.AcceptChanges();
                }
                else
                {
                    DataRow newDr = m_dtChandoanKCB.NewRow();
                    Utility.FromObjectToDatarow(objKcbChandoanKetluan, ref newDr);
                    newDr["sNgay_chandoan"] = dtpNgaychandoan.Text;
                    m_dtChandoanKCB.Rows.Add(newDr);
                    m_dtChandoanKCB.AcceptChanges();
                    Utility.GotoNewRowJanus(grdChandoan, KcbChandoanKetluan.Columns.IdChandoan, objKcbChandoanKetluan.IdChandoan.ToString());
                }
                EnableChandoan(false);
                cmdGhichandoan.Enabled = cmdHuychandoan.Enabled = false;
                ModifyCommmands();
                grdChandoan_SelectionChanged(grdChandoan, e);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }
        string GetDanhsachBenhphu()
        {
            var sMaICDPHU = new StringBuilder("");
            try
            {
                int recordRow = 0;


                foreach (DataRow row in dt_ICD_PHU.Rows)
                {
                    if (recordRow > 0)
                        sMaICDPHU.Append(",");
                    sMaICDPHU.Append(Utility.sDbnull(row[DmucBenh.Columns.MaBenh], ""));
                    recordRow++;
                }

                return sMaICDPHU.ToString();
            }
            catch
            {
                return "";
            }
        }
        void cmdHuychandoan_Click(object sender, EventArgs e)
        {
            m_enActChandoan = action.FirstOrFinished;
            EnableChandoan(false);
            ModifyCommmands();
            grdChandoan_SelectionChanged(grdChandoan, e);
            cmdGhichandoan.Enabled = cmdHuychandoan.Enabled = false;
            cmdHuychandoan.SendToBack();
        }

        void cmdXoachandoan_Click(object sender, EventArgs e)
        {
            string NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI", "1", false);
            if (objLuotkham != null && !Utility.isValidGrid(grdChandoan) && (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "0" || (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "1" && objPhieudieutri != null)))
            {
                Utility.ShowMsg("Bạn phải chọn chẩn đoán trên lưới trước khi thực hiện xóa.");
                return;
            }
            if (grdChandoan.GetCheckedRows().Length <= 0)
            {
                grdChandoan.CurrentRow.IsChecked = true;
            }
            XoaChandoan();
            ModifyCommmands();
        }
        private void XoaChandoan()
        {
            try
            {
                string s = "";
                int Pres_ID = Utility.Int32Dbnull(grdChandoan.GetValue(KcbChandoanKetluan.Columns.IdChandoan));
                List<int> lstIdchitiet = new List<int>();
                foreach (GridEXRow gridExRow in grdChandoan.GetCheckedRows())
                {
                    string stempt = "";
                    int IdChandoan = Utility.Int32Dbnull(gridExRow.Cells[KcbChandoanKetluan.Columns.IdChandoan].Value, 0m);
                    s += "," + IdChandoan.ToString();
                    lstIdchitiet.Add(IdChandoan);
                    grdChandoan.Delete();
                    grdChandoan.UpdateData();

                }
                _KCB_KEDONTHUOC.NoitruXoachandoan(s);
                XoachandoanKCB(lstIdchitiet);
                m_dtChandoanKCB.AcceptChanges();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }
        void XoachandoanKCB(List<int> lstIdChandoanKCB)
        {
            try
            {
                var p = (from q in m_dtChandoanKCB.Select("1=1").AsEnumerable()
                         where lstIdChandoanKCB.Contains(Utility.Int32Dbnull(q[KcbChandoanKetluan.Columns.IdChandoan]))
                         select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtChandoanKCB.Rows.Remove(p[i]);
                m_dtChandoanKCB.AcceptChanges();
            }
            catch
            {
            }
        }
        void cmdSuachandoan_Click(object sender, EventArgs e)
        {
            cmdThemchandoan.Enabled = cmdSuachandoan.Enabled = cmdXoachandoan.Enabled = false;
            cmdGhichandoan.Enabled = cmdHuychandoan.Enabled = true;
            cmdHuychandoan.BringToFront();
            EnableChandoan(true);
            m_enActChandoan = action.Update;
            txtMach.Focus();
        }

        void cmdThemchandoan_Click(object sender, EventArgs e)
        {
            cmdThemchandoan.Enabled = cmdSuachandoan.Enabled = cmdXoachandoan.Enabled = false;
            cmdGhichandoan.Enabled = cmdHuychandoan.Enabled = true;
            cmdHuychandoan.BringToFront();
            ClearChandoan();
            EnableChandoan(true);
            m_enActChandoan = action.Insert;
            dtpNgaychandoan.Value = globalVariables.SysDate;
            txtMach.Focus();
        }
    }
}
