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
    /// Controller class for qhe_doituong_goidichvu
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class QheDoituongGoidichvuController
    {
        // Preload our schema..
        QheDoituongGoidichvu thisSchemaLoad = new QheDoituongGoidichvu();
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
        public QheDoituongGoidichvuCollection FetchAll()
        {
            QheDoituongGoidichvuCollection coll = new QheDoituongGoidichvuCollection();
            Query qry = new Query(QheDoituongGoidichvu.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public QheDoituongGoidichvuCollection FetchByID(object IdQuanhe)
        {
            QheDoituongGoidichvuCollection coll = new QheDoituongGoidichvuCollection().Where("id_quanhe", IdQuanhe).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public QheDoituongGoidichvuCollection FetchByQuery(Query qry)
        {
            QheDoituongGoidichvuCollection coll = new QheDoituongGoidichvuCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdQuanhe)
        {
            return (QheDoituongGoidichvu.Delete(IdQuanhe) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdQuanhe)
        {
            return (QheDoituongGoidichvu.Destroy(IdQuanhe) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(short IdDoituongKcb,string MaDoituongKcb,int? IdLoaidoituongKcb,string MaGoidichvu,decimal? TyleGiam,byte KieuGiamgia,decimal? DonGia,decimal? PhuthuDungtuyen,decimal? PhuthuTraituyen,string MotaThem,DateTime? NgayTao,string NguoiTao,DateTime? NgaySua,string NguoiSua,string MaKhoaThuchien)
	    {
		    QheDoituongGoidichvu item = new QheDoituongGoidichvu();
		    
            item.IdDoituongKcb = IdDoituongKcb;
            
            item.MaDoituongKcb = MaDoituongKcb;
            
            item.IdLoaidoituongKcb = IdLoaidoituongKcb;
            
            item.MaGoidichvu = MaGoidichvu;
            
            item.TyleGiam = TyleGiam;
            
            item.KieuGiamgia = KieuGiamgia;
            
            item.DonGia = DonGia;
            
            item.PhuthuDungtuyen = PhuthuDungtuyen;
            
            item.PhuthuTraituyen = PhuthuTraituyen;
            
            item.MotaThem = MotaThem;
            
            item.NgayTao = NgayTao;
            
            item.NguoiTao = NguoiTao;
            
            item.NgaySua = NgaySua;
            
            item.NguoiSua = NguoiSua;
            
            item.MaKhoaThuchien = MaKhoaThuchien;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(short IdQuanhe,short IdDoituongKcb,string MaDoituongKcb,int? IdLoaidoituongKcb,string MaGoidichvu,decimal? TyleGiam,byte KieuGiamgia,decimal? DonGia,decimal? PhuthuDungtuyen,decimal? PhuthuTraituyen,string MotaThem,DateTime? NgayTao,string NguoiTao,DateTime? NgaySua,string NguoiSua,string MaKhoaThuchien)
	    {
		    QheDoituongGoidichvu item = new QheDoituongGoidichvu();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdQuanhe = IdQuanhe;
				
			item.IdDoituongKcb = IdDoituongKcb;
				
			item.MaDoituongKcb = MaDoituongKcb;
				
			item.IdLoaidoituongKcb = IdLoaidoituongKcb;
				
			item.MaGoidichvu = MaGoidichvu;
				
			item.TyleGiam = TyleGiam;
				
			item.KieuGiamgia = KieuGiamgia;
				
			item.DonGia = DonGia;
				
			item.PhuthuDungtuyen = PhuthuDungtuyen;
				
			item.PhuthuTraituyen = PhuthuTraituyen;
				
			item.MotaThem = MotaThem;
				
			item.NgayTao = NgayTao;
				
			item.NguoiTao = NguoiTao;
				
			item.NgaySua = NgaySua;
				
			item.NguoiSua = NguoiSua;
				
			item.MaKhoaThuchien = MaKhoaThuchien;
				
	        item.Save(UserName);
	    }
    }
}