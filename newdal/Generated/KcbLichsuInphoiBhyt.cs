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
	/// Strongly-typed collection for the KcbLichsuInphoiBhyt class.
	/// </summary>
    [Serializable]
	public partial class KcbLichsuInphoiBhytCollection : ActiveList<KcbLichsuInphoiBhyt, KcbLichsuInphoiBhytCollection>
	{	   
		public KcbLichsuInphoiBhytCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>KcbLichsuInphoiBhytCollection</returns>
		public KcbLichsuInphoiBhytCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                KcbLichsuInphoiBhyt o = this[i];
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
	/// This is an ActiveRecord class which wraps the kcb_lichsu_inphoi_bhyt table.
	/// </summary>
	[Serializable]
	public partial class KcbLichsuInphoiBhyt : ActiveRecord<KcbLichsuInphoiBhyt>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public KcbLichsuInphoiBhyt()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public KcbLichsuInphoiBhyt(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public KcbLichsuInphoiBhyt(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public KcbLichsuInphoiBhyt(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("kcb_lichsu_inphoi_bhyt", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "id";
				colvarId.DataType = DbType.Int64;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = true;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarIdBenhnhan = new TableSchema.TableColumn(schema);
				colvarIdBenhnhan.ColumnName = "id_benhnhan";
				colvarIdBenhnhan.DataType = DbType.Int64;
				colvarIdBenhnhan.MaxLength = 0;
				colvarIdBenhnhan.AutoIncrement = false;
				colvarIdBenhnhan.IsNullable = false;
				colvarIdBenhnhan.IsPrimaryKey = false;
				colvarIdBenhnhan.IsForeignKey = false;
				colvarIdBenhnhan.IsReadOnly = false;
				colvarIdBenhnhan.DefaultSetting = @"";
				colvarIdBenhnhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBenhnhan);
				
				TableSchema.TableColumn colvarMaLuotkham = new TableSchema.TableColumn(schema);
				colvarMaLuotkham.ColumnName = "ma_luotkham";
				colvarMaLuotkham.DataType = DbType.String;
				colvarMaLuotkham.MaxLength = 10;
				colvarMaLuotkham.AutoIncrement = false;
				colvarMaLuotkham.IsNullable = false;
				colvarMaLuotkham.IsPrimaryKey = false;
				colvarMaLuotkham.IsForeignKey = false;
				colvarMaLuotkham.IsReadOnly = false;
				colvarMaLuotkham.DefaultSetting = @"";
				colvarMaLuotkham.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaLuotkham);
				
				TableSchema.TableColumn colvarIdPhieuDct = new TableSchema.TableColumn(schema);
				colvarIdPhieuDct.ColumnName = "id_phieu_dct";
				colvarIdPhieuDct.DataType = DbType.Int64;
				colvarIdPhieuDct.MaxLength = 0;
				colvarIdPhieuDct.AutoIncrement = false;
				colvarIdPhieuDct.IsNullable = false;
				colvarIdPhieuDct.IsPrimaryKey = false;
				colvarIdPhieuDct.IsForeignKey = false;
				colvarIdPhieuDct.IsReadOnly = false;
				colvarIdPhieuDct.DefaultSetting = @"";
				colvarIdPhieuDct.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdPhieuDct);
				
				TableSchema.TableColumn colvarHanhDong = new TableSchema.TableColumn(schema);
				colvarHanhDong.ColumnName = "hanh_dong";
				colvarHanhDong.DataType = DbType.String;
				colvarHanhDong.MaxLength = 5;
				colvarHanhDong.AutoIncrement = false;
				colvarHanhDong.IsNullable = true;
				colvarHanhDong.IsPrimaryKey = false;
				colvarHanhDong.IsForeignKey = false;
				colvarHanhDong.IsReadOnly = false;
				colvarHanhDong.DefaultSetting = @"";
				colvarHanhDong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHanhDong);
				
				TableSchema.TableColumn colvarNguoiTao = new TableSchema.TableColumn(schema);
				colvarNguoiTao.ColumnName = "nguoi_tao";
				colvarNguoiTao.DataType = DbType.Int32;
				colvarNguoiTao.MaxLength = 0;
				colvarNguoiTao.AutoIncrement = false;
				colvarNguoiTao.IsNullable = true;
				colvarNguoiTao.IsPrimaryKey = false;
				colvarNguoiTao.IsForeignKey = false;
				colvarNguoiTao.IsReadOnly = false;
				colvarNguoiTao.DefaultSetting = @"";
				colvarNguoiTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiTao);
				
				TableSchema.TableColumn colvarNgayTao = new TableSchema.TableColumn(schema);
				colvarNgayTao.ColumnName = "ngay_tao";
				colvarNgayTao.DataType = DbType.DateTime;
				colvarNgayTao.MaxLength = 0;
				colvarNgayTao.AutoIncrement = false;
				colvarNgayTao.IsNullable = true;
				colvarNgayTao.IsPrimaryKey = false;
				colvarNgayTao.IsForeignKey = false;
				colvarNgayTao.IsReadOnly = false;
				colvarNgayTao.DefaultSetting = @"";
				colvarNgayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTao);
				
				TableSchema.TableColumn colvarNoiDung = new TableSchema.TableColumn(schema);
				colvarNoiDung.ColumnName = "noi_dung";
				colvarNoiDung.DataType = DbType.String;
				colvarNoiDung.MaxLength = 100;
				colvarNoiDung.AutoIncrement = false;
				colvarNoiDung.IsNullable = true;
				colvarNoiDung.IsPrimaryKey = false;
				colvarNoiDung.IsForeignKey = false;
				colvarNoiDung.IsReadOnly = false;
				colvarNoiDung.DefaultSetting = @"";
				colvarNoiDung.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNoiDung);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("kcb_lichsu_inphoi_bhyt",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Id")]
		[Bindable(true)]
		public long Id 
		{
			get { return GetColumnValue<long>(Columns.Id); }
			set { SetColumnValue(Columns.Id, value); }
		}
		  
		[XmlAttribute("IdBenhnhan")]
		[Bindable(true)]
		public long IdBenhnhan 
		{
			get { return GetColumnValue<long>(Columns.IdBenhnhan); }
			set { SetColumnValue(Columns.IdBenhnhan, value); }
		}
		  
		[XmlAttribute("MaLuotkham")]
		[Bindable(true)]
		public string MaLuotkham 
		{
			get { return GetColumnValue<string>(Columns.MaLuotkham); }
			set { SetColumnValue(Columns.MaLuotkham, value); }
		}
		  
		[XmlAttribute("IdPhieuDct")]
		[Bindable(true)]
		public long IdPhieuDct 
		{
			get { return GetColumnValue<long>(Columns.IdPhieuDct); }
			set { SetColumnValue(Columns.IdPhieuDct, value); }
		}
		  
		[XmlAttribute("HanhDong")]
		[Bindable(true)]
		public string HanhDong 
		{
			get { return GetColumnValue<string>(Columns.HanhDong); }
			set { SetColumnValue(Columns.HanhDong, value); }
		}
		  
		[XmlAttribute("NguoiTao")]
		[Bindable(true)]
		public int? NguoiTao 
		{
			get { return GetColumnValue<int?>(Columns.NguoiTao); }
			set { SetColumnValue(Columns.NguoiTao, value); }
		}
		  
		[XmlAttribute("NgayTao")]
		[Bindable(true)]
		public DateTime? NgayTao 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayTao); }
			set { SetColumnValue(Columns.NgayTao, value); }
		}
		  
		[XmlAttribute("NoiDung")]
		[Bindable(true)]
		public string NoiDung 
		{
			get { return GetColumnValue<string>(Columns.NoiDung); }
			set { SetColumnValue(Columns.NoiDung, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(long varIdBenhnhan,string varMaLuotkham,long varIdPhieuDct,string varHanhDong,int? varNguoiTao,DateTime? varNgayTao,string varNoiDung)
		{
			KcbLichsuInphoiBhyt item = new KcbLichsuInphoiBhyt();
			
			item.IdBenhnhan = varIdBenhnhan;
			
			item.MaLuotkham = varMaLuotkham;
			
			item.IdPhieuDct = varIdPhieuDct;
			
			item.HanhDong = varHanhDong;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgayTao = varNgayTao;
			
			item.NoiDung = varNoiDung;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varId,long varIdBenhnhan,string varMaLuotkham,long varIdPhieuDct,string varHanhDong,int? varNguoiTao,DateTime? varNgayTao,string varNoiDung)
		{
			KcbLichsuInphoiBhyt item = new KcbLichsuInphoiBhyt();
			
				item.Id = varId;
			
				item.IdBenhnhan = varIdBenhnhan;
			
				item.MaLuotkham = varMaLuotkham;
			
				item.IdPhieuDct = varIdPhieuDct;
			
				item.HanhDong = varHanhDong;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgayTao = varNgayTao;
			
				item.NoiDung = varNoiDung;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdBenhnhanColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn MaLuotkhamColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IdPhieuDctColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn HanhDongColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn NoiDungColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"id";
			 public static string IdBenhnhan = @"id_benhnhan";
			 public static string MaLuotkham = @"ma_luotkham";
			 public static string IdPhieuDct = @"id_phieu_dct";
			 public static string HanhDong = @"hanh_dong";
			 public static string NguoiTao = @"nguoi_tao";
			 public static string NgayTao = @"ngay_tao";
			 public static string NoiDung = @"noi_dung";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}