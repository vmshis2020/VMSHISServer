using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Windows.Forms;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.UI.StatusBar;
using SubSonic;
using SubSonic.Sugar;
using VNS.Libs;
using VNS.HIS.NGHIEPVU;
using VNS.HIS.DAL;
using VNS.UI.QMS;
using TextAlignment = Janus.Windows.GridEX.TextAlignment;
using TriState = Janus.Windows.GridEX.TriState;
using VNS.Properties;
using CrystalDecisions.CrystalReports.Engine;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.Classes;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs.AppUI;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.NGOAITRU;
namespace VNS.HIS.UI.NOITRU
{
    public delegate void SetParameterValueDelegate(string value, int IsUuTien);

    public delegate void SetParameterValueDelegateColose(Form frm);
    /// <summary>
    /// Đẩy thử code=Github
    /// </summary>
    public partial class frm_Taobenhnhancapcuu : Form
    {
        public delegate void OnActionSuccess();
        public event OnActionSuccess _OnActionSuccess;
        NoitruPhanbuonggiuong objBuonggiuong = null;
        NoitruPhanbuonggiuongCollection LstNoitruPhanbuonggiuong = new NoitruPhanbuonggiuongCollection();
        KCB_DANGKY _KCB_DANGKY = new KCB_DANGKY();
        KCB_QMS _KCB_QMS = new KCB_QMS();
        DMUC_CHUNG _DMUC_CHUNG = new DMUC_CHUNG();
        private readonly AutoCompleteStringCollection namesCollectionThanhPho = new AutoCompleteStringCollection();
        private readonly string strSaveandprintPath = Application.StartupPath + @"\CAUHINH\SaveAndPrintConfig.txt";

        private readonly string strSaveandprintPath1 = Application.StartupPath +
                                                       @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";

        private string MA_DTUONG = "DV";
        private string SoBHYT = "";
        private string TrongGio = "";
        public bool m_blnCancel;
        private bool b_HasLoaded;
        private bool AllowTextChanged;
        private bool AllowGridSelecttionChanged=true;
        private string _rowFilter = "1=1";
        private bool b_NhapNamSinh;

        public GridEX grdList;
        private bool hasjustpressBACKKey;
        private bool isAutoFinding;
        bool m_blnHasJustInsert = false;
        private DataTable m_DC;

        private DataTable m_dtDataRoom = new DataTable();
        private DataTable m_dtDatabed = new DataTable();
        private DataTable m_PhongKham = new DataTable();
        private DataTable m_kieuKham;
        private DataTable m_dtChoKham = new DataTable();
        private DataTable m_dtDoiTuong = new DataTable();
        private DataTable m_dtTrieuChung = new DataTable();
        public action m_enAction = action.Insert;

        private DataTable m_dataDataRegExam = new DataTable();
        private DataTable mdt_DataQuyenhuyen;
        private frm_ScreenSoKham _QMSScreen;
        public DataTable m_dtPatient = new DataTable();
        string m_strMaluotkham = "";//Lưu giá trị patientcode khi cập nhật để người dùng ko được phép gõ Patient_code lung tung
        KcbDanhsachBenhnhan objBenhnhan = null;
        public frm_Taobenhnhancapcuu()
        {
            InitializeComponent();
            InitEvents();
            txtTEN_BN.CharacterCasing = globalVariables.CHARACTERCASING == 0
                                            ? CharacterCasing.Normal
                                            : CharacterCasing.Upper;
           
            dtCreateDate.Value =dtNgayChuyen.Value= globalVariables.SysDate;
            dtInsFromDate.Value = new DateTime(globalVariables.SysDate.Year, 01, 01);
            dtInsToDate.Value = new DateTime(globalVariables.SysDate.Year, 12, 31);

           
            CauHinhKCB();
        }

        void InitEvents()
        {
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Taobenhnhancapcuu_FormClosing);
            this.Load += new System.EventHandler(this.frm_Taobenhnhancapcuu_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Taobenhnhancapcuu_KeyDown);

            txtMaBN.KeyDown += new KeyEventHandler(txtMaBN_KeyDown);
            txtMaLankham.KeyDown += new KeyEventHandler(txtMaLankham_KeyDown);

            dtpBOD.TextChanged += dtpBOD_TextChanged;
            txtMaDtuong_BHYT.KeyDown += txtMaDtuong_BHYT_KeyDown;
            txtMaDtuong_BHYT.TextChanged += new EventHandler(txtMaDtuong_BHYT_TextChanged);
            txtMaDtuong_BHYT.LostFocus += txtMaQuyenloi_BHYT_LostFocus;

            txtMaQuyenloi_BHYT.KeyDown += txtMaQuyenloi_BHYT_KeyDown;
            txtMaQuyenloi_BHYT.TextChanged += new EventHandler(txtMaQuyenloi_BHYT_TextChanged);
            txtMaQuyenloi_BHYT.PreviewKeyDown += txtMaQuyenloi_BHYT_PreviewKeyDown;
            txtMaQuyenloi_BHYT.KeyPress += txtMaQuyenloi_BHYT_KeyPress;
            txtMaQuyenloi_BHYT.LostFocus += txtMaQuyenloi_BHYT_LostFocus;

            txtNoiphattheBHYT.TextChanged += new EventHandler(txtNoiphattheBHYT_TextChanged);
            txtNoiphattheBHYT.KeyDown += txtNoiphattheBHYT_KeyDown;
            txtOthu4.KeyDown += txtOthu4_KeyDown;
            txtOthu4.TextChanged += new EventHandler(txtOthu4_TextChanged);
            txtOthu5.KeyDown += txtOthu5_KeyDown;
            txtOthu5.TextChanged += new EventHandler(txtOthu5_TextChanged);
            txtOthu6.TextChanged += new EventHandler(txtOthu6_TextChanged);
            txtOthu6.KeyDown += txtOthu6_KeyDown;
            txtOthu6.LostFocus += _LostFocus;
            txtNoiDKKCBBD.LostFocus += txtNoiDKKCBBD_LostFocus;
            txtNoiDKKCBBD.KeyDown += txtNoiDKKCBBD_KeyDown;
            txtNoiDKKCBBD.TextChanged += new EventHandler(txtNoiDKKCBBD_TextChanged);

            txtNoiDongtrusoKCBBD.TextChanged += new EventHandler(txtNoiDongtrusoKCBBD_TextChanged);
            txtNoiDongtrusoKCBBD.KeyDown += txtNoiDongtrusoKCBBD_KeyDown;

            txtTEN_BN.TextChanged+=new EventHandler(txtTEN_BN_TextChanged);
            txtTEN_BN.LostFocus += txtTEN_BN_LostFocus;
            txtCMT.KeyDown += txtCMT_KeyDown;
            chkTraiTuyen.CheckedChanged += chkTraiTuyen_CheckedChanged;
            cboPatientSex.SelectedIndex = 0;
            
            cmdThemMoiBN.Click += new System.EventHandler(cmdThemMoiBN_Click);
            cmdSave.Click += new System.EventHandler(cmdSave_Click);

