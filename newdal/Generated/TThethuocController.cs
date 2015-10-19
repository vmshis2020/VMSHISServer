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
    /// Controller class for t_thethuoc
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TThethuocController
    {
        // Preload our schema..
        TThethuoc thisSchemaLoad = new TThethuoc();
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
        public TThethuocCollection FetchAll()
        {
            TThethuocCollection coll = new TThethuocCollection();
            Query qry = new Query(TThethuoc.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TThethuocCollection FetchByID(object IdThuoc)
        {
            TThethuocCollection coll = new TThethuocCollection().Where("id_thuoc", IdThuoc).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TThethuocCollection FetchByQuery(Query qry)
        {
            TThethuocCollection coll = new TThethuocCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdThuoc)
        {
            return (TThethuoc.Delete(IdThuoc) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdThuoc)
        {
            return (TThethuoc.Destroy(IdThuoc) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(long IdThuoc,short IdKho,DateTime NgayHethan,decimal DonGia,DateTime Ngay)
        {
            Query qry = new Query(TThethuoc.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("IdThuoc", IdThuoc).AND("IdKho", IdKho).AND("NgayHethan", NgayHethan).AND("DonGia", DonGia).AND("Ngay", Ngay);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(long IdThuoc,short IdKho,DateTime NgayHethan,decimal DonGia,DateTime Ngay,int TonDaungay,int Nhap,int Xuat,int? Nhapchot,int? Xuatchot)
	    {
		    TThethuoc item = new TThethuoc();
		    
            item.IdThuoc = IdThuoc;
            
            item.IdKho = IdKho;
            
            item.NgayHethan = NgayHethan;
            
            item.DonGia = DonGia;
            
            item.Ngay = Ngay;
            
            item.TonDaungay = TonDaungay;
            
            item.Nhap = Nhap;
            
            item.Xuat = Xuat;
            
            item.Nhapchot = Nhapchot;
            
            item.Xuatchot = Xuatchot;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long IdThuoc,short IdKho,DateTime NgayHethan,decimal DonGia,DateTime Ngay,int TonDaungay,int Nhap,int Xuat,int? Nhapchot,int? Xuatchot)
	    {
		    TThethuoc item = new TThethuoc();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdThuoc = IdThuoc;
				
			item.IdKho = IdKho;
				
			item.NgayHethan = NgayHethan;
				
			item.DonGia = DonGia;
				
			item.Ngay = Ngay;
				
			item.TonDaungay = TonDaungay;
				
			item.Nhap = Nhap;
				
			item.Xuat = Xuat;
				
			item.Nhapchot = Nhapchot;
				
			item.Xuatchot = Xuatchot;
				
	        item.Save(UserName);
	    }
    }
}