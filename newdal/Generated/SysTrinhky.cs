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
	/// Strongly-typed collection for the SysTrinhky class.
	/// </summary>
    [Serializable]
	public partial class SysTrinhkyCollection : ActiveList<SysTrinhky, SysTrinhkyCollection>
	{	   
		public SysTrinhkyCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysTrinhkyCollection</returns>
		public SysTrinhkyCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysTrinhky o = this[i];
                foreach (SubSonic.Where w in this.wheres)
                {
                    bool remove = false;
                    System.Reflection.PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case SubSonic.Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        this.Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
		
		
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the Sys_Trinhky table.
	/// </summary>
	[Serializable]
	public partial class SysTrinhky : ActiveRecord<SysTrinhky>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public SysTrinhky()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysTrinhky(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysTrinhky(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysTrinhky(string columnName, object columnValue)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByParam(columnName,columnValue);
		}
		
		protected static void SetSQLProps() { GetTableSchema(); }
		
		#endregion
		
		#region Schema and Query Accessor	
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("Sys_Trinhky", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarReportName = new TableSchema.TableColumn(schema);
				colvarReportName.ColumnName = "ReportName";
				colvarReportName.DataType = DbType.String;
				colvarReportName.MaxLength = 300;
				colvarReportName.AutoIncrement = false;
				colvarReportName.IsNullable = false;
				colvarReportName.IsPrimaryKey = true;
				colvarReportName.IsForeignKey = false;
				colvarReportName.IsReadOnly = false;
				colvarReportName.DefaultSetting = @"";
				colvarReportName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportName);
				
				TableSchema.TableColumn colvarObjectName = new TableSchema.TableColumn(schema);
				colvarObjectName.ColumnName = "ObjectName";
				colvarObjectName.DataType = DbType.String;
				colvarObjectName.MaxLength = 100;
				colvarObjectName.AutoIncrement = false;
				colvarObjectName.IsNullable = false;
				colvarObjectName.IsPrimaryKey = true;
				colvarObjectName.IsForeignKey = false;
				colvarObjectName.IsReadOnly = false;
				colvarObjectName.DefaultSetting = @"";
				colvarObjectName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarObjectName);
				
				TableSchema.TableColumn colvarTrinhky = new TableSchema.TableColumn(schema);
				colvarTrinhky.ColumnName = "trinhky";
				colvarTrinhky.DataType = DbType.String;
				colvarTrinhky.MaxLength = 1073741823;
				colvarTrinhky.AutoIncrement = false;
				colvarTrinhky.IsNullable = true;
				colvarTrinhky.IsPrimaryKey = false;
				colvarTrinhky.IsForeignKey = false;
				colvarTrinhky.IsReadOnly = false;
				colvarTrinhky.DefaultSetting = @"";
				colvarTrinhky.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrinhky);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("Sys_Trinhky",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("ReportName")]
		[Bindable(true)]
		public string ReportName 
		{
			get { return GetColumnValue<string>(Columns.ReportName); }
			set { SetColumnValue(Columns.ReportName, value); }
		}
		  
		[XmlAttribute("ObjectName")]
		[Bindable(true)]
		public string ObjectName 
		{
			get { return GetColumnValue<string>(Columns.ObjectName); }
			set { SetColumnValue(Columns.ObjectName, value); }
		}
		  
		[XmlAttribute("Trinhky")]
		[Bindable(true)]
		public string Trinhky 
		{
			get { return GetColumnValue<string>(Columns.Trinhky); }
			set { SetColumnValue(Columns.Trinhky, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varReportName,string varObjectName,string varTrinhky)
		{
			SysTrinhky item = new SysTrinhky();
			
			item.ReportName = varReportName;
			
			item.ObjectName = varObjectName;
			
			item.Trinhky = varTrinhky;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(string varReportName,string varObjectName,string varTrinhky)
		{
			SysTrinhky item = new SysTrinhky();
			
				item.ReportName = varReportName;
			
				item.ObjectName = varObjectName;
			
				item.Trinhky = varTrinhky;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn ReportNameColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn ObjectNameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TrinhkyColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string ReportName = @"ReportName";
			 public static string ObjectName = @"ObjectName";
			 public static string Trinhky = @"trinhky";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