            lnkThem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkThem_LinkClicked);
            txtTuoi.TextChanged += new System.EventHandler(txtTuoi_TextChanged);
            txtTuoi.Click += new System.EventHandler(txtTuoi_Click);
            txtTuoi.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTuoi_KeyDown);

            txtTuoi.LostFocus += txtTuoi_LostFocus;
            txtNamSinh.TextChanged += txtNamSinh_TextChanged;

            txtNamSinh.LostFocus += txtNamSinh_LostFocus;

            chkChuyenVien.CheckedChanged += new EventHandler(chkChuyenVien_CheckedChanged);
            cboDoituongKCB.SelectedIndexChanged += new EventHandler(cboDoituongKCB_SelectedIndexChanged);
           

           

            txtTrieuChungBD._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtTrieuChungBD__OnShowData);
            txtDantoc._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtDantoc__OnShowData);
            txtNgheNghiep._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNgheNghiep__OnShowData);


            txtMaDTsinhsong._OnEnterMe += txtMaDTsinhsong__OnEnterMe;
            chkGiayBHYT.CheckedChanged += chkGiayBHYT_CheckedChanged;
            cmdGetBV.Click += new EventHandler(cmdGetBV_Click);
            cmdThemmoiDiachinh.Click += cmdThemmoiDiachinh_Click;
            chkLaysokham.CheckedChanged += chkLaysokham_CheckedChanged;
            dtNgayChuyen.TextChanged += new EventHandler(dtNgayChuyen_TextChanged);
            dtNgayChuyen.ValueChanged += new EventHandler(dtNgayChuyen_ValueChanged);
            txtGia._OnSelectionChanged += txtGia__OnSelectionChanged;
            txtKhoanoitru._OnEnterMe += txtKhoanoitru__OnEnterMe;
            txtRoom_code._OnEnterMe += txtRoom_code__OnEnterMe;
            grdBuong.SelectionChanged+=grdBuong_SelectionChanged;
            grdBuong.KeyDown+=grdBuong_KeyDown;
            grdGiuong.KeyDown+=grdGiuong_KeyDown;
            grdGiuong.SelectionChanged += grdGiuong_SelectionChanged;
            grdGiuong.MouseDoubleClick+=grdGiuong_MouseDoubleClick;
            txtBedCode._OnEnterMe += txtBedCode__OnEnterMe;
            cmdLaysoBANoitru.Click += cmdLaysoBANoitru_Click;

        }

        void cmdLaysoBANoitru_Click(object sender, EventArgs e)
        {
            txtSoBenhAn.Text = THU_VIEN_CHUNG.LaySoBenhAn();
        }

        void grdGiuong_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowGridSelecttionChanged || !Utility.isValidGrid(grdGiuong)) return;
            ChonGiuong();
        }
        void ChonGiuong()
        {
            string IdGiuong = Utility.sDbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong), -1);
            txtBedCode.SetId(IdGiuong);
        }
        void txtBedCode__OnEnterMe()
        {
            Utility.GotoNewRowJanus(grdGiuong, NoitruDmucGiuongbenh.Columns.IdGiuong, Utility.sDbnull(txtBedCode.MyID, ""));
        }

        void txtRoom_code__OnEnterMe()
        {
            Utility.GotoNewRowJanus(grdBuong, NoitruDmucBuong.Columns.IdBuong, Utility.sDbnull(txtRoom_code.MyID, ""));
        }
        void grdGiuong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            grdGiuong_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        }
        private void grdGiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.isValidGrid(grdGiuong))
            {
                cmdSave_Click(cmdSave, new EventArgs());
            }
        }
        private void grdBuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBedCode.Focus();
                txtRoom_code.Text = Utility.sDbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.TenBuong));
            }
        }
        private void grdBuong_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowGridSelecttionChanged || !Utility.isValidGrid(grdBuong)) return;
            ChonBuong();
        }
        void ChonBuong()
        {
            string IdBuong = Utility.sDbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong), -1);
            txtRoom_code.SetId(IdBuong);
            m_dtDatabed = THU_VIEN_CHUNG.NoitruTimkiemgiuongTheobuong(Utility.Int32Dbnull(txtKhoanoitru.MyID),
                                                           Utility.Int32Dbnull(IdBuong));
            Utility.SetDataSourceForDataGridEx_Basic(grdGiuong, m_dtDatabed, true, true, "1=1", "isFull asc,dang_nam ASC,ten_giuong");
            string oldBed = txtBedCode.MyCode;
            txtBedCode.Init(m_dtDatabed, new List<string>() { NoitruDmucGiuongbenh.Columns.IdGiuong, NoitruDmucGiuongbenh.Columns.MaGiuong, NoitruDmucGiuongbenh.Columns.TenGiuong });
            txtBedCode.SetCode(oldBed);
            if (grdGiuong.DataSource != null)
            {
                grdGiuong.MoveFirst();

            }
            if (txtBedCode.MyCode == "-1")
            {
                string IdGiuong = Utility.sDbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong), -1);
                txtBedCode.SetId(IdGiuong);
            }
        }
        void txtKhoanoitru__OnEnterMe()
        {
            m_dtDataRoom = THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(txtKhoanoitru.MyID));

            Utility.SetDataSourceForDataGridEx_Basic(grdBuong, m_dtDataRoom, true, true, "1=1", "sluong_giuong_trong desc,ten_buong");
            txtRoom_code.Init(m_dtDataRoom, new List<string>() { NoitruDmucBuong.Columns.IdBuong, NoitruDmucBuong.Columns.MaBuong, NoitruDmucBuong.Columns.TenBuong });
            if (grdBuong.DataSource != null)
            {
                grdBuong.MoveFirst();
            }
        }
        void txtGia__OnSelectionChanged()
        {
            cboGia.Text = txtGia.MyText;
        }
        void dtNgayChuyen_ValueChanged(object sender, EventArgs e)
        {
            txtGio.Text = Utility.sDbnull(dtNgayChuyen.Value.Hour);
            txtPhut.Text = Utility.sDbnull(dtNgayChuyen.Value.Minute);
        }

        void dtNgayChuyen_TextChanged(object sender, EventArgs e)
        {
            txtGio.Text = Utility.sDbnull(dtNgayChuyen.Value.Hour);
            txtPhut.Text = Utility.sDbnull(dtNgayChuyen.Value.Minute);

        }
        void chkLaysokham_CheckedChanged(object sender, EventArgs e)
        {
            txtSoKcb.Enabled =chkLaysokham.Enabled && chkLaysokham.Checked;
        }

        void cmdThemmoiDiachinh_Click(object sender, EventArgs e)
        {
            frm_themmoi_diachinh_new _themmoi_diachinh = new frm_themmoi_diachinh_new();
            _themmoi_diachinh.ShowDialog();
            if (_themmoi_diachinh.m_blnHasChanged)
            {
               
                AddAutoCompleteDiaChi();
            }
        }

        void dtpBOD_TextChanged(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_NHAP_NGAYTHANGNAMSINH", false) == "1")
            {
                txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - dtpBOD.Value.Year);
            }
        }

       

        void chkGiayBHYT_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkTraiTuyen.Checked && chkGiayBHYT.Checked)
            //    chkTraiTuyen.Checked = false;
            TinhPtramBHYT();
        }

        void txtMaDTsinhsong__OnEnterMe()
        {
            if (txtMaDTsinhsong.myCode != "-1")
            {
                if ( chkTraiTuyen.Checked)
                {
                    chkTraiTuyen.Checked = false;
                    lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
                }
            }
            TinhPtramBHYT();
        }

        void cmdGetBV_Click(object sender, EventArgs e)
        {
            frm_danhsachbenhvien _danhsachbenhvien = new frm_danhsachbenhvien();
            if (_danhsachbenhvien.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtNoichuyenden.SetId(_danhsachbenhvien.idBenhvien);
            }
        }

        void txtNgheNghiep__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtNgheNghiep.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNgheNghiep.myCode;
                txtNgheNghiep.Init();
                txtNgheNghiep.SetCode(oldCode);
                txtNgheNghiep.Focus();
            } 
        }

        void txtDantoc__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtDantoc.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtDantoc.myCode;
                txtDantoc.Init();
                txtDantoc.SetCode(oldCode);
                txtDantoc.Focus();
            }
        }

        void txtTrieuChungBD__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtTrieuChungBD.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtTrieuChungBD.myCode;
                txtTrieuChungBD.Init();
                txtTrieuChungBD.SetCode(oldCode);
                txtTrieuChungBD.Focus();
            }
        }

      

        void cboDoituongKCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowTextChanged) return;
                _MaDoituongKcb = Utility.sDbnull(cboDoituongKCB.SelectedValue);
                objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                ChangeObjectRegion();
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void chkChuyenVien_CheckedChanged(object sender, EventArgs e)
        {
            txtNoichuyenden.Enabled = chkChuyenVien.Checked;
            cmdGetBV.Enabled = chkChuyenVien.Checked;
            //Tạm bỏ 2014-12-04
            //LoadClinicCode();
        }

       
        bool AutoLoad = false;
       
      

       
        

        void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._KCBProperties);
            frm.ShowDialog();
            CauHinhKCB();
        }

        

        private string GetSoBHYT
        {
            get { return SoBHYT; }
            set { SoBHYT = value; }
        }

        private void txtTEN_BN_LostFocus(object sender, EventArgs e)
        {
            txtTEN_BN.Text =Utility.CapitalizeWords(txtTEN_BN.Text.Trim());
        }

        private void _LostFocus(object sender, EventArgs e)
        {
            if (isAutoFinding) return;
            string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                             txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
            if (MA_BHYT.Length == 15) FindPatientIDbyBHYT(MA_BHYT);
        }

        private void txtNoiDKKCBBD_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                //string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiDongtrusoKCBBD.Text.Trim() + txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                //if (MA_BHYT.Length == 15) FindPatientIDbyBHYT(MA_BHYT);
                return;
            }
            if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtNoiDKKCBBD.Text.Length <= 0)
                {
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.Select(txtNoiDongtrusoKCBBD.Text.Length, 0);
                }
            }
        }

        private void txtNoiphattheBHYT_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                return;
            }
            if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtNoiphattheBHYT.Text.Length <= 0)
                {
                    txtMaQuyenloi_BHYT.Focus();
                    txtMaQuyenloi_BHYT.Select(txtMaQuyenloi_BHYT.Text.Length, 0);
                }
            }
        }

        private void chkTraiTuyen_CheckedChanged(object sender, EventArgs e)
        {
            lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
            TinhPtramBHYT();
        }

        private void txtMaQuyenloi_BHYT_LostFocus(object sender, EventArgs e)
        {
        }

        private void txtNoiDKKCBBD_LostFocus(object sender, EventArgs e)
        {
            //if (lblClinicName.Text.Trim() == "")
            //{
            //    txtNoiDKKCBBD.Focus();
            //    txtNoiDKKCBBD.SelectAll();
            //}
        }

        private void txtMaQuyenloi_BHYT_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtMaQuyenloi_BHYT_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }


       

      

        private void txtMaLankham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtMaLankham.Text.Trim() != "")
            {
                txtNoiDKKCBBD.Clear();
                txtNoiphattheBHYT.Clear();
                isAutoFinding = true;
                string patient_ID = Utility.GetYY(globalVariables.SysDate) +
                                    Utility.FormatNumberToString(Utility.Int32Dbnull(txtMaLankham.Text, 0), "000000");
                txtMaLankham.Text = patient_ID;
                FindPatientIDbyMaLanKham(txtMaLankham.Text.Trim());
                isAutoFinding = false;
            }
        }

        private void FindPatientIDbyBHYT(string Insurance_Num)
        {
            try
            {

                DataTable temdt = SPs.KcbTimkiembenhnhantheomathebhyt(Insurance_Num).GetDataSet().Tables[0];
                if (temdt.Rows.Count <= 0) return;
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), Insurance_Num);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr = temdt.Select(KcbLuotkham.Columns.MatheBhyt+ "='" + Insurance_Num + "'");
                    if (arrDr.Length == 1)
                        AutoFindLastExamandFetchIntoControls(arrDr[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), Insurance_Num);
                    else
                    {
                        var _ChonBN = new frm_CHON_BENHNHAN();
                        _ChonBN.temdt = temdt;
                        _ChonBN.ShowDialog();
                        if (!_ChonBN.mv_bCancel)
                        {
                            AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, Insurance_Num);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }

        private void FindPatientIDbyCMT(string CMT)
        {
            try
            {
                DataTable temdt = SPs.KcbTimkiembenhnhantheosocmt(CMT).GetDataSet().Tables[0];
                if (temdt.Rows.Count <= 0) return;
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr = temdt.Select(KcbDanhsachBenhnhan.Columns.Cmt+ "='" + CMT + "'");
                    if (arrDr.Length == 1)
                        AutoFindLastExamandFetchIntoControls(arrDr[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty);
                    else
                    {
                        var _ChonBN = new frm_CHON_BENHNHAN();
                        _ChonBN.temdt = temdt;
                        _ChonBN.ShowDialog();
                        if (!_ChonBN.mv_bCancel)
                        {
                            AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, string.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtMaBN.Text.Trim() != "")
            {
                txtNoiDKKCBBD.Clear();
                txtNoiphattheBHYT.Clear();
                isAutoFinding = true;
                FindPatient(txtMaBN.Text.Trim());
                isAutoFinding = false;
            }
        }

        private bool NotPayment(string patient_ID, ref string NgayKhamGanNhat)
        {
            try
            {
                DataTable temdt = _KCB_DANGKY.KcbLaythongtinBenhnhan(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)));
                if (temdt != null && Utility.ByteDbnull(temdt.Rows[0][KcbLuotkham.Columns.TrangthaiNoitru], 0) > 0 && Utility.ByteDbnull(temdt.Rows[0][KcbLuotkham.Columns.TrangthaiNoitru], 0) < 4)
                {
                    Utility.ShowMsg("Bệnh nhân đang ở trạng thái nội trú và chưa ra viện nên không thể thêm lần khám mới. Đề nghị bạn xem lại");
                    return true;
                }
               
                if (temdt != null && temdt.Rows.Count <= 0)
                {
                    NgayKhamGanNhat = "NOREG";
                    //Chưa đăngký khám lần nào(mới gõ thông tin BN)-->Trạng thái sửa
                    return true; //Chưa thanh toán-->Cho về trạng thái sửa
                }
                if (temdt != null && temdt.Rows.Count > 0 && temdt.Select("trangthai_thanhtoan=0").Length > 0)
                {
                    NgayKhamGanNhat = temdt.Select("trangthai_thanhtoan=0", "ma_luotkham")[0]["Ngay_Kham"].ToString();
                    return true; //Chưa thanh toán-->Có thể cho về trạng thái sửa
                }
                else //Đã thanh toán--.Thêm lần khám mới
                    return false;
            }
            catch (Exception ex)
            {
                return false; //Đã thanh toán--.Thêm lần khám mới
            }
        }

        private void FindPatient(string patient_ID)
        {
            try
            {
                QueryCommand cmd = KcbDanhsachBenhnhan.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql =
                    "Select id_benhnhan,ten_benhnhan,gioi_tinh from kcb_danhsach_benhnhan where id_benhnhan like '%" +
                    patient_ID + "%'";

                DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr = temdt.Select("id_benhnhan=" + patient_ID);
                    if (arrDr.Length == 1)
                        AutoFindLastExamandFetchIntoControls(arrDr[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty);
                    else
                    {
                        var _ChonBN = new frm_CHON_BENHNHAN();
                        _ChonBN.temdt = temdt;
                        _ChonBN.ShowDialog();
                        if (!_ChonBN.mv_bCancel)
                        {
                            AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, string.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }

        private void FindPatientIDbyMaLanKham(string malankham)
        {
            try
            {
                QueryCommand cmd = KcbDanhsachBenhnhan.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql =
                    "Select id_benhnhan,ten_benhnhan,gioi_tinh from kcb_danhsach_benhnhan p where exists(select 1 from kcb_luotkham where id_benhnhan=P.id_benhnhan and ma_luotkham like '%" +
                    malankham + "%')";
                DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                if (temdt.Rows.Count <= 0) return;
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty);
                }
                else //Show dialog for select
                {
                    var _ChonBN = new frm_CHON_BENHNHAN();
                    _ChonBN.temdt = temdt;
                    _ChonBN.ShowDialog();
                    if (!_ChonBN.mv_bCancel)
                    {
                        AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
            
        }

        private void AutoFindLastExamandFetchIntoControls(string patientID, string sobhyt)
        {
            try
            {
                if (!Utility.CheckLockObject(m_strMaluotkham, "Tiếp đón", "TD"))
                    return;
                //Trả lại mã lượt khám nếu chưa được dùng đến
                new Update(KcbDmucLuotkham.Schema)
                       .Set(KcbDmucLuotkham.Columns.TrangThai).EqualTo(0)
                       .Set(KcbDmucLuotkham.Columns.UsedBy).EqualTo(DBNull.Value)
                       .Set(KcbDmucLuotkham.Columns.StartTime).EqualTo(DBNull.Value)
                       .Set(KcbDmucLuotkham.Columns.EndTime).EqualTo(null)
                       .Where(KcbDmucLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.Int32Dbnull(m_strMaluotkham, "-1"))
                       .And(KcbDmucLuotkham.Columns.TrangThai).IsEqualTo(1)
                       .And(KcbDmucLuotkham.Columns.UsedBy).IsEqualTo(globalVariables.UserName)
                       .And(KcbDmucLuotkham.Columns.Nam).IsEqualTo(globalVariables.SysDate.Year).Execute();
                ;

                SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(patientID);
                if (!string.IsNullOrEmpty(sobhyt))
                {
                    sqlQuery.And(KcbLuotkham.Columns.MatheBhyt).IsEqualTo(sobhyt);
                }
                sqlQuery.OrderDesc(KcbLuotkham.Columns.NgayTiepdon);

                var objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
                if (objLuotkham != null)
                {
                    txtMaBN.Text = patientID;
                    txtMaLankham.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                    m_strMaluotkham = objLuotkham.MaLuotkham;
                    m_enAction = action.Update;
                    AllowTextChanged = false;
                    LoadThongtinBenhnhan();
                    CanhbaoInphoi();
                    string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");
                    //Nếu ngày hệ thống=Ngày đăng ký gần nhất-->Sửa
                    if (globalVariables.SysDate.ToString("dd/MM/yyyy") == dtpInputDate.Value.ToString("dd/MM/yyyy"))
                    {
                        m_enAction = action.Update;
                        Utility.ShowMsg(
                           "Bệnh nhân vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                        //LaydanhsachdangkyKCB();
                        txtTEN_BN.Select();
                    }
                    else//Thêm lần khám cho ngày mới
                    {
                        m_enAction = action.Add;
                        SinhMaLanKham();
                        //Reset dịch vụ KCB
                        //txtTongChiPhiKham.Text = "0";
                        m_dataDataRegExam.Rows.Clear();

                    }
                    StatusControl();
                    ModifyCommand();
                }
                else
                {
                    Utility.ShowMsg(
                        "Bệnh nhân này chưa có lần khám nào-->Có thể bị lỗi dữ liệu. Đề nghị liên hệ với VMS để được giải đáp");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("AutoFindLastExam().Exception-->" + ex.Message);
            }
            finally
            {
                SetActionStatus();
                AllowTextChanged = true;
            }
        }

    
     

        
        
        // private  b_QMSStop=false;
        /// <summary>
        /// hàm thực hiện việc lấy thông tin của phần dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Taobenhnhancapcuu_Load(object sender, EventArgs e)
        {
            try
            {
                AllowTextChanged = false;
                b_HasLoaded = false;
                dtInsFromDate.Value = new DateTime(globalVariables.SysDate.Year, 1, 1);
                dtInsToDate.Value = new DateTime(globalVariables.SysDate.Year, 12, 31);
                Utility.SetColor(lblDiachiBHYT, THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BHYT", "1", false) == "1" ? lblHoten.ForeColor : lblMatheBHYT.ForeColor);
                Utility.SetColor(lblDiachiBN, THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BENHNHAN", "1", false) == "1" ? lblHoten.ForeColor : lblMatheBHYT.ForeColor);
                chkTraiTuyen.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_CHOPHEPTIEPDON_TRAITUYEN", "1", false) == "1";
                chkLaysokham.Enabled = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATBUOCLAY_SOKHAMCHUABENH", "0", false) == "0";
                txtSoKcb.Enabled = chkLaysokham.Enabled && chkLaysokham.Checked;

                XoathongtinBHYT(true);
                AddAutoCompleteDiaChi();
                Get_DanhmucChung();
                AutocompleteBenhvien();
                txtKhoanoitru.Init(THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0), new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
                
                DataTable dtGia = new dmucgiagiuong_busrule().dsGetList("-1").Tables[0];
                dtGia.DefaultView.Sort = NoitruGiabuonggiuong.Columns.SttHthi + "," + NoitruGiabuonggiuong.Columns.TenGia;
                txtGia.Init(dtGia, new System.Collections.Generic.List<string>() { NoitruGiabuonggiuong.Columns.IdGia, NoitruGiabuonggiuong.Columns.MaGia, NoitruGiabuonggiuong.Columns.TenGia });
                cboGia.DataSource = dtGia;
                cboGia.DataMember = NoitruGiabuonggiuong.Columns.IdGia;
                cboGia.ValueMember = NoitruGiabuonggiuong.Columns.IdGia;
                cboGia.DisplayMember = NoitruGiabuonggiuong.Columns.TenGia;

                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(), DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "", false);
                objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                ChangeObjectRegion();
                if (m_enAction == action.Insert)//Thêm mới BN
                {
                    objLuotkham = null;
                    if (PropertyLib._KCBProperties.SexInput)
                        cboPatientSex.SelectedIndex = -1;
                    SinhMaLanKham();
                    txtSoBenhAn.Text = THU_VIEN_CHUNG.LaySoBenhAn();
                    txtTEN_BN.Select();
                }
                else if (m_enAction == action.Update)//Cập nhật thông tin Bệnh nhân
                {
                    LoadThongtinBenhnhan();
                    txtTEN_BN.Select();
                }
                else if (m_enAction == action.Add) //Thêm mới lần khám
                {
                    objLuotkham = null;
                    string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");
                    if (!NotPayment(txtMaBN.Text.Trim(), ref ngay_kham))//Nếu đã thanh toán xong hết thì thêm lần khám mới
                    {
                        SinhMaLanKham();
                        LoadThongtinBenhnhan();
                        
                    }
                    else//Còn lần khám chưa thanh toán-->Kiểm tra
                    {
                        //nếu là ngày hiện tại thì đặt về trạng thái sửa
                        if (ngay_kham == "NOREG" || ngay_kham==globalVariables.SysDate.ToString("dd/MM/yyyy"))
                        {
                            LoadThongtinBenhnhan();
                            if (ngay_kham == "NOREG")//Bn chưa đăng ký phòng khám nào cả. 
                            {
                                //Nếu ngày hệ thống=Ngày đăng ký gần nhất-->Sửa
                                if (globalVariables.SysDate.ToString("dd/MM/yyyy") == dtpInputDate.Value.ToString("dd/MM/yyyy"))
                                {
                                    m_enAction = action.Update;

                                    Utility.ShowMsg(
                                       "Bệnh nhân vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                                    txtTEN_BN.Select();
                                }
                                else//Thêm lần khám cho ngày mới
                                {
                                    m_enAction = action.Add;
                                    SinhMaLanKham();
                                    
                                }
                            }
                            else//Quay về trạng thái sửa
                            {
                                m_enAction = action.Update;

                                Utility.ShowMsg(
                                   "Bệnh nhân vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                                txtTEN_BN.Select();
                            }
                        }
                        else //Không cho phép thêm lần khám khác nếu chưa thanh toán lần khám của ngày hôm trước
                        {
                            Utility.ShowMsg(
                                "Bệnh nhân đang có lần khám chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Bệnh nhân trước khi thêm lần khám mới. Nhấn OK để hệ thống chuyển về trạng thái thêm mới Bệnh nhân");
                            cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                        }
                    }
                }
                StatusControl();
                ModifyCommand();
                AllowTextChanged = true;
            }
            catch
            {
            }
            finally
            {
                if (PropertyLib._ConfigProperties.HIS_AppMode != VNS.Libs.AppType.AppEnum.AppMode.License)
                    this.Text = "Đăng ký KCB -->Demo 1500";
                SetActionStatus(); 
               
                ModifyCommand();

                b_HasLoaded = true;
                CanhbaoInphoi();
                
            }
        }
        void CanhbaoInphoi()
        {
            try
            {
                int patient_ID = Utility.Int32Dbnull(txtMaBN.Text, -1);
                if (patient_ID <= 0) return;
                DmucCanhbaoCollection lst = new Select().From(DmucCanhbao.Schema).Where(DmucCanhbao.MaBnColumn).IsEqualTo(patient_ID).ExecuteAsCollection<DmucCanhbaoCollection>();
                if (lst.Count > 0)//Delete
                {
                    if (lst[0].CanhBao.TrimStart().TrimEnd() != "")
                        Utility.ShowMsg(lst[0].CanhBao,"Thông tin cảnh báo dành cho Bệnh nhân");
                }
            }
            catch
            {
            }
        }
      
        byte _IdLoaidoituongKcb = 1;
        Int16 _IdDoituongKcb = 1;
        string _MaDoituongKcb = "DV";
        string _TenDoituongKcb = "Dịch vụ";
        decimal PtramBhytCu = 0m;
        decimal PtramBhytGocCu = 0m;
        KcbLuotkham objLuotkham = null;
        private void LoadThongtinBenhnhan()
        {
            PtramBhytCu = 0m;
            PtramBhytGocCu = 0m;
            AllowTextChanged = false;
            LstNoitruPhanbuonggiuong = new NoitruPhanbuonggiuongCollection();
            objBuonggiuong = null;
            objBenhnhan = KcbDanhsachBenhnhan.FetchByID(txtMaBN.Text);
            if (objBenhnhan != null)
            {
                txtTEN_BN.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan);
                txtNamSinh.Text = Utility.sDbnull(objBenhnhan.NamSinh);
                txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai);
                txtDiachi_bhyt._Text = Utility.sDbnull(objBenhnhan.DiachiBhyt);
                txtDiachi._Text = Utility.sDbnull(objBenhnhan.DiaChi);
                if (objBenhnhan.NgaySinh != null) dtpBOD.Value = objBenhnhan.NgaySinh.Value;
                else dtpBOD.Value = new DateTime((int)objBenhnhan.NamSinh, 1, 1);
                txtNamSinh.Text = Utility.sDbnull(objBenhnhan.NamSinh);
                txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(objBenhnhan.NamSinh));
                txtNgheNghiep._Text = Utility.sDbnull(objBenhnhan.NgheNghiep);
                cboPatientSex.SelectedIndex = Utility.GetSelectedIndex(cboPatientSex, Utility.sDbnull(objBenhnhan.IdGioitinh));
                if (Utility.Int32Dbnull(objBenhnhan.DanToc) > 0)
                    txtDantoc._Text = objBenhnhan.DanToc;
                txtCMT.Text = Utility.sDbnull(objBenhnhan.Cmt);


                objLuotkham = new Select().From(KcbLuotkham.Schema)
                   .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaLankham.Text)
                   .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtMaBN.Text, -1)).ExecuteSingle
                   <KcbLuotkham>();
                if (objLuotkham != null)
                {
                     LstNoitruPhanbuonggiuong = new Select().From(NoitruPhanbuonggiuong.Schema)
              .Where(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
              .And(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
              .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();
                     if (LstNoitruPhanbuonggiuong.Count > 0)
                         objBuonggiuong = LstNoitruPhanbuonggiuong[0];
                         
                     ucTamung1.ChangePatients(objLuotkham,"LYDOTAMUNGCAPCUU");
                    KcbDangkySokham objSoKCB=new Select().From(KcbDangkySokham.Schema)
                        .Where(KcbDangkySokham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(KcbDangkySokham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<KcbDangkySokham>();
                    if (objSoKCB != null)
                    {
                        chkLaysokham.Checked = true;
                        txtSoKcb.SetCode(objSoKCB.MaSokcb);
                    }
                    else
                    {
                        chkLaysokham.Checked = false;
                        txtSoKcb.SetDefaultItem();
                    }
                    if (string.IsNullOrEmpty(Utility.sDbnull(objLuotkham.SoBenhAn, "")))
                    {
                        txtSoBenhAn.Text = THU_VIEN_CHUNG.LaySoBenhAn();
                    }
                    else
                    {
                        txtSoBenhAn.Text = Utility.sDbnull(objLuotkham.SoBenhAn, "");
                    }

                    m_strMaluotkham = objLuotkham.MaLuotkham;
                    if (objBuonggiuong != null)
                    {
                        AllowGridSelecttionChanged = false;
                        txtKhoanoitru.SetId(objBuonggiuong.IdKhoanoitru);
                        txtKhoanoitru__OnEnterMe();
                        txtGia.SetId(objBuonggiuong.IdGia);
                        txtRoom_code.SetId(objBuonggiuong.IdBuong);
                        txtRoom_code__OnEnterMe();
                        ChonBuong();
                        txtBedCode.SetId(objBuonggiuong.IdGiuong);
                        txtBedCode__OnEnterMe();
                        AllowGridSelecttionChanged = true;
                        dtNgayChuyen.Value = Convert.ToDateTime(objBuonggiuong.NgayVaokhoa);
                        txtGio.Text = Utility.sDbnull(dtNgayChuyen.Value.Hour);
                        txtPhut.Text = Utility.sDbnull(dtNgayChuyen.Value.Minute);
                    }
                    txtSolankham.Text = Utility.sDbnull(objLuotkham.SolanKham);
                    _IdDoituongKcb = objLuotkham.IdDoituongKcb;
                    dtpInputDate.Value = objLuotkham.NgayTiepdon;
                    dtCreateDate.Value = objLuotkham.NgayTiepdon;
                    chkCapCuu.Checked = Utility.Int32Dbnull(objLuotkham.TrangthaiCapcuu, 0) == 1;
                    chkTraiTuyen.Checked = Utility.Int32Dbnull(objLuotkham.DungTuyen, 0) == 0;
                    lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
                    _MaDoituongKcb = Utility.sDbnull(objLuotkham.MaDoituongKcb);
                    objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();

                    ChangeObjectRegion();
                    PtramBhytCu = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                    PtramBhytGocCu = Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0);
                    _IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
                    _TenDoituongKcb = objDoituongKCB.TenDoituongKcb;
                    cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                    chkChuyenVien.Checked = Utility.Int32Dbnull(objLuotkham.TthaiChuyenden, 0) == 1;
                    txtNoichuyenden.SetId(Utility.Int32Dbnull(objLuotkham.IdBenhvienDen, -1));
                    if (!string.IsNullOrEmpty(objLuotkham.MatheBhyt))//Thông tin BHYT
                    {
                        txtTrieuChungBD._Text = Utility.sDbnull(objLuotkham.TrieuChung);
                        if (!string.IsNullOrEmpty(Utility.sDbnull(objLuotkham.NgaybatdauBhyt)))
                            dtInsFromDate.Value = Convert.ToDateTime(objLuotkham.NgaybatdauBhyt);
                        if (!string.IsNullOrEmpty(Utility.sDbnull(objLuotkham.NgayketthucBhyt)))
                            dtInsToDate.Value = Convert.ToDateTime(objLuotkham.NgayketthucBhyt);
                        txtPtramBHYT.Text = Utility.sDbnull(objLuotkham.PtramBhyt, "0");
                        txtptramDauthe.Text = Utility.sDbnull(objLuotkham.PtramBhytGoc, "0");
                        //HS7010340000005
                        txtMaDtuong_BHYT.Text = Utility.sDbnull(objLuotkham.MaDoituongBhyt);

                        txtMaQuyenloi_BHYT.Text = Utility.sDbnull(objLuotkham.MaQuyenloi);
                        txtNoiDongtrusoKCBBD.Text = Utility.sDbnull(objLuotkham.NoiDongtrusoKcbbd);
                        txtOthu4.Text = Utility.sDbnull(objLuotkham.MatheBhyt).Substring(5, 2);
                        txtOthu5.Text = Utility.sDbnull(objLuotkham.MatheBhyt).Substring(7, 3);
                        txtOthu6.Text = Utility.sDbnull(objLuotkham.MatheBhyt).Substring(10, 5);

                        txtMaDTsinhsong.SetCode(objLuotkham.MadtuongSinhsong);
                        chkGiayBHYT.Checked = Utility.Byte2Bool(objLuotkham.GiayBhyt);

                        txtNoiphattheBHYT.Text = Utility.sDbnull(objLuotkham.MaNoicapBhyt);
                        txtNoiDKKCBBD.Text = Utility.sDbnull(objLuotkham.MaKcbbd);
                        pnlBHYT.Enabled = true;
                    }
                    else
                    {
                        XoathongtinBHYT(true);
                    }
                }
                else
                {
                }
            }
            chkChuyenVien_CheckedChanged(chkChuyenVien, new EventArgs());
        }

        void XoathongtinBHYT(bool forcetodel)
        {
            if (forcetodel)
            {
                _IdDoituongKcb = 1;
                _MaDoituongKcb = "DV";
                _TenDoituongKcb = "Dịch vụ";
                dtInsFromDate.Value = new DateTime(globalVariables.SysDate.Year, 1, 1);
                dtInsToDate.Value = new DateTime(globalVariables.SysDate.Year, 12, 31);
                txtPtramBHYT.Text = "";
                txtptramDauthe.Text = "";
                lblNoiCapThe.Text = "";
                lblClinicName.Text = "";
                txtMaDtuong_BHYT.Clear();
                txtMaDTsinhsong.ResetText();
                chkGiayBHYT.Checked = false;
                txtMaQuyenloi_BHYT.Clear();
                txtNoiDongtrusoKCBBD.Clear();
                txtOthu4.Clear();
                txtOthu5.Clear();
                txtOthu6.Clear();
                chkTraiTuyen.Checked = false;
                lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
                txtNoiphattheBHYT.Clear();
                txtDiachi_bhyt.Clear();
                txtNoiDKKCBBD.Clear();
                //pnlBHYT.Enabled = false;
            }
            
            
        }
      
        private void Get_DanhmucChung()
        {
           
            AutoCompleteDmucChung();
            AutocompleteDautheBHYT();
        }
        private void AddAutoCompleteDiaChi()
        {
            txtDiachi_bhyt.dtData = globalVariables.dtAutocompleteAddress;
            txtDiachi.dtData = globalVariables.dtAutocompleteAddress.Copy();
            this.txtDiachi_bhyt.AutoCompleteList = globalVariables.LstAutocompleteAddressSource;
            this.txtDiachi_bhyt.CaseSensitive = false;
            this.txtDiachi_bhyt.MinTypedCharacters = 1;

            this.txtDiachi.AutoCompleteList = globalVariables.LstAutocompleteAddressSource;
            this.txtDiachi.CaseSensitive = false;
            this.txtDiachi.MinTypedCharacters = 1;

            
         
        }
        private void AutocompleteDautheBHYT()
        {
            try
            {
                return;
                DataTable dt_dataDoituongBHYT = new Select().From(DmucDoituongbhyt.Schema).ExecuteDataSet().Tables[0];
                txtMaDtuong_BHYT2.Init(dt_dataDoituongBHYT, new List<string>() { DmucDoituongbhyt.Columns.IdDoituongbhyt, DmucDoituongbhyt.Columns.MaDoituongbhyt, DmucDoituongbhyt.Columns.TenDoituongbhyt });
            }
            catch
            {
            }
            finally
            {
            }
        }

        private void AutoCompleteDmucChung()
        {
            txtMaDTsinhsong.Init();
            txtDantoc.Init();
            txtNgheNghiep.Init();
            txtTrieuChungBD.Init();
            txtSoKcb.Init();
        }

     

        private void AutocompleteBenhvien()
        {
          
            try
            {
                DataTable m_dtBenhvien = new Select().From(DmucBenhvien.Schema).ExecuteDataSet().Tables[0];
                if (m_dtBenhvien == null) return;
                txtNoichuyenden.Init(m_dtBenhvien, new List<string>() { DmucBenhvien.Columns.IdBenhvien, DmucBenhvien.Columns.MaBenhvien, DmucBenhvien.Columns.TenBenhvien });
               
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
              

            }
        }
       
      
        private void pnlBHYT_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// hàm thực hiện việc đánh nhanh thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaDtuong_BHYT_TextChanged(object sender, EventArgs e)
        {
            if (_MaDoituongKcb == "DV") return;
            if (txtMaDtuong_BHYT.Text.Length < 2) return;
            if (!IsValidTheBHYT()) return;
            TinhPtramBHYT();
            txtMaQuyenloi_BHYT.Focus();
            txtMaQuyenloi_BHYT.SelectAll();
        }

        private bool IsValidTheBHYT()
        {
            if (!string.IsNullOrEmpty(txtMaDtuong_BHYT.Text))
            {
                SqlQuery sqlQuery = new Select().From(DmucDoituongbhyt.Schema)
                    .Where(DmucDoituongbhyt.Columns.MaDoituongbhyt).IsEqualTo(txtMaDtuong_BHYT.Text);
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg(
                        "Mã đối tượng BHYT không tồn tại trong hệ thống. Mời bạn kiểm tra lại",
                        "Thông báo", MessageBoxIcon.Information);
                    txtMaDtuong_BHYT.Focus();
                    txtMaDtuong_BHYT.SelectAll();
                    return false;
                }
            }
            if (Utility.DoTrim(txtMaDtuong_BHYT.Text) != "" && Utility.DoTrim(txtMaQuyenloi_BHYT.Text) != "")
            {
                QheDautheQloiBhyt objQheDautheQloiBhyt = new Select().From(QheDautheQloiBhyt.Schema).Where(QheDautheQloiBhyt.Columns.MaDoituongbhyt).IsEqualTo(Utility.DoTrim(txtMaDtuong_BHYT.Text))
                    .And(QheDautheQloiBhyt.Columns.MaQloi).IsEqualTo(Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, 0)).ExecuteSingle<QheDautheQloiBhyt>();
                if (objQheDautheQloiBhyt == null)
                {
                    Utility.ShowMsg(string.Format("Đầu thẻ BHYT: {0} chưa được cấu hình gắn với mã quyền lợi: {1}. Mời bạn kiểm tra lại", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text));
                    txtMaQuyenloi_BHYT.Focus();
                    txtMaQuyenloi_BHYT.SelectAll();
                    return false;
                }
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_KIEMTRAMATHE","1",true) == "1")
            {
                if (!string.IsNullOrEmpty(txtMaQuyenloi_BHYT.Text))
                {
                    if (Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, 0) < 1 || Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, 0) > 9)
                    {
                        Utility.ShowMsg("Số thứ tự 2 của mã bảo hiểm nằm trong khoảng từ 1->9", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtMaQuyenloi_BHYT.Focus();
                        txtMaQuyenloi_BHYT.SelectAll();
                        return false;
                    }

                    QheDautheQloiBhytCollection lstqhe = new Select().From(QheDautheQloiBhyt.Schema).Where(QheDautheQloiBhyt.Columns.MaDoituongbhyt).IsEqualTo(txtMaDtuong_BHYT.Text).ExecuteAsCollection<QheDautheQloiBhytCollection>();
                    if (lstqhe.Count > 0)
                    {
                        var q = from p in lstqhe
                                where p.MaQloi == Utility.ByteDbnull(txtMaQuyenloi_BHYT.Text, -1)
                                select objDoituongKCB;

                        if (q.Count() <= 0)
                        {

                            Utility.ShowMsg(
                                string.Format(
                                    "Đầu thẻ :{0} chưa được tạo quan hệ với mã quyền lợi {1}\n Đề nghị bạn kiểm tra lại danh mục đối tượng tham gia BHYT",
                                    txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text));
                            txtMaQuyenloi_BHYT.Focus();
                            txtMaQuyenloi_BHYT.SelectAll();
                            return false;
                        }
                    }
                    else
                    {
                        Utility.ShowMsg(
                            string.Format(
                                "Đầu thẻ :{0} chưa được tạo quan hệ với mã quyền lợi {1}\n Đề nghị bạn kiểm tra lại danh mục đối tượng tham gia BHYT",
                                txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text));
                        txtMaQuyenloi_BHYT.Focus();
                        txtMaQuyenloi_BHYT.SelectAll();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtNoiphattheBHYT.Text))
                {
                    if (txtNoiphattheBHYT.Text.Length <= 1)
                    {
                        Utility.ShowMsg("Mã nơi phát thẻ BHYT phải nằm trong khoảng từ 00->99", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtNoiphattheBHYT.Focus();
                        txtNoiphattheBHYT.SelectAll();
                        return false;
                    }
                    if (Utility.Int32Dbnull(txtNoiphattheBHYT.Text, 0) <= 0)
                    {
                        Utility.ShowMsg("Mã nơi phát thẻ BHYT không được phép có chữ cái và phải nằm trong khoảng từ 00->99", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtNoiphattheBHYT.Focus();
                        txtNoiphattheBHYT.SelectAll();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtOthu4.Text))
                {
                    if (txtOthu4.Text.Length <= 1)
                    {
                        Utility.ShowMsg("Hai kí tự ô số 4 của mã bảo hiểm nằm trong khoảng từ 01->99", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu4.Focus();
                        txtOthu4.SelectAll();
                        return false;
                    }

                    if (Utility.Int32Dbnull(txtOthu4.Text, 0) <= 0)
                    {
                        Utility.ShowMsg("Hai kí tự ô số 4 của mã bảo hiểm không được phép có chữ cái và phải nằm trong khoảng từ 01->99", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu4.Focus();
                        txtOthu4.SelectAll();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtOthu5.Text))
                {
                    if (txtOthu5.Text.Length <= 2)
                    {
                        Utility.ShowMsg("3 kí tự ô số 5 của mã bảo hiểm nằm trong khoảng từ 001->999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu5.Focus();
                        txtOthu5.SelectAll();
                        return false;
                    }

                    if (Utility.Int32Dbnull(txtOthu5.Text, 0) <= 0)
                    {
                        Utility.ShowMsg("3 kí tự ô số 5 của mã bảo hiểm không được phép có chữ cái và phải nằm trong khoảng từ 001->999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu5.Focus();
                        txtOthu5.SelectAll();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtOthu6.Text))
                {
                    if (txtOthu6.Text.Length <= 4)
                    {
                        Utility.ShowMsg("5 kí tự ô số 6 của mã bảo hiểm nằm trong khoảng từ 00001->99999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu6.Focus();
                        txtOthu6.SelectAll();
                        return false;
                    }

                    if (Utility.Int32Dbnull(txtOthu6.Text, 0) <= 0)
                    {
                        Utility.ShowMsg("5 kí tự ô số 6 của mã bảo hiểm không được phép có chữ cái và phải nằm trong khoảng từ 00001->99999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu6.Focus();
                        txtOthu6.SelectAll();
                        return false;
                    }
                }
            }
            if (!string.IsNullOrEmpty(txtNoiDongtrusoKCBBD.Text))
            {
                if (txtNoiDongtrusoKCBBD.Text.Length <=1)
                {
                    Utility.ShowMsg("2 kí tự nơi đóng trụ sợ KCBBD phải nhập từ 01->99", "Thông báo",
                                    MessageBoxIcon.Information);
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.SelectAll();
                    return false;
                }

                if (Utility.Int32Dbnull(txtNoiDongtrusoKCBBD.Text, 0) <= 0)
                {
                    Utility.ShowMsg("2 kí tự nơi đóng trụ sợ KCBBD không được phép có chữ cái và phải nằm trong khoảng từ 01->99", "Thông báo",
                                    MessageBoxIcon.Information);
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.SelectAll();
                    return false;
                }

                SqlQuery sqlQuery = new Select().From(DmucDiachinh.Schema)
                    .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(txtNoiDongtrusoKCBBD.Text);
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg(
                        "Mã thành phố nơi đăng ký khám hiện không tồn tại trong CSDL\n Mời bạn liên hệ với quản trị mạng để nhập thêm",
                        "Thông báo", MessageBoxIcon.Information);
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.SelectAll();
                    return false;
                }
            }
            if (!string.IsNullOrEmpty( txtNoiDKKCBBD.Text))
            {
               
                SqlQuery sqlQuery = new Select().From(DmucNoiKCBBD.Schema)
                    .Where(DmucNoiKCBBD.Columns.MaKcbbd).IsEqualTo(txtNoiDKKCBBD.Text)
                    .And(DmucNoiKCBBD.Columns.MaDiachinh).IsEqualTo(txtNoiphattheBHYT.Text);
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg(
                        "Mã  nơi đăng ký khám hiện không tồn tại trong CSDL\n Mời bạn liên hệ với quản trị mạng để nhập thêm",
                        "Thông báo", MessageBoxIcon.Information);
                    txtNoiDKKCBBD.Focus();
                    txtNoiDKKCBBD.SelectAll();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// hàm thực hiện việc số thứ tự của BHYT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaQuyenloi_BHYT_TextChanged(object sender, EventArgs e)
        {
            if (_MaDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtMaQuyenloi_BHYT.Text.Length <= 0)
            {
                txtMaDtuong_BHYT.Focus();
                if (txtMaDtuong_BHYT.Text.Length > 0) txtMaDtuong_BHYT.Select(txtMaDtuong_BHYT.Text.Length, 0);
            }
            if (txtMaQuyenloi_BHYT.Text.Length < 1) return;
            if (!IsValidTheBHYT()) return;
            TinhPtramBHYT();
            txtNoiphattheBHYT.Focus();
            txtNoiphattheBHYT.SelectAll();
        }

        /// <summary>
        /// hàm thực hiện việc thay đổi thông tin của phần 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoiDongtrusoKCBBD_TextChanged(object sender, EventArgs e)
        {
            if (_MaDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtNoiDongtrusoKCBBD.Text.Length <= 0)
            {
                txtOthu6.Focus();
                if (txtOthu6.Text.Length > 0) txtOthu6.Select(txtOthu6.Text.Length, 0);
                return;
            }
            if (txtNoiDongtrusoKCBBD.Text.Length < 2) return;
            if (!IsValidTheBHYT()) return;
            LoadClinicCode();
            txtNoiDKKCBBD.Focus();
            txtNoiDKKCBBD.SelectAll();
        }

        private void txtOthu4_TextChanged(object sender, EventArgs e)
        {
            if (_MaDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtOthu4.Text.Length <= 0)
            {
                txtNoiphattheBHYT.Focus();
                if (txtNoiphattheBHYT.Text.Length > 0) txtNoiphattheBHYT.Select(txtNoiphattheBHYT.Text.Length, 0);
                return;
            }
            if (txtOthu4.Text.Length < 2) return;
            if (!IsValidTheBHYT()) return;
            txtOthu5.Focus();
            txtOthu5.SelectAll();
        }

        private void txtOthu5_TextChanged(object sender, EventArgs e)
        {
            if (_MaDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtOthu5.Text.Length <= 0)
            {
                txtOthu4.Focus();
                if (txtOthu4.Text.Length > 0) txtOthu4.Select(txtOthu4.Text.Length, 0);
                return;
            }
            if (txtOthu5.Text.Length < 3) return;
            if (!IsValidTheBHYT()) return;
            txtOthu6.Focus();
            txtOthu6.SelectAll();
        }

        private void txtOthu6_TextChanged(object sender, EventArgs e)
        {
            if (_MaDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtOthu6.Text.Length <= 0)
            {
                txtOthu5.Focus();
                if (txtOthu5.Text.Length > 0) txtOthu5.Select(txtOthu5.Text.Length, 0);
                return;
            }
            if (txtOthu6.Text.Length < 5) return;
            if (!IsValidTheBHYT()) return;
            txtNoiDongtrusoKCBBD.Focus();
            txtNoiDongtrusoKCBBD.SelectAll();
        }

        private void txtNoiphattheBHYT_TextChanged(object sender, EventArgs e)
        {
            if (_MaDoituongKcb == "DV") return;
            if (txtNoiphattheBHYT.Text.Length < 2)
            {
                Utility.SetMsg(lblNoiCapThe, "", false);
                return;
            }
            else
                GetNoiDangKy();
            if (!IsValidTheBHYT()) return;
            txtOthu4.Focus();
            txtOthu4.SelectAll();
            
        }

        private void GetNoiDangKy()
        {
            SqlQuery sqlQuery = new Select().From(DmucDiachinh.Schema)
                .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(txtNoiphattheBHYT.Text);
            var objDiachinh = sqlQuery.ExecuteSingle<DmucDiachinh>();
            if (objDiachinh != null)
            {
                Utility.SetMsg(lblNoiCapThe, Utility.sDbnull(objDiachinh.TenDiachinh), true);
                //LoadClinicCode();
            }
            else
            {
                lblNoiCapThe.Visible = false;
            }
        }

        private void txtNoiDKKCBBD_TextChanged(object sender, EventArgs e)
        {
            if (_MaDoituongKcb == "DV") return;
            if (txtNoiDKKCBBD.Text.Length < 3)
            {
                Utility.SetMsg(lblClinicName, "", false);
                return;
            }
            LoadClinicCode();
            if (lnkThem.Visible) lnkThem.Focus();
            else
                dtInsFromDate.Focus();
        }

        private void LaySoTheBHYT()
        {
            string SoBHYT = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text,
                                          txtNoiphattheBHYT.Text, txtOthu4.Text, txtOthu5.Text, txtOthu6.Text,
                                          txtNoiDongtrusoKCBBD.Text, txtNoiDKKCBBD.Text);
            GetSoBHYT = SoBHYT;
        }
        private string mathe_bhyt_full()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text,
                                          txtNoiphattheBHYT.Text, txtOthu4.Text, txtOthu5.Text, txtOthu6.Text,
                                          txtNoiDongtrusoKCBBD.Text, txtNoiDKKCBBD.Text);
           
        }

        private string Laymathe_BHYT()
        {
            string SoBHYT = string.Format("{0}{1}{2}{3}{4}{5}", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text,
                                          txtNoiphattheBHYT.Text, txtOthu4.Text, txtOthu5.Text, txtOthu6.Text);
            return SoBHYT;
        }

        /// <summary>
        /// hàm thực hiện việc tính phàn trăm bảo hiểm
        /// </summary>
        private void TinhPtramBHYT()
        {
            try
            {
                LaySoTheBHYT();
                if (!string.IsNullOrEmpty(Laymathe_BHYT()) && Laymathe_BHYT().Length >= 15)
                {
                    if ((!string.IsNullOrEmpty(GetSoBHYT)) && (!string.IsNullOrEmpty(txtNoiDKKCBBD.Text)))
                    {
                        var objLuotkham = new KcbLuotkham();
                        objLuotkham.MaNoicapBhyt = Utility.sDbnull(txtNoiphattheBHYT.Text);
                        objLuotkham.NoiDongtrusoKcbbd = Utility.sDbnull(txtNoiDongtrusoKCBBD.Text);
                        objLuotkham.MatheBhyt = Laymathe_BHYT();
                        objLuotkham.MaDoituongBhyt = txtMaDtuong_BHYT.Text;
                        objLuotkham.DungTuyen = !chkTraiTuyen.Visible ? 1 : (((byte?)(chkTraiTuyen.Checked ? 0 : 1)));
                        objLuotkham.MadtuongSinhsong = txtMaDTsinhsong.myCode;
                        objLuotkham.GiayBhyt = Utility.Bool2byte(chkGiayBHYT.Checked);
                        objLuotkham.MaKcbbd = Utility.sDbnull(txtNoiDKKCBBD.Text);
                        objLuotkham.IdDoituongKcb = _IdDoituongKcb;
                        objLuotkham.MaQuyenloi = Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text);
                        THU_VIEN_CHUNG.TinhPtramBHYT(objLuotkham);
                        txtPtramBHYT.Text = objLuotkham.PtramBhyt.ToString();
                        txtptramDauthe.Text = objLuotkham.PtramBhytGoc.ToString();
                    }
                    else
                    {
                        txtPtramBHYT.Text = "0";
                        txtptramDauthe.Text = "0";
                    }
                }
                else
                {
                    txtPtramBHYT.Text = "0";
                    txtptramDauthe.Text = "0";
                }
            }
            catch (Exception)
            {
                txtPtramBHYT.Text = "0";
                txtptramDauthe.Text = "0";
            }
            finally
            {
               
            }
        }

        /// <summary>
        /// hàm thực hiện việc load thông tin của nơi khám chữa bệnh ban đầu
        /// </summary>
        private void LoadClinicCode()
        {
            try
            {
                //Lấy mã Cơ sở KCBBD
                string v_CliniCode = txtNoiDongtrusoKCBBD.Text.Trim() + txtNoiDKKCBBD.Text.Trim();
                string strClinicName = "";
                DataTable dataTable = _KCB_DANGKY.GetClinicCode(v_CliniCode);
                if (dataTable.Rows.Count > 0)
                {
                    strClinicName = dataTable.Rows[0][DmucNoiKCBBD.Columns.TenKcbbd].ToString();
                    Utility.SetMsg(lblClinicName, strClinicName, !string.IsNullOrEmpty(txtNoiDKKCBBD.Text));
                }
                else
                {
                    Utility.SetMsg(lblClinicName, strClinicName, false);
                }
                lblClinicName.Visible = dataTable.Rows.Count > 0;
                lnkThem.Visible = dataTable.Rows.Count <= 0;
                //txtNamePresent.Text = strClinicName;
                //Check đúng tuyến cần lấy mã nơi cấp BHYT+mã kcbbd thay vì mã cơ sở kcbbd
                if (!chkCapCuu.Checked) //Nếu không phải trường hợp cấp cứu
                {
                    if (globalVariables.gv_intBHYT_TUDONGCHECKTRAITUYEN == 1)
                        //Nếu có chế độ tự động kiểm tra trái tuyến đúng tuyến
                        chkTraiTuyen.Checked =
                            !(THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiDongtrusoKCBBD.Text.Trim() +
                                                                    txtNoiDKKCBBD.Text.Trim()) ||
                              (!THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiDongtrusoKCBBD.Text.Trim() +
                                                                      txtNoiDKKCBBD.Text.Trim()) &&
                               chkChuyenVien.Checked));
                }
                else //Nếu là BN cấp cứu
                {
                    if (globalVariables.gv_intBHYT_TUDONGCHECKTRAITUYEN == 1)
                        //Nếu có chế độ tự động kiểm tra trái tuyến đúng tuyến
                        chkTraiTuyen.Checked =
                            (!(THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiDongtrusoKCBBD.Text.Trim() +
                                                                     txtNoiDKKCBBD.Text.Trim()) ||
                               (!THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiDongtrusoKCBBD.Text.Trim() +
                                                                       txtNoiDKKCBBD.Text.Trim()) &&
                                chkChuyenVien.Checked))) && (!chkCapCuu.Checked);
                }

                if (txtMaDTsinhsong.myCode != "-1")
                {
                    if (chkTraiTuyen.Checked)
                        chkTraiTuyen.Checked = false;
                }
                TinhPtramBHYT();
            }
            catch (Exception exception)
            {
            }
            finally
            {
                lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
            }
        }
              

        /// <summary>
        /// hàm thực hiện viecj tính tuổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNamSinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_NHAP_NGAYTHANGNAMSINH", false) == "1") return;
                if (txtNamSinh.Text.Length < 4) return;
                if (!string.IsNullOrEmpty(txtNamSinh.Text))
                {
                    txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(txtNamSinh.Text, 0));
                }
                else
                {
                    txtTuoi.Clear();
                }
                if (txtNamSinh.Focused)
                {
                    txtTuoi.Focus();
                    txtTuoi.SelectAll();
                }

                StatusControl();
            }
            catch (Exception exception)
            {
            }
        }

        private void txtTuoi_LostFocus(object sender, EventArgs e)
        {
            //txtNamSinh.TextChanged += new EventHandler(txtNamSinh_TextChanged);   
        }

        /// <summary>
        /// hàm thực hiện việc tính toán tuổi của bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTuoi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTuoi.Text))
                {
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_NHAP_NGAYTHANGNAMSINH", false) == "0")
                        txtNamSinh.Text = Utility.sDbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(txtTuoi.Text, 0));
                    else
                        dtpBOD.Value = new DateTime(Utility.Int32Dbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(txtTuoi.Text, 0)),dtpBOD.Value.Month, dtpBOD.Value.Day);
                }
            }
            catch (Exception exception)
            {
            }
        }

      

        private void SinhMaLanKham()
        {
            txtSolankham.Text = string.Empty;
            if (m_enAction == action.Insert)
            {
                txtMaBN.Text = "Tự sinh";
            }
            txtMaLankham.Text = THU_VIEN_CHUNG.KCB_SINH_MALANKHAM();
            m_strMaluotkham = txtMaLankham.Text;
            //Tạm bỏ
            //LaySoThuTuDoiTuong();
            SqlQuery sqlQuery = new Select(Aggregate.Max(KcbLuotkham.Columns.SolanKham)).From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtMaBN.Text, -1));
            var SoThuTuKham = sqlQuery.ExecuteScalar<Int32>();
            txtSolankham.Text = Utility.sDbnull(SoThuTuKham + 1);
        }
        /// <summary>
        /// Hàm này hơi vô nghĩa vì số lần khám tính theo id_benhnhan
        /// </summary>
        private void LaySoThuTuDoiTuong()
        {
            txtSolankham.Text =
                THU_VIEN_CHUNG.LaySTTKhamTheoDoituong(_IdDoituongKcb).ToString();
        }

        /// <summary>
        /// hàm thực hiện việc lọc thông tin nhanh của quận huyện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaQuan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //string _rowFilter = "1=1";
                //if (!string.IsNullOrEmpty(txtMaQuan.Text))
                //{
                //    _rowFilter = string.Format("{0}='{1}'", DmucDiachinh.Columns.MaDiachinh, txtMaQuan.Text.Trim());
                //}
                //mdt_DataQuyenhuyen.DefaultView.RowFilter = _rowFilter;
                //mdt_DataQuyenhuyen.AcceptChanges();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// hàm thực hiện việc làm sách thông tin của bệnh nhân
        /// </summary>
        private void ClearControl()
        {
            Utility.SetMsg(lblMsg, "", false);
            objBuonggiuong = null;
            objLuotkham = null;
            LstNoitruPhanbuonggiuong = new NoitruPhanbuonggiuongCollection();
            m_blnHasJustInsert = false;
            txtSolankham.Text = "1";
            txtTEN_BN.Clear();
            txtNamSinh.Clear();
            dtpBOD.Value = globalVariables.SysDate;
            txtTuoi.Clear();
            txtCMT.Clear();
            txtNgheNghiep.Clear();
            txtDiachi.Clear();
            txtDantoc.Clear();
            txtTrieuChungBD.Clear();
            txtSoDT.Clear();
            chkChuyenVien.Checked = false;
            txtNoichuyenden.SetCode("-1");
            txtKhoanoitru.SetCode("-1");
            txtGia.SetCode("-1");
            txtRoom_code.SetCode("-1");
            txtBedCode.SetCode("-1");
            if (m_dtDataRoom != null) m_dtDataRoom.Clear();
            if (m_dtDatabed != null) m_dtDatabed.Clear();
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
               this.Text= "Bệnh nhân đang khám dịch vụ ngoài giờ";
            }
            ModifyCommand();
            AllowTextChanged = false;
            XoathongtinBHYT(true);

            _MaDoituongKcb = Utility.sDbnull(cboDoituongKCB.SelectedValue);
            objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
            if (objDoituongKCB == null) return;
            _IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
            _IdLoaidoituongKcb = objDoituongKCB.IdLoaidoituongKcb;
            _TenDoituongKcb = objDoituongKCB.TenDoituongKcb;
            PtramBhytCu = objDoituongKCB.PhantramTraituyen.Value;
            PtramBhytGocCu = PtramBhytCu;
            txtPtramBHYT.Text = objDoituongKCB.PhantramTraituyen.ToString();
            txtptramDauthe.Text = objDoituongKCB.PhantramTraituyen.ToString();
            if (objDoituongKCB.IdLoaidoituongKcb == 0)//ĐỐi tượng BHYT
            {
                pnlBHYT.Enabled = true;
                lblPtram.Text = "Phần trăm BHYT";
                TinhPtramBHYT();
                txtMaDtuong_BHYT.SelectAll();
                txtMaDtuong_BHYT.Focus();
            }
            else//Đối tượng khác BHYT
            {
                pnlBHYT.Enabled = false;
                lblPtram.Text = "P.trăm giảm giá";
                txtTEN_BN.Focus();
            }

            chkTraiTuyen.Checked = false;
            lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
            lblPtramdauthe.Visible = objDoituongKCB.IdLoaidoituongKcb == 0;
            txtptramDauthe.Visible = objDoituongKCB.IdLoaidoituongKcb == 0;
            chkChuyenVien.Checked = false;
            chkCapCuu.Checked = false;
            txtPtramBHYT.Text = "0";
            txtptramDauthe.Text = "0";
            AllowTextChanged = true;
            //Chuyển về trạng thái thêm mới
            m_enAction = action.Insert;
            if (PropertyLib._KCBProperties.SexInput) cboPatientSex.SelectedIndex = -1;
            lnkThem.Visible = false;
            SinhMaLanKham();
            txtSoBenhAn.Text = THU_VIEN_CHUNG.LaySoBenhAn();
            m_dataDataRegExam.Clear();
            if (pnlBHYT.Enabled)
            {
                lblPtram.Text = "Phần trăm BHYT";
                txtMaDtuong_BHYT.Focus();
            }
            else
            {
                lblPtram.Text = "P.trăm giảm giá";
                PtramBhytCu = objDoituongKCB.PhantramTraituyen.Value;
                PtramBhytGocCu = PtramBhytCu;
                txtPtramBHYT.Text = objDoituongKCB.PhantramTraituyen.ToString();
                txtptramDauthe.Text = objDoituongKCB.PhantramTraituyen.ToString();
                txtTEN_BN.Focus();
            }
            if (m_enAction == action.Insert)
            {
                dtpInputDate.Value = globalVariables.SysDate;
                dtCreateDate.Value = globalVariables.SysDate;
                dtInsFromDate.Value = new DateTime(globalVariables.SysDate.Year, 1, 1);
                dtInsToDate.Value = new DateTime(globalVariables.SysDate.Year, 12, 31);
            }
            SetActionStatus();
           
        }

        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            //Cập nhật lại mã lượt khám chưa dùng tới trong trường hợp nhấn New liên tục
            new Update(KcbDmucLuotkham.Schema)
                      .Set(KcbDmucLuotkham.Columns.TrangThai).EqualTo(0)
                      .Set(KcbDmucLuotkham.Columns.UsedBy).EqualTo(DBNull.Value)
                      .Set(KcbDmucLuotkham.Columns.StartTime).EqualTo(DBNull.Value)
                      .Set(KcbDmucLuotkham.Columns.EndTime).EqualTo(null)
                      .Where(KcbDmucLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.Int32Dbnull(m_strMaluotkham, "-1"))
                      .And(KcbDmucLuotkham.Columns.TrangThai).IsEqualTo(1)
                      .And(KcbDmucLuotkham.Columns.UsedBy).IsEqualTo(globalVariables.UserName)
                      .And(KcbDmucLuotkham.Columns.Nam).IsEqualTo(globalVariables.SysDate.Year).Execute();
            ;

            ClearControl();
        }

        /// <summary>
        /// hàm thực hiện viecj thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// hàm thực hiện việc lưu thông tin của đối tượng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isExceedData())
                {
                    Utility.ShowMsg("Phiên bản Demo chỉ cho phép bạn tiếp đón tối đa 1500 lượt khám. Mời bạn liên hệ để được trợ giúp");
                    return;
                }
                cmdSave.Enabled = false;
                
                PerformAction();
                cmdSave.Enabled = true;
            }
            catch
            {
            }
            finally
            {
                cmdSave.Enabled = true;
            }
        }
        bool isExceedData()
        {
            try
            {
                if (PropertyLib._ConfigProperties.HIS_AppMode != VNS.Libs.AppType.AppEnum.AppMode.License)
                {
                    KcbLuotkhamCollection lst = new Select().From(KcbLuotkham.Schema).ExecuteAsCollection<KcbLuotkhamCollection>();
                    return lst.Count >= 1500;
                }
                return false;
            }
            catch(Exception ex)
            {
                Utility.CatchException("isExceedData()-->",ex);
                return true;
            }
        }
        private void StatusControl()
        {
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
                this.Text = "Bệnh nhân đang khám dịch vụ ngoài giờ";
            }
            
        }

        private bool IsValidData()
        {
            if (m_enAction==action.Insert && dtCreateDate.Value.ToString("dd/MM/yyyy") != globalVariables.SysDate.ToString("dd/MM/yyyy"))
            {
                if (!Utility.AcceptQuestion("Ngày tiếp đón khác ngày hiện tại. Bạn có chắc chắn hay không?","Cảnh báo",true))
                {
                    dtCreateDate.Focus();
                    return false;
                }
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(_IdLoaidoituongKcb))
            {
                if (!IsValidBHYT()) return false;
                if (!IsValidTheBHYT()) return false;
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(objDoituongKCB.IdLoaidoituongKcb))
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BHYT", "0", false) == "1")
                {
                    if (Utility.DoTrim(txtDiachi_bhyt.Text)=="")
                    {
                        Utility.SetMsg(lblMsg, "Bạn phải nhập địa chỉ thẻ BHYT", true);
                        txtDiachi_bhyt.Focus();
                        return false;
                    }
                }
                if (Utility.DoTrim(txtMaDTsinhsong.Text) != "" && txtMaDTsinhsong.myCode == "-1")
                {
                    Utility.SetMsg(lblMsg, "Mã đối tượng sinh sống chưa đúng. Mời bạn nhập lại", true);
                    txtMaDTsinhsong.SelectAll();
                    txtMaDTsinhsong.Focus();
                    return false;
                }

                
            }
            if (chkChuyenVien.Checked)
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BATNHAPNOICHUYENDEN", "0", false) == "1")
                {
                    if (txtNoichuyenden.MyCode == "-1")
                    {
                        Utility.SetMsg(lblMsg, "Bạn phải nhập bệnh viện chuyển đến", true);
                        txtNoichuyenden.SelectAll();
                        txtNoichuyenden.Focus();
                        return false;
                    }
                }
            }
            if (string.IsNullOrEmpty(txtTEN_BN.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập tên Bệnh nhân", true);
                txtTEN_BN.Focus();
                return false;
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_NHAP_NGAYTHANGNAMSINH", false) == "0" && string.IsNullOrEmpty(txtNamSinh.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập ngày tháng năm sinh, hoặc năm sinh cho bệnh nhân ", true);
                txtNamSinh.Focus();
                return false;
            }
            if (cboPatientSex.SelectedIndex<0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn giới tính của Bệnh nhân",true);
                cboPatientSex.Focus();
                return false;
            }

            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BENHNHAN", "0", false) == "1")
                {
                    if (Utility.DoTrim(txtDiachi.Text) == "")
                    {
                        Utility.SetMsg(lblMsg, "Bạn phải nhập địa chỉ Bệnh nhân", true);
                        txtDiachi.Focus();
                        return false;
                    }
                }
            if (txtKhoanoitru.MyCode=="-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn khoa nội trú nhập viện", true);
                txtKhoanoitru.Focus();
                return false;
            }

            if (txtRoom_code.MyCode != "-1" && txtBedCode.MyCode != "-1" && txtGia.MyCode=="-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn giá buồng giường", true);
                txtGia.Focus();
                return false;
            }
            if (txtRoom_code.MyCode == "-1" || txtBedCode.MyCode == "-1" )
            {
                if (!Utility.AcceptQuestion("Chú ý: Bạn chưa phân buồng giường cho Bệnh nhân. Nhấn No để quay lại phân buồng giường, nhấn YES để tiếp tục lưu Bệnh nhân mà không có thông tin Buồng giường", "Cảnh báo chưa phân buồng giường cho BN cấp cứu", true))
                {
                    if (txtRoom_code.MyCode == "-1")
                    {
                        txtRoom_code.Focus();
                    }
                    else if (txtBedCode.MyCode == "-1")
                    {
                        txtBedCode.Focus();
                    }
                    return false;
                }
            }
            return isValidIdentifyNum();
        }

        /// <summary>
        /// hàm thực hiện viecj kiểm tra thông tin cảu đối tượng bảo hiểm
        /// </summary>
        /// <returns></returns>
        private bool IsValidBHYT()
        {
            if (string.IsNullOrEmpty(txtMaDtuong_BHYT.Text))
            {
                Utility.ShowMsg("Bạn phải nhập đối tượng đầu thẻ cho bảo hiểm không bỏ trống", "Thông báo",
                                MessageBoxIcon.Information);
                txtMaDtuong_BHYT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMaQuyenloi_BHYT.Text))
            {
                Utility.ShowMsg("Bạn phải nhập mã quyền lợi cho bảo hiểm không bỏ trống", "Thông báo");
                txtMaQuyenloi_BHYT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNoiDongtrusoKCBBD.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 3  cho bảo hiểm không bỏ trống","Thông báo");
                txtNoiDongtrusoKCBBD.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtOthu4.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 4  cho bảo hiểm không bỏ trống", "Thông báo");
                txtOthu4.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtOthu5.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 5  cho bảo hiểm không bỏ trống", "Thông báo");
                txtOthu5.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtOthu6.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 6  cho bảo hiểm không bỏ trống", "Thông báo");
                txtOthu6.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNoiphattheBHYT.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi cấp thẻ  cho bảo hiểm không bỏ trống", "Thông báo",
                                MessageBoxIcon.Information);
                txtNoiphattheBHYT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNoiDKKCBBD.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký khám chữa bệnh ban đầu cho bảo hiểm không bỏ trống",
                                "Thông báo");
                txtNoiDKKCBBD.Focus();
                return false;
            }
            if (dtInsToDate.Value < dtInsFromDate.Value)
            {
                Utility.ShowMsg("Ngày hết hạn thẻ BHYT phải lớn hơn hoặc bằng ngày đăng ký thẻ BHYT", "Thông báo");
                dtInsToDate.Focus();
                return false;
            }
            if (dtInsToDate.Value < globalVariables.SysDate)
            {
                Utility.ShowMsg("Ngày hết hạn thẻ BHYT phải lớn hơn hoặc bằng ngày hiện tại", "Thông báo");
                dtInsToDate.Focus();
                return false;
            }
            return true;
        }

        private void  ModifyCommand()
        {
            cmdSave.Enabled = Utility.DoTrim(txtTEN_BN.Text).Length > 0;
        }
        private void PerformAction()
        {
            if (!IsValidData()) return;
            switch (m_enAction)
            {
                case action.Update:
                    if (!IsValid4Update()) return;
                    CapnhatthongtinBenhnhan();
                    break;
                case action.Insert:
                    InsertPatient();
                    break;
                case action.Add:
                    ThemLanKham();
                    break;
            }
           
            ModifyCommand();
        }

        private bool IsValid4Update()
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaLankham.Text))
                {
                    Utility.ShowMsg("Mã lần khám không bỏ trống", "Thông báo", MessageBoxIcon.Error);
                    txtMaLankham.Focus();
                    txtMaLankham.SelectAll();
                    return false;
                }
                SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtMaLankham.Text));
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg("Mã lần khám này không tồn tại trong CSDL,Mời bạn xem lại", "Thông báo",
                                    MessageBoxIcon.Error);
                    txtMaLankham.Focus();
                    txtMaLankham.SelectAll();
                    return false;
                }
                //Kiểm tra xem có thay đổi phần trăm BHYT
                if (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) != Utility.DecimaltoDbnull(txtPtramBHYT.Text))
                {
                    KcbThanhtoanCollection _lstthanhtoan = new Select().From(KcbThanhtoan.Schema)
                        .Where(KcbThanhtoan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(KcbThanhtoan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .And(KcbThanhtoan.Columns.PtramBhyt).IsEqualTo(Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0))
                        .ExecuteAsCollection<KcbThanhtoanCollection>();
                    if (_lstthanhtoan.Count > 0)
                    {
                        Utility.ShowMsg(string.Format("Bệnh nhân này đã thanh toán với mức BHYT {0}. Do đó hệ thống không cho phép bạn thay đổi phần trăm BHYT.\nMuốn thay đổi đề nghị bạn hủy hết các thanh toán",Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0).ToString()));
                        return false;
                    }
                }
                if (LstNoitruPhanbuonggiuong != null && LstNoitruPhanbuonggiuong.Count > 1)
                {
                    Utility.SetMsg(lblMsg, "Bệnh nhân đã chuyển khoa hoặc chuyển giường nên bạn không thể thay đổi thông tin Khoa - Buồng - Giường nội trú", true);
                    return false;
                }
                if (objLuotkham.SoBenhAn != txtSoBenhAn.Text)
                {
                    if (!Utility.AcceptQuestion(string.Format( "Số bệnh án nội trú cũ {0} khác với số BA nội trú mới bạn vừa thay đổi. Bạn có chắc chắn lấy số BA mới này để cập nhật cho Bệnh nhân hay không?\nNhấn Yes để đồng ý. Nhấn No để lưu Bệnh nhân với số Bệnh án cũ",objLuotkham.SoBenhAn), "Cảnh báo thay đổi số Bệnh án nội trú", true))
                    {
                        txtSoBenhAn.Text = objLuotkham.SoBenhAn;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi kiểm tra hợp lệ dữ liệu trước khi cập nhật Bệnh nhân",ex);
                return false;
            }
        }

       
        private void txtTuoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab) cboPatientSex.Focus();
        }

        /// <summary>
        /// ham thwucj hiện việc chọn thông tin tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>



        void ChangeObjectRegion()
        {
            if (objDoituongKCB == null) return;
            _IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
            _IdLoaidoituongKcb = objDoituongKCB.IdLoaidoituongKcb;
            _TenDoituongKcb = objDoituongKCB.TenDoituongKcb;
            PtramBhytCu = objDoituongKCB.PhantramTraituyen.Value;
            PtramBhytGocCu = PtramBhytCu;
            txtPtramBHYT.Text = objDoituongKCB.PhantramTraituyen.ToString();
            txtptramDauthe.Text = objDoituongKCB.PhantramTraituyen.ToString();
            if (objDoituongKCB.IdLoaidoituongKcb == 0)//ĐỐi tượng BHYT
            {
                pnlBHYT.Enabled = true;
                lblPtram.Text = "Phần trăm BHYT";
                TinhPtramBHYT();
                lblTuyenBHYT.Visible = true;
                txtMaDtuong_BHYT.SelectAll();
                txtMaDtuong_BHYT.Focus();
            }
            else//Đối tượng khác BHYT
            {
                lblTuyenBHYT.Visible = false;
                pnlBHYT.Enabled = false;
                lblPtram.Text = "P.trăm giảm giá";
                XoathongtinBHYT(PropertyLib._KCBProperties.XoaBHYT);
                txtTEN_BN.Focus();
            }
        }
        DmucDoituongkcb objDoituongKCB = null;
        /// <summary>
        /// hàm thực hienej phím tắt của form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Taobenhnhancapcuu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (this.ActiveControl != null && this.ActiveControl.Name == txtTEN_BN.Name && Utility.DoTrim(txtTEN_BN.Text)!="")
                {
                    frm_DSACH_BN_TKIEM Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM();
                    Timkiem_Benhnhan.AutoSearch = true;
                    Timkiem_Benhnhan.FillAndSearchData(false, "", "", Utility.DoTrim(txtTEN_BN.Text), "", "", "-1");
                    if (Timkiem_Benhnhan.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        txtNoiDKKCBBD.Clear();
                        txtNoiphattheBHYT.Clear();
                        isAutoFinding = true;
                        FindPatient(Timkiem_Benhnhan.IdBenhnhan.ToString());
                        isAutoFinding = false;
                    }
                }
                else if (this.ActiveControl != null && this.ActiveControl.Name == txtCMT.Name && Utility.DoTrim(txtCMT.Text) != "")
                {
                    frm_DSACH_BN_TKIEM Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM();
                    Timkiem_Benhnhan.AutoSearch = true;
                    Timkiem_Benhnhan.FillAndSearchData(false, "", "", "", Utility.DoTrim(txtCMT.Text), "", "-1");
                    if (Timkiem_Benhnhan.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        txtNoiDKKCBBD.Clear();
                        txtNoiphattheBHYT.Clear();
                        isAutoFinding = true;
                        FindPatient(Timkiem_Benhnhan.IdBenhnhan.ToString());
                        isAutoFinding = false;
                    }
                }
                else if (this.ActiveControl != null && this.ActiveControl.Name == txtSoDT.Name && Utility.DoTrim(txtSoDT.Text) != "")
                {
                    frm_DSACH_BN_TKIEM Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM();
                    Timkiem_Benhnhan.AutoSearch = true;
                    Timkiem_Benhnhan.FillAndSearchData(false, "", "", "", "", Utility.DoTrim(txtSoDT.Text), "-1");
                    if (Timkiem_Benhnhan.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        txtNoiDKKCBBD.Clear();
                        txtNoiphattheBHYT.Clear();
                        isAutoFinding = true;
                        FindPatient(Timkiem_Benhnhan.IdBenhnhan.ToString());
                        isAutoFinding = false;
                    }
                }
                return;
            }

            if (e.Control && e.KeyCode == Keys.D)
            {
               
                _MaDoituongKcb = "DV";
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                return;
            }
            if (e.Control && e.KeyCode == Keys.B)
            {
                _MaDoituongKcb = "BHYT";
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                return;
            }
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.P))
            {
                AllowTextChanged = false;
                txtDiachi._Text = txtDiachi_bhyt.Text;
                AllowTextChanged = true;
                return;
            }
            
            string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");
            if (e.Control && e.KeyCode == Keys.K)
            {
                if (!NotPayment(txtMaBN.Text.Trim(), ref ngay_kham))
                {
                    m_enAction = action.Add;
                    SinhMaLanKham();
                    txtKhoanoitru.Focus();
                }
                else
                {
                    //nếu là ngày hiện tại thì đặt về trạng thái sửa
                    if (ngay_kham == globalVariables.SysDate.ToString("dd/MM/yyyy"))
                    {
                        Utility.ShowMsg(
                            "Bệnh nhân đang có lần khám chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Bệnh nhân trước khi thêm lần khám mới.Nhấn OK để hệ thống quay về trạng thái sửa thông tin BN");
                        m_enAction = action.Update;
                        AllowTextChanged = false;
                        LoadThongtinBenhnhan();
                        txtTEN_BN.Focus();
                    }
                    else //Không cho phép thêm lần khám khác nếu chưa thanh toán lần khám của ngày hôm trước
                    {
                        Utility.ShowMsg(
                            "Bệnh nhân đang có lần khám chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Bệnh nhân trước khi thêm lần khám mới. Nhấn OK để hệ thống chuyển về trạng thái thêm mới Bệnh nhân");
                        cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                    }
                }
                return;
            }
            if (e.Control && e.KeyCode == Keys.F)
            {
                txtMaBN.SelectAll();
                txtMaBN.Focus();
            }
            
            if (e.KeyCode == Keys.F1)
            {
                tabControl1.SelectedTab = tabControl1.TabPages[0];
                return;
            }
            if (e.KeyCode == Keys.F2)
            {
                tabControl1.SelectedTab = tabControl1.TabPages[1];
                return;
            }
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);
            if (e.KeyCode == Keys.Escape && this.ActiveControl != null && this.ActiveControl.GetType()!=txtDantoc.GetType())
            {

                Close();
            }
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.N && e.Control) cmdThemMoiBN.PerformClick();
            if (this.ActiveControl != null && this.ActiveControl.Name != ucTamung1.Name && e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }


        private void cboThanhPho_SelectedIndexChanged_1(object sender, EventArgs e)
        {
        }

        private void txtTEN_BN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cmdSave.Enabled = Utility.DoTrim(txtTEN_BN.Text).Length > 0;
            }
            catch (Exception exception)
            {
            }
        }
        private void txtNamSinh_LostFocus(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_NHAP_NGAYTHANGNAMSINH", false) == "1") return;
            if (!string.IsNullOrEmpty(txtNamSinh.Text))
            {
                if (txtNamSinh.Text.Length < 4)
                {
                    Utility.ShowMsg("Năm sinh của bệnh nhân phải là 4 số", "Thông báo", MessageBoxIcon.Information);
                    txtNamSinh.Focus();
                    txtNamSinh.SelectAll();
                }
            }
        }

        private void txtTuoi_Click(object sender, EventArgs e)
        {
        }
        private void txtMaQuyenloi_BHYT_Click(object sender, EventArgs e)
        {
        }

        private void lnkCungDC_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AllowTextChanged = false;
            txtDiachi._Text = txtDiachi_bhyt.Text;
            AllowTextChanged = true;
        }

        private void txtCMT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter && txtCMT.Text.Trim() != "")
            {
                FindPatientIDbyCMT(txtCMT.Text.Trim());
            }
        }

        private void txtMaDtuong_BHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiDongtrusoKCBBD.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                if (MA_BHYT.Length == 15) FindPatientIDbyBHYT(MA_BHYT);
            }
        }

        private void txtMaQuyenloi_BHYT_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                if (MA_BHYT.Length == 15) FindPatientIDbyBHYT(MA_BHYT);
                return;
            }
            if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtMaQuyenloi_BHYT.Text.Length <= 0)
                {
                    txtMaDtuong_BHYT.Focus();
                    txtMaDtuong_BHYT.Select(txtMaDtuong_BHYT.Text.Length, 0);
                }
                return;
            }
            if (txtMaQuyenloi_BHYT.Text.Length == 1 && (Char.IsDigit((char) e.KeyCode) || Char.IsLetter((char) e.KeyCode)))
            {
                if (txtNoiphattheBHYT.Text.Length > 0)
                {
                    // txtNoiDongtrusoKCBBD.Text = ((char)e.KeyCode).ToString() + txtNoiDongtrusoKCBBD.Text.Substring(1);
                    txtNoiphattheBHYT.Focus();
                    txtNoiphattheBHYT.SelectAll();
                }
                return;
            }
            
        }

        private void txtNoiDongtrusoKCBBD_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
               
            }
            else if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtNoiDongtrusoKCBBD.Text.Length <= 0)
                {
                    txtOthu6.Focus();
                    txtOthu6.Select(txtOthu6.Text.Length, 0);
                }
            }
        }

        private void txtOthu4_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                if (MA_BHYT.Length == 15) FindPatientIDbyBHYT(MA_BHYT);
                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtOthu4.Text.Length <= 0)
                {
                    txtNoiphattheBHYT.Focus();
                    txtNoiphattheBHYT.Select(txtNoiphattheBHYT.Text.Length, 0);
                }
            }
        }

        private void txtOthu5_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                if (MA_BHYT.Length == 15) FindPatientIDbyBHYT(MA_BHYT);
            }
            else if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtOthu5.Text.Length <= 0)
                {
                    txtOthu4.Focus();
                    txtOthu4.Select(txtOthu4.Text.Length, 0);
                }
            }
        }

        private void txtOthu6_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                if (MA_BHYT.Length == 15) FindPatientIDbyBHYT(MA_BHYT);
                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtOthu6.Text.Length <= 0)
                {
                    txtOthu5.Focus();
                    txtOthu5.Select(txtOthu5.Text.Length, 0);
                }
            }
        }

        private void lnkThem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newItem = new frm_ThemnoiKCBBD();
            newItem.m_dtDataThanhPho = globalVariables.gv_dtDmucDiachinh;
            newItem.SetInfor(txtNoiDKKCBBD.Text, txtNoiphattheBHYT.Text);
            if (newItem.ShowDialog() == DialogResult.OK)
            {
                txtNoiDKKCBBD.Text = "";
                txtNoiphattheBHYT.Text = "";
                txtNoiDKKCBBD.Text = newItem.txtMa.Text.Trim();
                txtNoiphattheBHYT.Text = newItem.txtMaThanhPho.Text.Trim();
                dtInsFromDate.Focus();
            }
        }

        private void txtKieuKham_Enter(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
                this.Text= "Bệnh nhân đang khám dịch vụ ngoài giờ";
            }
            
        }

        private void txtPhongkham_Enter(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
                this.Text = "Bệnh nhân đang khám dịch vụ ngoài giờ";
            }
           
        }

        private bool isQMSActive(string name)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void frm_Taobenhnhancapcuu_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Utility.FreeLockObject(m_strMaluotkham);
                //Trả lại mã lượt khám nếu chưa được dùng đến
                new Update(KcbDmucLuotkham.Schema)
                       .Set(KcbDmucLuotkham.Columns.TrangThai).EqualTo(0)
                       .Set(KcbDmucLuotkham.Columns.UsedBy).EqualTo(DBNull.Value)
                       .Set(KcbDmucLuotkham.Columns.StartTime).EqualTo(DBNull.Value)
                       .Set(KcbDmucLuotkham.Columns.EndTime).EqualTo(null)
                       .Where(KcbDmucLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.Int32Dbnull( m_strMaluotkham,"-1"))
                       .And(KcbDmucLuotkham.Columns.TrangThai).IsEqualTo(1)
                       .And(KcbDmucLuotkham.Columns.UsedBy).IsEqualTo(globalVariables.UserName)
                       .And(KcbDmucLuotkham.Columns.Nam).IsEqualTo(globalVariables.SysDate.Year).Execute();
                       ;
               
            }
            catch (Exception exception)
            {
            }
        }

      

     
        void SetActionStatus()
        {
           this.Text = m_enAction == action.Insert ? "BỆNH NHÂN MỚI" : (m_enAction==action.Add?"THÊM LẦN KHÁM":"CẬP NHẬT");
        }
        private void CauHinhKCB()
        {
            dtpBOD.Value = globalVariables.SysDate;
            dtpBOD.Visible=THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_NHAP_NGAYTHANGNAMSINH", false) == "1";
            txtNamSinh.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_NHAP_NGAYTHANGNAMSINH", false) == "0";
            if (dtpBOD.Visible)
                txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - dtpBOD.Value.Year);
           
        }

        private void txtSoDT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtKhoanoitru.Focus();
        }

        #region "Sự kiện bắt cho phần khám bệnh"
                     
        #endregion

        #region "khởi tạo sự kiện để lưu lại thông tin của bệnh nhân"

        private string mavuasinh = "";

        private void ThemMoiLanKhamVaoLuoi()
        {
            try
            {
                DataRow dr = m_dtPatient.NewRow();
                dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan] = objBenhnhan.IdBenhnhan;
                dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan] = objBenhnhan.TenBenhnhan;

                dr[KcbDanhsachBenhnhan.Columns.GioiTinh] = objBenhnhan.GioiTinh;
                dr[KcbDanhsachBenhnhan.Columns.IdGioitinh] = objBenhnhan.IdGioitinh;

                dr[KcbDanhsachBenhnhan.Columns.NamSinh] = objBenhnhan.NamSinh;

                dr[KcbLuotkham.Columns.MatheBhyt] = objLuotkham.MatheBhyt;
                dr[KcbDanhsachBenhnhan.Columns.DiaChi] = objLuotkham.DiaChi;
                dr["SO_HSBA"] = objLuotkham.MaLuotkham;
                dr["Tuoi"] = Utility.Int32Dbnull(txtTuoi.Text, 0);
                dr[KcbLuotkham.Columns.NgayTiepdon] = dtCreateDate.Value;
                dr[KcbLuotkham.Columns.NgayRavien] = null;
                dr[KcbLuotkham.Columns.NgayNhapvien] = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                               dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                               Utility.Int32Dbnull(txtPhut.Text), 00);
                dr[KcbLuotkham.Columns.IdKhoanoitru] = objLuotkham.IdKhoanoitru;
                dr[KcbLuotkham.Columns.IdDoituongKcb] = objLuotkham.IdDoituongKcb;
                dr[KcbLuotkham.Columns.MaLuotkham] = objLuotkham.MaLuotkham;
                dr[KcbLuotkham.Columns.MaDoituongKcb] = objLuotkham.MaDoituongKcb;
                dr["Id"] = objBuonggiuong != null ? objBuonggiuong.Id : -1;
                dr[KcbLuotkham.Columns.TrangthaiNoitru] = 1;
                dr[KcbLuotkham.Columns.TthaiThopNoitru] = 0;
                dr[KcbLuotkham.Columns.TrangthaiCapcuu] = 1;
                dr[KcbLuotkham.Columns.IdBuong] = objBuonggiuong != null ? objBuonggiuong.IdBuong : -1;
                dr[KcbLuotkham.Columns.IdGiuong] = objBuonggiuong != null ? objBuonggiuong.IdGiuong : -1;

                dr["ten_doituong_kcb"] = objDoituongKCB != null ? objDoituongKCB.TenDoituongKcb : "";
                dr["ten_trangthai_kcb"] = "Nội trú";
                dr["ten_buong"] = Utility.GetValueFromGridColumn(grdBuong, NoitruDmucBuong.Columns.TenBuong);
                dr["ten_giuong"] = Utility.GetValueFromGridColumn(grdGiuong, NoitruDmucGiuongbenh.Columns.TenGiuong);
                dr["ten_khoanoitru"] = txtKhoanoitru.Text;
                dr["songay"] = 0;
                dr["id_chuyen"] = -1;
                m_dtPatient.Rows.InsertAt(dr, 0);
            }
            catch (Exception)
            {
                
               
            }
            
        }

        private void UpdateBNVaoTrenLuoi()
        {
            try
            {
                EnumerableRowCollection<DataRow> query = from bn in m_dtPatient.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(bn[KcbLuotkham.Columns.MaLuotkham]) ==
                                                             txtMaLankham.Text
                                                         select bn;
                if (query.Count() > 0)
                {
                    DataRow dr = query.FirstOrDefault();
                    dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan] = objBenhnhan.IdBenhnhan;
                    dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan] = objBenhnhan.TenBenhnhan;

                    dr[KcbDanhsachBenhnhan.Columns.GioiTinh] = objBenhnhan.GioiTinh;
                    dr[KcbDanhsachBenhnhan.Columns.IdGioitinh] = objBenhnhan.IdGioitinh;

                    dr[KcbDanhsachBenhnhan.Columns.NamSinh] = objBenhnhan.NamSinh;

                    dr[KcbLuotkham.Columns.MatheBhyt] = objLuotkham.MatheBhyt;
                    dr[KcbDanhsachBenhnhan.Columns.DiaChi] = objLuotkham.DiaChi;
                    dr["SO_HSBA"] = objLuotkham.MaLuotkham;
                    dr["Tuoi"] = Utility.Int32Dbnull(txtTuoi.Text, 0);
                    dr[KcbLuotkham.Columns.NgayTiepdon] = dtCreateDate.Value;
                    dr[KcbLuotkham.Columns.NgayRavien] = null;
                    dr[KcbLuotkham.Columns.NgayNhapvien] = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                                   dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                                   Utility.Int32Dbnull(txtPhut.Text), 00);
                    dr[KcbLuotkham.Columns.IdKhoanoitru] = objLuotkham.IdKhoanoitru;
                    dr[KcbLuotkham.Columns.IdDoituongKcb] = objLuotkham.IdDoituongKcb;
                    dr[KcbLuotkham.Columns.MaLuotkham] = objLuotkham.MaLuotkham;
                    dr[KcbLuotkham.Columns.MaDoituongKcb] = objLuotkham.MaDoituongKcb;
                   
                    dr["Id"] = objBuonggiuong != null ? objBuonggiuong.Id : -1;
                    dr[KcbLuotkham.Columns.TrangthaiNoitru] = 1;
                    dr[KcbLuotkham.Columns.TthaiThopNoitru] = 0;
                    dr[KcbLuotkham.Columns.TrangthaiCapcuu] = 1;
                    dr[KcbLuotkham.Columns.IdBuong] = objBuonggiuong != null ? objBuonggiuong.IdBuong : -1;
                    dr[KcbLuotkham.Columns.IdGiuong] = objBuonggiuong != null ? objBuonggiuong.IdGiuong : -1;

                    dr["ten_doituong_kcb"] = objDoituongKCB != null ? objDoituongKCB.TenDoituongKcb : "";
                    dr["ten_trangthai_kcb"] = "Nội trú";
                    dr["ten_buong"] = Utility.GetValueFromGridColumn(grdBuong, NoitruDmucBuong.Columns.TenBuong);
                    dr["ten_giuong"] = Utility.GetValueFromGridColumn(grdGiuong, NoitruDmucGiuongbenh.Columns.TenGiuong);
                    dr["ten_khoanoitru"] = txtKhoanoitru.Text;
                    dr["songay"] = 0;
                    dr["id_chuyen"] = -1;
                    m_dtPatient.AcceptChanges();
                }
            }
            catch (Exception)
            {
                
            }
           
        }

        private void ThemLanKham()
        {
            DateTime ngaychuyenkhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                             dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                             Utility.Int32Dbnull(txtPhut.Text), 00);
            objBenhnhan = TaoBenhNhan();
            objLuotkham = TaoLuotkham();
          objBuonggiuong = TaodulieuBuonggiuong();
            KcbDangkySokham objSokham = TaosoKCB();
            long v_id_kham = -1;
            string msg = "";
            errorProvider1.Clear();
            ActionResult actionResult = _KCB_DANGKY.ThemmoiLuotkhamCapcuu(objBenhnhan, objLuotkham, objSokham, objBuonggiuong, ngaychuyenkhoa, ref msg);

            if (msg.Trim() != "")
            {
                errorProvider1.SetError(txtSoKcb, msg);
            }
            switch (actionResult)
            {
                case ActionResult.Success:
                    if (objLuotkham.SoBenhAn!=null && objLuotkham.SoBenhAn != txtSoBenhAn.Text)
                    {
                        Utility.ShowMsg(string.Format( "Chú ý: Số bệnh án nội trú {0} đã được Bệnh nhân khác sử dụng nên số bệnh án nội trú mới của Bệnh nhân là {1}",txtSoBenhAn.Text,objLuotkham.SoBenhAn ));
                    }
                    txtSoBenhAn.Text = objLuotkham.SoBenhAn;
                    PtramBhytCu = Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0);
                    PtramBhytGocCu = Utility.DecimaltoDbnull(txtptramDauthe.Text, 0);
                    txtMaLankham.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                    txtMaBN.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                    m_blnHasJustInsert = true;
                    m_enAction = action.Update;
                    Utility.SetMsg(lblMsg, "Bạn thêm mới lần khám bệnh nhân thành công", false);
                    ThemMoiLanKhamVaoLuoi();
                    if (_OnActionSuccess != null) _OnActionSuccess();
                   
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtMaLankham.Text);
                    if (chkTudongthemmoi.Checked)
                        cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                    else
                    {
                        ucTamung1.ChangePatients(objLuotkham, "LYDOTAMUNGCAPCUU");
                        tabControl1.SelectedTab = tabControl1.TabPages[1];
                        ucTamung1.Themmoi();
                    }
                    m_blnCancel = false;
                    break;
                case ActionResult.Error:
                    Utility.SetMsg(lblMsg, "Lỗi trong quá trình thêm lần khám !", true);
                    cmdSave.Focus();
                    break;
            }
        }
      
        private KcbDangkySokham TaosoKCB()
        {
            KcbDangkySokham objSokham = null;
            if (objDoituongKCB == null) return null;
            if (chkLaysokham.Checked &&  txtSoKcb.myCode != "-1")
            {
                
                DmucChung objDmucchung = THU_VIEN_CHUNG.LaydoituongDmucChung(txtSoKcb.LOAI_DANHMUC, txtSoKcb.myCode);
                if (objDmucchung != null)
                {
                    objSokham = new KcbDangkySokham();
                    if (objDoituongKCB != null)
                    {
                        objSokham.IdLoaidoituongkcb = objDoituongKCB.IdLoaidoituongKcb;
                        objSokham.MaDoituongkcb = objDoituongKCB.MaDoituongKcb;
                        objSokham.IdDoituongkcb = objDoituongKCB.IdDoituongKcb;
                    }
                    
                    objSokham.MaSokcb = txtSoKcb.myCode;
                    objSokham.PhuThu = 0;
                    objSokham.TrongGoi = 0;
                    objSokham.IdGoi = -1;
                    objSokham.IdNhanvien = globalVariables.gv_intIDNhanvien;
                    objSokham.DonGia = Utility.DecimaltoDbnull(objDmucchung.VietTat, 0);
                    objSokham.BhytChitra = 0;
                    objSokham.BnhanChitra = objSokham.DonGia;
                    objSokham.PtramBhyt = 0;
                    objSokham.PtramBhytGoc = 0;
                    objSokham.TrangthaiThanhtoan = 0;
                    objSokham.IdThanhtoan = -1;
                    objSokham.NgayThanhtoan = null;
                    objSokham.Noitru = 0;
                    objSokham.NguonThanhtoan = 0;
                    objSokham.TuTuc = Utility.Bool2byte(THU_VIEN_CHUNG.IsBaoHiem(objSokham.IdLoaidoituongkcb));
                    objSokham.IdKhoakcb = globalVariablesPrivate.objKhoaphong.IdKhoaphong;
                }
            }
            return objSokham;
        }

        private bool isValidIdentifyNum()
        {
            try
            {
                if (txtCMT.Text.Trim() == "") return true;
                string sql = "";
                QueryCommand cmd = KcbDanhsachBenhnhan.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                sql =
                    "Select cmt,id_benhnhan,ten_benhnhan,gioi_tinh from kcb_danhsach_benhnhan ";
                sql += " where cmt = '" + txtCMT.Text.Trim() + "'";
                if (m_enAction == action.Insert)
                    sql += "";
                else //Là update hoặc thêm mới lần khám cần kiểm tra có trùng với BN khác chưa
                    sql += " AND id_benhnhan <> " + txtMaBN.Text.Trim();
                cmd.CommandSql = sql;
                DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                if (temdt.Rows.Count > 0)
                {
                    Utility.ShowMsg(
                        string.Format("Số CMT này đang được sử dụng cho Bệnh nhân {0}:{1}\nMời bạn kiểm tra lại",
                                      temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan], temdt.Rows[0]["ten_benhnhan"]));
                    txtCMT.Focus();
                    return false;
                }
                return temdt.Rows.Count <= 0;
            }
            catch
            {
                return false;
            }
        }
        private void InsertPatient()
        {
            DateTime ngaychuyenkhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                           dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                           Utility.Int32Dbnull(txtPhut.Text), 00);
            objBenhnhan = TaoBenhNhan();
            objLuotkham = TaoLuotkham();
           
                objBuonggiuong = TaodulieuBuonggiuong();
            KcbDangkySokham objSokham = TaosoKCB();
            long v_id_kham = -1;
            string msg = "";
            errorProvider1.Clear();
            ActionResult actionResult = _KCB_DANGKY.ThemmoiBenhnhanCapcuu(objBenhnhan, objLuotkham, objSokham,objBuonggiuong,
                                                                            ngaychuyenkhoa, ref msg);

            if (msg.Trim() != "")
            {
                errorProvider1.SetError(txtSoKcb, msg);
            }
            switch (actionResult)
            {
                case ActionResult.Success:

                    if (objLuotkham.SoBenhAn!=null && objLuotkham.SoBenhAn != txtSoBenhAn.Text)
                    {
                        Utility.ShowMsg(string.Format( "Chú ý: Số bệnh án nội trú {0} đã được Bệnh nhân khác sử dụng nên số bệnh án nội trú mới của Bệnh nhân là {1}",txtSoBenhAn.Text,objLuotkham.SoBenhAn ));
                    }
                    txtSoBenhAn.Text = objLuotkham.SoBenhAn;

                    PtramBhytCu = Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0);
                    PtramBhytGocCu = Utility.DecimaltoDbnull(txtptramDauthe.Text, 0);
                    txtMaLankham.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                    txtMaBN.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                    mavuasinh = Utility.sDbnull(objLuotkham.IdBenhnhan);
                    m_enAction = action.Update;
                    m_blnHasJustInsert = true;
                    m_strMaluotkham = txtMaLankham.Text;
                    ThemMoiLanKhamVaoLuoi();
                    if (_OnActionSuccess != null) _OnActionSuccess();
                    Utility.SetMsg(lblMsg, "Bạn thêm mới bệnh nhân thành công", false);
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtMaLankham.Text);
                    m_blnCancel = false;
                    if (chkTudongthemmoi.Checked)
                    {
                        cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                    }
                     else
                    {
                        ucTamung1.ChangePatients(objLuotkham, "LYDOTAMUNGCAPCUU");
                        tabControl1.SelectedTab = tabControl1.TabPages[1];
                        ucTamung1.Themmoi();
                    }
                        
                    txtMaBN.Text = Utility.sDbnull(mavuasinh);
                    break;
                case ActionResult.Error:
                    Utility.SetMsg(lblMsg, "Bạn thực hiện thêm dữ liệu không thành công !", true);
                    cmdSave.Focus();
                    break;
            }
        }
       
        private NoitruPhanbuonggiuong TaodulieuBuonggiuong()
        {

            NoitruPhanbuonggiuong objPhanbuonggiuong = null;
            if (objBuonggiuong != null)
                objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(objBuonggiuong.Id);
            else
                objPhanbuonggiuong = new NoitruPhanbuonggiuong();
            objPhanbuonggiuong.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
            objPhanbuonggiuong.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1);
            objPhanbuonggiuong.IdKhoanoitru = Utility.Int16Dbnull(txtKhoanoitru.MyID, -1);
            objPhanbuonggiuong.NgayTao = globalVariables.SysDate;
            objPhanbuonggiuong.NgayVaokhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                           dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                           Utility.Int32Dbnull(txtPhut.Text), 00);
            objPhanbuonggiuong.IdKham = -1;
            objPhanbuonggiuong.NguoiTao = globalVariables.UserName;
            objPhanbuonggiuong.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
            objPhanbuonggiuong.NoiTru = 1;
            objPhanbuonggiuong.TrangthaiThanhtoan = 0;
            objPhanbuonggiuong.TrangThai = 0;
            objPhanbuonggiuong.DuyetBhyt = 0;
            objPhanbuonggiuong.CachtinhSoluong = 0;
            objPhanbuonggiuong.SoluongGio = 0;
            objPhanbuonggiuong.IdBuong = Utility.Int16Dbnull(txtRoom_code.MyID,-1);
            objPhanbuonggiuong.IdGiuong = Utility.Int16Dbnull(txtBedCode.MyID,-1);
            objPhanbuonggiuong.IdGia = Utility.Int32Dbnull(txtGia.MyID, -1);
            return objPhanbuonggiuong;
        }
        private void CapnhatthongtinBenhnhan()
        {
            DateTime ngaychuyenkhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                          dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                          Utility.Int32Dbnull(txtPhut.Text), 00);
            objBenhnhan = TaoBenhNhan();
            
            objLuotkham = TaoLuotkham();
            
                objBuonggiuong = TaodulieuBuonggiuong();
            KcbDangkySokham objSokham = TaosoKCB();
            string msg = "";
            errorProvider1.Clear();
            ActionResult actionResult = _KCB_DANGKY.UpdateBenhnhanCapcuu(objBenhnhan, objLuotkham, objSokham, objBuonggiuong, ngaychuyenkhoa, PtramBhytCu, PtramBhytGocCu, ref msg);
            // THEM_PHI_DVU_KYC(objLuotkham);
            if (msg.Trim() != "")
            {
                errorProvider1.SetError(txtSoKcb, msg);
            }
            switch (actionResult)
            {
                case ActionResult.Success:
                   
                    if (objLuotkham.SoBenhAn!=null && objLuotkham.SoBenhAn != txtSoBenhAn.Text)
                    {
                        Utility.ShowMsg(string.Format( "Chú ý: Số bệnh án nội trú {0} đã được Bệnh nhân khác sử dụng nên số bệnh án nội trú mới của Bệnh nhân là {1}",txtSoBenhAn.Text,objLuotkham.SoBenhAn ));
                    }
                    txtSoBenhAn.Text=objLuotkham.SoBenhAn;
                    //gọi lại nếu thay đổi địa chỉ
                    m_blnHasJustInsert = false;
                    PtramBhytCu = Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0);
                    PtramBhytGocCu = Utility.DecimaltoDbnull(txtptramDauthe.Text, 0);
                    Utility.SetMsg(lblMsg, "Bạn sửa thông tin Bệnh nhân thành công", false);
                    UpdateBNVaoTrenLuoi();
                    if (_OnActionSuccess != null) _OnActionSuccess();
                    
                    if (string.IsNullOrEmpty(objLuotkham.MatheBhyt))
                    {
                        dtInsFromDate.Value = globalVariables.SysDate;
                        dtInsToDate.Value = globalVariables.SysDate;
                        txtPtramBHYT.Text = "";
                        txtptramDauthe.Text = "";
                        txtMaDtuong_BHYT.Clear();
                        txtMaQuyenloi_BHYT.Clear();
                        txtNoiDongtrusoKCBBD.Clear();
                        txtOthu4.Clear();
                        txtOthu5.Clear();
                        txtOthu6.Clear();
                        chkTraiTuyen.Checked = false;
                        lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
                        chkChuyenVien.Checked = false;
                        txtNoiphattheBHYT.Clear();
                        txtNoiDKKCBBD.Clear();
                    }
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtMaLankham.Text);
                    m_blnCancel = false;

                    break;
                case ActionResult.Error:
                    Utility.SetMsg(lblMsg, "Bạn thực hiện sửa thông tin không thành công !", true);
                    break;
                case ActionResult.Cancel:
                    Utility.ShowMsg(string.Format( "Bệnh nhân này đã thanh toán một số dịch vụ nên bạn không được phép chuyển đối tượng hoặc thay đổi phần trăm BHYT\nPhần trăm cũ {0} % - Phần trăm mới {1} %",PtramBhytCu.ToString(),txtPtramBHYT.Text),"Cảnh báo");
                    break;
            }
        }

        /// <summary>
        /// Insert dữ liệu khi thêm mới hoàn toàn
        /// </summary>hàm chen du lieu moi tin day, benhnhan kham benh moi tinh
        private KcbDanhsachBenhnhan TaoBenhNhan()
        {
            
            var objBenhnhan = new KcbDanhsachBenhnhan();
            if (m_enAction == action.Add) objBenhnhan.IdBenhnhan = Utility.Int64Dbnull(txtMaBN.Text, -1);
            if (m_enAction == action.Update) objBenhnhan = KcbDanhsachBenhnhan.FetchByID(Utility.Int64Dbnull(txtMaBN.Text, -1));
            objBenhnhan.TenBenhnhan = txtTEN_BN.Text;
            objBenhnhan.DiaChi = txtDiachi.Text;
            //Tạm REM lại tìm hiểu tại sao lại gán ="" với đối tượng dịch vụ
            //if (_IdDoituongKcb == 1) //Đối tượng dịch vụ
            //    objBenhnhan.DiaChi = "";
            //else //Đối tượng BHYT
            objBenhnhan.DiachiBhyt = Utility.sDbnull(txtDiachi_bhyt.Text);
            objBenhnhan.DienThoai = txtSoDT.Text;
            objBenhnhan.Email = "";
            //objBenhnhan.Locked = 0;
            objBenhnhan.NgayTao = globalVariables.SysDate;
            objBenhnhan.NguoiTao = globalVariables.UserName;
            objBenhnhan.NguonGoc = "KCB";
            objBenhnhan.Cmt = Utility.sDbnull(txtCMT.Text, "");
            objBenhnhan.CoQuan = string.Empty;
            objBenhnhan.NgheNghiep = txtNgheNghiep.Text;
            objBenhnhan.GioiTinh = cboPatientSex.Text;
            objBenhnhan.IdGioitinh = Utility.ByteDbnull(cboPatientSex.SelectedValue, 0);
            objBenhnhan.NamSinh = txtNamSinh.Visible ? Utility.Int16Dbnull(txtNamSinh.Text, null) : Utility.Int16Dbnull(dtpBOD.Value.Year);
            string BirthDate = txtNamSinh.Visible ? string.Format("{0}/{1}/{2}", 1, 1, txtNamSinh.Text) : dtpBOD.Value.ToString("dd/MM/yyyy");
            if (Dates.IsDate(BirthDate))
            {
                objBenhnhan.NgaySinh = Convert.ToDateTime(BirthDate);
            }
            else
            {
                objBenhnhan.NgaySinh = null;
            }

            if (m_enAction == action.Insert)
            {
                objBenhnhan.NgayTiepdon = dtCreateDate.Value;
                objBenhnhan.NguoiTao = globalVariables.UserName;
                objBenhnhan.IpMaytao = globalVariables.gv_strIPAddress;
                objBenhnhan.TenMaytao = globalVariables.gv_strComputerName;
            }
            if (m_enAction == action.Update)
            {
                objBenhnhan.NgaySua = globalVariables.SysDate;
                objBenhnhan.NguoiSua = globalVariables.UserName;
                objBenhnhan.NgayTiepdon = dtCreateDate.Value;

                objBenhnhan.IpMaysua = globalVariables.gv_strIPAddress;
                objBenhnhan.TenMaysua = globalVariables.gv_strComputerName;
            }
            objBenhnhan.DanToc = txtDantoc.Text;
            return objBenhnhan;
        }

        /// <summary>
        /// hàm thực hiện việc khwoir tạo thoog tin PatietnExam
        /// </summary>
        /// <returns></returns>
        private KcbLuotkham TaoLuotkham()
        {
           
            if (m_enAction == action.Insert || m_enAction == action.Add)
            {
                objLuotkham = new KcbLuotkham();
                objLuotkham.IsNew = true;
            }
            else
            {
                objLuotkham.IsLoaded = true;
                objLuotkham.MarkOld();
                objLuotkham.IsNew = false;
            }
            if (string.IsNullOrEmpty(Utility.sDbnull(objLuotkham.SoBenhAn, "")))
            {
                txtSoBenhAn.Text = THU_VIEN_CHUNG.LaySoBenhAn();
            }
            else
            {
                txtSoBenhAn.Text = Utility.sDbnull(objLuotkham.SoBenhAn, "");
            }
            objLuotkham.SoBenhAn = Utility.sDbnull(txtSoBenhAn.Text);
            objLuotkham.MotaNhapvien = Utility.DoTrim(txtGhiChu.Text);

            objLuotkham.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            objLuotkham.Noitru = 0;
            objLuotkham.IdDoituongKcb = _IdDoituongKcb;
            objLuotkham.IdLoaidoituongKcb = _IdLoaidoituongKcb;
            objLuotkham.Locked = 0;
            objLuotkham.HienthiBaocao = 1;
            objLuotkham.TrangthaiCapcuu = 1;
            objLuotkham.CachTao = 1;
            objLuotkham.IdKhoatiepnhan = globalVariables.idKhoatheoMay;
            objLuotkham.NguoiTao = globalVariables.UserName;
            objLuotkham.NgayTao = globalVariables.SysDate;
            objLuotkham.Cmt = Utility.sDbnull(txtCMT.Text, "");
            objLuotkham.DiaChi = txtDiachi.Text;
            objLuotkham.Email = "";
            objLuotkham.NoiGioithieu = "";
            objLuotkham.NhomBenhnhan = "-1";
            objLuotkham.IdBenhvienDen = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
            objLuotkham.TthaiChuyenden = (byte)(chkChuyenVien.Checked ? 1 : 0);
            if (THU_VIEN_CHUNG.IsBaoHiem(_IdLoaidoituongKcb))
            {
                Laymathe_BHYT();
                objLuotkham.MaKcbbd = Utility.sDbnull(txtNoiDKKCBBD.Text, "");
                objLuotkham.NoiDongtrusoKcbbd = Utility.sDbnull(txtNoiDongtrusoKCBBD.Text, "");
                objLuotkham.MaNoicapBhyt = Utility.sDbnull(txtNoiphattheBHYT.Text);
                objLuotkham.LuongCoban = globalVariables.LUONGCOBAN;
                objLuotkham.MatheBhyt = Laymathe_BHYT();
                objLuotkham.MaDoituongBhyt = Utility.sDbnull(txtMaDtuong_BHYT.Text);
                objLuotkham.MaQuyenloi = Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, null);
                objLuotkham.DungTuyen= !chkTraiTuyen.Visible ? 1 : (((byte?)(chkTraiTuyen.Checked ? 0 : 1)));

                objLuotkham.MadtuongSinhsong = txtMaDTsinhsong.myCode;
                objLuotkham.GiayBhyt = Utility.Bool2byte(chkGiayBHYT.Checked);

                objLuotkham.NgayketthucBhyt = dtInsToDate.Value.Date;
                objLuotkham.NgaybatdauBhyt = dtInsFromDate.Value.Date;
                objLuotkham.NoicapBhyt = Utility.GetValue(lblNoiCapThe.Text, false);
                objLuotkham.DiachiBhyt = Utility.sDbnull(txtDiachi_bhyt.Text);
                
            }
            else
            {
                objLuotkham.GiayBhyt = 0;
                objLuotkham.MadtuongSinhsong = "";
                objLuotkham.MaKcbbd = "";
                objLuotkham.NoiDongtrusoKcbbd = "";
                objLuotkham.MaNoicapBhyt = "";
                objLuotkham.LuongCoban = globalVariables.LUONGCOBAN;
                objLuotkham.MatheBhyt = "";
                objLuotkham.MaDoituongBhyt = "";
                objLuotkham.MaQuyenloi = -1;
                objLuotkham.DungTuyen = 0;
               
                objLuotkham.NgayketthucBhyt = null;
                objLuotkham.NgaybatdauBhyt = null;
                objLuotkham.NoicapBhyt = "";
                objLuotkham.DiachiBhyt = "";
               
            }
            
            objLuotkham.SolanKham = Utility.Int16Dbnull(txtSolankham.Text, 0);
            objLuotkham.TrieuChung = Utility.ReplaceStr(txtTrieuChungBD.Text);
            //Tránh lỗi khi update người dùng nhập mã lần khám lung tung
            if (m_enAction == action.Update) txtMaLankham.Text = m_strMaluotkham;
            objLuotkham.MaLuotkham = Utility.sDbnull(txtMaLankham.Text, "");
            objLuotkham.IdBenhnhan = Utility.Int64Dbnull(txtMaBN.Text, -1);
            DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(_IdDoituongKcb);
            if (objectType != null)
            {
                objLuotkham.MaDoituongKcb = Utility.sDbnull(objectType.MaDoituongKcb, "");
            }
            if (m_enAction == action.Update)
            {
                objLuotkham.NgayTiepdon = dtCreateDate.Value;
                objLuotkham.NguoiSua = globalVariables.UserName;
                objLuotkham.NgaySua = globalVariables.SysDate;
                objLuotkham.IpMaysua = globalVariables.gv_strIPAddress;
                objLuotkham.TenMaysua = globalVariables.gv_strComputerName;
            }
            if (m_enAction == action.Add || m_enAction == action.Insert)
            {
                objLuotkham.NgayTiepdon = dtCreateDate.Value;
                objLuotkham.NguoiTiepdon = globalVariables.UserName;

                objLuotkham.IpMaytao = globalVariables.gv_strIPAddress;
                objLuotkham.TenMaytao = globalVariables.gv_strComputerName;
            }
            objLuotkham.PtramBhytGoc = Utility.DecimaltoDbnull(txtptramDauthe.Text, 0);
            objLuotkham.PtramBhyt =Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0);//chkTraiTuyen.Visible ?Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0):(objLuotkham.DungTuyen == 0 ? 0 : Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0));
            return objLuotkham;
        }

        #endregion

       
       
        /// <summary>
        /// hàm thực hiện việc enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDantoc.Focus();
            }
        }
    }
}