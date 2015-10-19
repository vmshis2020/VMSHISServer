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
    /// Controller class for dmuc_dichvukcb
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class DmucDichvukcbController
    {
        // Preload our schema..
        DmucDichvukcb thisSchemaLoad = new DmucDichvukcb();
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
        public DmucDichvukcbCollection FetchAll()
        {
            DmucDichvukcbCollection coll = new DmucDichvukcbCollection();
            Query qry = new Query(DmucDichvukcb.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DmucDichvukcbCollection FetchByID(object IdDichvukcb)
        {
            DmucDichvukcbCollection coll = new DmucDichvukcbCollection().Where("id_dichvukcb", IdDichvukcb).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public DmucDichvukcbCollection FetchByQuery(Query qry)
        {
            DmucDichvukcbCollection coll = new DmucDichvukcbCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdDichvukcb)
        {
            return (DmucDichvukcb.Delete(IdDichvukcb) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdDichvukcb)
        {
            return (DmucDichvukcb.Destroy(IdDichvukcb) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaDichvukcb,string TenDichvukcb,short IdKieukham,short IdKhoaphong,short IdBacsy,short IdDoituongKcb,short IdPhongkham,decimal DonGia,decimal? PhuthuDungtuyen,decimal? PhuthuTraituyen,decimal? DongiaNgoaigio,decimal? PhuthuNgoaigio,string MaDoituongKcb,int? IdPhikemtheo,int? IdPhikemtheongoaigio,string NhomBaocao,byte? TuTuc,short? SttHthi,string MotaThem)
	    {
		    DmucDichvukcb item = new DmucDichvukcb();
		    
            item.MaDichvukcb = MaDichvukcb;
            
            item.TenDichvukcb = TenDichvukcb;
            
            item.IdKieukham = IdKieukham;
            
            item.IdKhoaphong = IdKhoaphong;
            
            item.IdBacsy = IdBacsy;
            
            item.IdDoituongKcb = IdDoituongKcb;
            
            item.IdPhongkham = IdPhongkham;
            
            item.DonGia = DonGia;
            
            item.PhuthuDungtuyen = PhuthuDungtuyen;
            
            item.PhuthuTraituyen = PhuthuTraituyen;
            
            item.DongiaNgoaigio = DongiaNgoaigio;
            
            item.PhuthuNgoaigio = PhuthuNgoaigio;
            
            item.MaDoituongKcb = MaDoituongKcb;
            
            item.IdPhikemtheo = IdPhikemtheo;
            
            item.IdPhikemtheongoaigio = IdPhikemtheongoaigio;
            
            item.NhomBaocao = NhomBaocao;
            
            item.TuTuc = TuTuc;
            
            item.SttHthi = SttHthi;
            
            item.MotaThem = MotaThem;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdDichvukcb,string MaDichvukcb,string TenDichvukcb,short IdKieukham,short IdKhoaphong,short IdBacsy,short IdDoituongKcb,short IdPhongkham,decimal DonGia,decimal? PhuthuDungtuyen,decimal? PhuthuTraituyen,decimal? DongiaNgoaigio,decimal? PhuthuNgoaigio,string MaDoituongKcb,int? IdPhikemtheo,int? IdPhikemtheongoaigio,string NhomBaocao,byte? TuTuc,short? SttHthi,string MotaThem)
	    {
		    DmucDichvukcb item = new DmucDichvukcb();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdDichvukcb = IdDichvukcb;
				
			item.MaDichvukcb = MaDichvukcb;
				
			item.TenDichvukcb = TenDichvukcb;
				
			item.IdKieukham = IdKieukham;
				
			item.IdKhoaphong = IdKhoaphong;
				
			item.IdBacsy = IdBacsy;
				
			item.IdDoituongKcb = IdDoituongKcb;
				
			item.IdPhongkham = IdPhongkham;
				
			item.DonGia = DonGia;
				
			item.PhuthuDungtuyen = PhuthuDungtuyen;
				
			item.PhuthuTraituyen = PhuthuTraituyen;
				
			item.DongiaNgoaigio = DongiaNgoaigio;
				
			item.PhuthuNgoaigio = PhuthuNgoaigio;
				
			item.MaDoituongKcb = MaDoituongKcb;
				
			item.IdPhikemtheo = IdPhikemtheo;
				
			item.IdPhikemtheongoaigio = IdPhikemtheongoaigio;
				
			item.NhomBaocao = NhomBaocao;
				
			item.TuTuc = TuTuc;
				
			item.SttHthi = SttHthi;
				
			item.MotaThem = MotaThem;
				
	        item.Save(UserName);
	    }
    }
}