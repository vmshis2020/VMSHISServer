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
    /// Controller class for hoadon_capphat
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class HoadonCapphatController
    {
        // Preload our schema..
        HoadonCapphat thisSchemaLoad = new HoadonCapphat();
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
        public HoadonCapphatCollection FetchAll()
        {
            HoadonCapphatCollection coll = new HoadonCapphatCollection();
            Query qry = new Query(HoadonCapphat.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public HoadonCapphatCollection FetchByID(object IdCapphat)
        {
            HoadonCapphatCollection coll = new HoadonCapphatCollection().Where("id_capphat", IdCapphat).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public HoadonCapphatCollection FetchByQuery(Query qry)
        {
            HoadonCapphatCollection coll = new HoadonCapphatCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdCapphat)
        {
            return (HoadonCapphat.Delete(IdCapphat) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdCapphat)
        {
            return (HoadonCapphat.Destroy(IdCapphat) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(long IdHoadonMau,string MaNhanvien,string MauHoadon,string KiHieu,string MaQuyen,string SerieDau,string SerieCuoi,string SerieHientai,DateTime NgayCapphat,short TrangThai)
	    {
		    HoadonCapphat item = new HoadonCapphat();
		    
            item.IdHoadonMau = IdHoadonMau;
            
            item.MaNhanvien = MaNhanvien;
            
            item.MauHoadon = MauHoadon;
            
            item.KiHieu = KiHieu;
            
            item.MaQuyen = MaQuyen;
            
            item.SerieDau = SerieDau;
            
            item.SerieCuoi = SerieCuoi;
            
            item.SerieHientai = SerieHientai;
            
            item.NgayCapphat = NgayCapphat;
            
            item.TrangThai = TrangThai;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdCapphat,long IdHoadonMau,string MaNhanvien,string MauHoadon,string KiHieu,string MaQuyen,string SerieDau,string SerieCuoi,string SerieHientai,DateTime NgayCapphat,short TrangThai)
	    {
		    HoadonCapphat item = new HoadonCapphat();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdCapphat = IdCapphat;
				
			item.IdHoadonMau = IdHoadonMau;
				
			item.MaNhanvien = MaNhanvien;
				
			item.MauHoadon = MauHoadon;
				
			item.KiHieu = KiHieu;
				
			item.MaQuyen = MaQuyen;
				
			item.SerieDau = SerieDau;
				
			item.SerieCuoi = SerieCuoi;
				
			item.SerieHientai = SerieHientai;
				
			item.NgayCapphat = NgayCapphat;
				
			item.TrangThai = TrangThai;
				
	        item.Save(UserName);
	    }
    }
}
