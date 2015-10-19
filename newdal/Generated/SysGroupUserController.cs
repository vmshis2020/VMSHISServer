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
    /// Controller class for Sys_GroupUser
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysGroupUserController
    {
        // Preload our schema..
        SysGroupUser thisSchemaLoad = new SysGroupUser();
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
        public SysGroupUserCollection FetchAll()
        {
            SysGroupUserCollection coll = new SysGroupUserCollection();
            Query qry = new Query(SysGroupUser.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysGroupUserCollection FetchByID(object BranchID)
        {
            SysGroupUserCollection coll = new SysGroupUserCollection().Where("BranchID", BranchID).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysGroupUserCollection FetchByQuery(Query qry)
        {
            SysGroupUserCollection coll = new SysGroupUserCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object BranchID)
        {
            return (SysGroupUser.Delete(BranchID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object BranchID)
        {
            return (SysGroupUser.Destroy(BranchID) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string BranchID,int GroupID,string UserID)
        {
            Query qry = new Query(SysGroupUser.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("BranchID", BranchID).AND("GroupID", GroupID).AND("UserID", UserID);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string BranchID,int GroupID,string UserID)
	    {
		    SysGroupUser item = new SysGroupUser();
		    
            item.BranchID = BranchID;
            
            item.GroupID = GroupID;
            
            item.UserID = UserID;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string BranchID,int GroupID,string UserID)
	    {
		    SysGroupUser item = new SysGroupUser();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.BranchID = BranchID;
				
			item.GroupID = GroupID;
				
			item.UserID = UserID;
				
	        item.Save(UserName);
	    }
    }
}
