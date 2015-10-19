using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
// <auto-generated />
namespace VNS.HIS.DAL
{
    /// <summary>
    /// Controller class for noitru_dmuc_giuongbenh
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class NoitruDmucGiuongbenhController
    {
        // Preload our schema..
        NoitruDmucGiuongbenh thisSchemaLoad = new NoitruDmucGiuongbenh();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public NoitruDmucGiuongbenhCollection FetchAll()
        {
            NoitruDmucGiuongbenhCollection coll = new NoitruDmucGiuongbenhCollection();
            Query qry = new Query(NoitruDmucGiuongbenh.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public NoitruDmucGiuongbenhCollection FetchByID(object IdGiuong)
        {
            NoitruDmucGiuongbenhCollection coll = new NoitruDmucGiuongbenhCollection().Where("id_giuong", IdGiuong).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public NoitruDmucGiuongbenhCollection FetchByQuery(Query qry)
        {
            NoitruDmucGiuongbenhCollection coll = new NoitruDmucGiuongbenhCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdGiuong)
        {
            return (NoitruDmucGiuongbenh.Delete(IdGiuong) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdGiuong)
        {
            return (NoitruDmucGiuongbenh.Destroy(IdGiuong) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaGiuong,string TenGiuong,short IdKhoanoitru,short IdBuong,decimal GiaDichvu,decimal? GiaBhyt,decimal? PhuthuDungtuyen,decimal? PhuthuTraituyen,decimal? GiaKhac,short SonguoiToida,string MotaThem,string MaDonvitinh,byte? TrangThai,byte? TthaiTunguyen,byte? DangSudung,short? SttHthi,string TenBhyt)
	    {
		    NoitruDmucGiuongbenh item = new NoitruDmucGiuongbenh();
		    
            item.MaGiuong = MaGiuong;
            
            item.TenGiuong = TenGiuong;
            
            item.IdKhoanoitru = IdKhoanoitru;
            
            item.IdBuong = IdBuong;
            
            item.GiaDichvu = GiaDichvu;
            
            item.GiaBhyt = GiaBhyt;
            
            item.PhuthuDungtuyen = PhuthuDungtuyen;
            
            item.PhuthuTraituyen = PhuthuTraituyen;
            
            item.GiaKhac = GiaKhac;
            
            item.SonguoiToida = SonguoiToida;
            
            item.MotaThem = MotaThem;
            
            item.MaDonvitinh = MaDonvitinh;
            
            item.TrangThai = TrangThai;
            
            item.TthaiTunguyen = TthaiTunguyen;
            
            item.DangSudung = DangSudung;
            
            item.SttHthi = SttHthi;
            
            item.TenBhyt = TenBhyt;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(short IdGiuong,string MaGiuong,string TenGiuong,short IdKhoanoitru,short IdBuong,decimal GiaDichvu,decimal? GiaBhyt,decimal? PhuthuDungtuyen,decimal? PhuthuTraituyen,decimal? GiaKhac,short SonguoiToida,string MotaThem,string MaDonvitinh,byte? TrangThai,byte? TthaiTunguyen,byte? DangSudung,short? SttHthi,string TenBhyt)
	    {
		    NoitruDmucGiuongbenh item = new NoitruDmucGiuongbenh();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdGiuong = IdGiuong;
				
			item.MaGiuong = MaGiuong;
				
			item.TenGiuong = TenGiuong;
				
			item.IdKhoanoitru = IdKhoanoitru;
				
			item.IdBuong = IdBuong;
				
			item.GiaDichvu = GiaDichvu;
				
			item.GiaBhyt = GiaBhyt;
				
			item.PhuthuDungtuyen = PhuthuDungtuyen;
				
			item.PhuthuTraituyen = PhuthuTraituyen;
				
			item.GiaKhac = GiaKhac;
				
			item.SonguoiToida = SonguoiToida;
				
			item.MotaThem = MotaThem;
				
			item.MaDonvitinh = MaDonvitinh;
				
			item.TrangThai = TrangThai;
				
			item.TthaiTunguyen = TthaiTunguyen;
				
			item.DangSudung = DangSudung;
				
			item.SttHthi = SttHthi;
				
			item.TenBhyt = TenBhyt;
				
	        item.Save(UserName);
	    }
    }
}
