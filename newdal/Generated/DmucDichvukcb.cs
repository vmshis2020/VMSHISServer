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
	/// Strongly-typed collection for the DmucDichvukcb class.
	/// </summary>
    [Serializable]
	public partial class DmucDichvukcbCollection : ActiveList<DmucDichvukcb, DmucDichvukcbCollection>
	{	   
		public DmucDichvukcbCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DmucDichvukcbCollection</returns>
		public DmucDichvukcbCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DmucDichvukcb o = this[i];
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
	/// This is an ActiveRecord class which wraps the dmuc_dichvukcb table.
	/// </summary>
	[Serializable]
	public partial class DmucDichvukcb : ActiveRecord<DmucDichvukcb>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DmucDichvukcb()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DmucDichvukcb(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DmucDichvukcb(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DmucDichvukcb(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("dmuc_dichvukcb", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdDichvukcb = new TableSchema.TableColumn(schema);
				colvarIdDichvukcb.ColumnName = "id_dichvukcb";
				colvarIdDichvukcb.DataType = DbType.Int32;
				colvarIdDichvukcb.MaxLength = 0;
				colvarIdDichvukcb.AutoIncrement = true;
				colvarIdDichvukcb.IsNullable = false;
				colvarIdDichvukcb.IsPrimaryKey = true;
				colvarIdDichvukcb.IsForeignKey = false;
				colvarIdDichvukcb.IsReadOnly = false;
				colvarIdDichvukcb.DefaultSetting = @"";
				colvarIdDichvukcb.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdDichvukcb);
				
				TableSchema.TableColumn colvarMaDichvukcb = new TableSchema.TableColumn(schema);
				colvarMaDichvukcb.ColumnName = "ma_dichvukcb";
				colvarMaDichvukcb.DataType = DbType.String;
				colvarMaDichvukcb.MaxLength = 50;
				colvarMaDichvukcb.AutoIncrement = false;
				colvarMaDichvukcb.IsNullable = true;
				colvarMaDichvukcb.IsPrimaryKey = false;
				colvarMaDichvukcb.IsForeignKey = false;
				colvarMaDichvukcb.IsReadOnly = false;
				colvarMaDichvukcb.DefaultSetting = @"";
				colvarMaDichvukcb.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaDichvukcb);
				
				TableSchema.TableColumn colvarTenDichvukcb = new TableSchema.TableColumn(schema);
				colvarTenDichvukcb.ColumnName = "ten_dichvukcb";
				colvarTenDichvukcb.DataType = DbType.String;
				colvarTenDichvukcb.MaxLength = 100;
				colvarTenDichvukcb.AutoIncrement = false;
				colvarTenDichvukcb.IsNullable = true;
				colvarTenDichvukcb.IsPrimaryKey = false;
				colvarTenDichvukcb.IsForeignKey = false;
				colvarTenDichvukcb.IsReadOnly = false;
				colvarTenDichvukcb.DefaultSetting = @"";
				colvarTenDichvukcb.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenDichvukcb);
				
				TableSchema.TableColumn colvarIdKieukham = new TableSchema.TableColumn(schema);
				colvarIdKieukham.ColumnName = "id_kieukham";
				colvarIdKieukham.DataType = DbType.Int16;
				colvarIdKieukham.MaxLength = 0;
				colvarIdKieukham.AutoIncrement = false;
				colvarIdKieukham.IsNullable = false;
				colvarIdKieukham.IsPrimaryKey = false;
				colvarIdKieukham.IsForeignKey = false;
				colvarIdKieukham.IsReadOnly = false;
				colvarIdKieukham.DefaultSetting = @"";
				colvarIdKieukham.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKieukham);
				
				TableSchema.TableColumn colvarIdKhoaphong = new TableSchema.TableColumn(schema);
				colvarIdKhoaphong.ColumnName = "id_khoaphong";
				colvarIdKhoaphong.DataType = DbType.Int16;
				colvarIdKhoaphong.MaxLength = 0;
				colvarIdKhoaphong.AutoIncrement = false;
				colvarIdKhoaphong.IsNullable = false;
				colvarIdKhoaphong.IsPrimaryKey = false;
				colvarIdKhoaphong.IsForeignKey = false;
				colvarIdKhoaphong.IsReadOnly = false;
				colvarIdKhoaphong.DefaultSetting = @"";
				colvarIdKhoaphong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKhoaphong);
				
				TableSchema.TableColumn colvarIdBacsy = new TableSchema.TableColumn(schema);
				colvarIdBacsy.ColumnName = "id_bacsy";
				colvarIdBacsy.DataType = DbType.Int16;
				colvarIdBacsy.MaxLength = 0;
				colvarIdBacsy.AutoIncrement = false;
				colvarIdBacsy.IsNullable = false;
				colvarIdBacsy.IsPrimaryKey = false;
				colvarIdBacsy.IsForeignKey = false;
				colvarIdBacsy.IsReadOnly = false;
				
						colvarIdBacsy.DefaultSetting = @"((-1))";
				colvarIdBacsy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBacsy);
				
				TableSchema.TableColumn colvarIdDoituongKcb = new TableSchema.TableColumn(schema);
				colvarIdDoituongKcb.ColumnName = "id_doituong_kcb";
				colvarIdDoituongKcb.DataType = DbType.Int16;
				colvarIdDoituongKcb.MaxLength = 0;
				colvarIdDoituongKcb.AutoIncrement = false;
				colvarIdDoituongKcb.IsNullable = false;
				colvarIdDoituongKcb.IsPrimaryKey = false;
				colvarIdDoituongKcb.IsForeignKey = false;
				colvarIdDoituongKcb.IsReadOnly = false;
				colvarIdDoituongKcb.DefaultSetting = @"";
				colvarIdDoituongKcb.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdDoituongKcb);
				
				TableSchema.TableColumn colvarIdPhongkham = new TableSchema.TableColumn(schema);
				colvarIdPhongkham.ColumnName = "id_phongkham";
				colvarIdPhongkham.DataType = DbType.Int16;
				colvarIdPhongkham.MaxLength = 0;
				colvarIdPhongkham.AutoIncrement = false;
				colvarIdPhongkham.IsNullable = false;
				colvarIdPhongkham.IsPrimaryKey = false;
				colvarIdPhongkham.IsForeignKey = false;
				colvarIdPhongkham.IsReadOnly = false;
				colvarIdPhongkham.DefaultSetting = @"";
				colvarIdPhongkham.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdPhongkham);
				
				TableSchema.TableColumn colvarDonGia = new TableSchema.TableColumn(schema);
				colvarDonGia.ColumnName = "don_gia";
				colvarDonGia.DataType = DbType.Decimal;
				colvarDonGia.MaxLength = 0;
				colvarDonGia.AutoIncrement = false;
				colvarDonGia.IsNullable = false;
				colvarDonGia.IsPrimaryKey = false;
				colvarDonGia.IsForeignKey = false;
				colvarDonGia.IsReadOnly = false;
				colvarDonGia.DefaultSetting = @"";
				colvarDonGia.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDonGia);
				
				TableSchema.TableColumn colvarPhuthuDungtuyen = new TableSchema.TableColumn(schema);
				colvarPhuthuDungtuyen.ColumnName = "phuthu_dungtuyen";
				colvarPhuthuDungtuyen.DataType = DbType.Decimal;
				colvarPhuthuDungtuyen.MaxLength = 0;
				colvarPhuthuDungtuyen.AutoIncrement = false;
				colvarPhuthuDungtuyen.IsNullable = true;
				colvarPhuthuDungtuyen.IsPrimaryKey = false;
				colvarPhuthuDungtuyen.IsForeignKey = false;
				colvarPhuthuDungtuyen.IsReadOnly = false;
				colvarPhuthuDungtuyen.DefaultSetting = @"";
				colvarPhuthuDungtuyen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhuthuDungtuyen);
				
				TableSchema.TableColumn colvarPhuthuTraituyen = new TableSchema.TableColumn(schema);
				colvarPhuthuTraituyen.ColumnName = "phuthu_traituyen";
				colvarPhuthuTraituyen.DataType = DbType.Decimal;
				colvarPhuthuTraituyen.MaxLength = 0;
				colvarPhuthuTraituyen.AutoIncrement = false;
				colvarPhuthuTraituyen.IsNullable = true;
				colvarPhuthuTraituyen.IsPrimaryKey = false;
				colvarPhuthuTraituyen.IsForeignKey = false;
				colvarPhuthuTraituyen.IsReadOnly = false;
				colvarPhuthuTraituyen.DefaultSetting = @"";
				colvarPhuthuTraituyen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhuthuTraituyen);
				
				TableSchema.TableColumn colvarDongiaNgoaigio = new TableSchema.TableColumn(schema);
				colvarDongiaNgoaigio.ColumnName = "dongia_ngoaigio";
				colvarDongiaNgoaigio.DataType = DbType.Decimal;
				colvarDongiaNgoaigio.MaxLength = 0;
				colvarDongiaNgoaigio.AutoIncrement = false;
				colvarDongiaNgoaigio.IsNullable = true;
				colvarDongiaNgoaigio.IsPrimaryKey = false;
				colvarDongiaNgoaigio.IsForeignKey = false;
				colvarDongiaNgoaigio.IsReadOnly = false;
				colvarDongiaNgoaigio.DefaultSetting = @"";
				colvarDongiaNgoaigio.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDongiaNgoaigio);
				
				TableSchema.TableColumn colvarPhuthuNgoaigio = new TableSchema.TableColumn(schema);
				colvarPhuthuNgoaigio.ColumnName = "phuthu_ngoaigio";
				colvarPhuthuNgoaigio.DataType = DbType.Decimal;
				colvarPhuthuNgoaigio.MaxLength = 0;
				colvarPhuthuNgoaigio.AutoIncrement = false;
				colvarPhuthuNgoaigio.IsNullable = true;
				colvarPhuthuNgoaigio.IsPrimaryKey = false;
				colvarPhuthuNgoaigio.IsForeignKey = false;
				colvarPhuthuNgoaigio.IsReadOnly = false;
				colvarPhuthuNgoaigio.DefaultSetting = @"";
				colvarPhuthuNgoaigio.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhuthuNgoaigio);
				
				TableSchema.TableColumn colvarMaDoituongKcb = new TableSchema.TableColumn(schema);
				colvarMaDoituongKcb.ColumnName = "ma_doituong_kcb";
				colvarMaDoituongKcb.DataType = DbType.String;
				colvarMaDoituongKcb.MaxLength = 50;
				colvarMaDoituongKcb.AutoIncrement = false;
				colvarMaDoituongKcb.IsNullable = true;
				colvarMaDoituongKcb.IsPrimaryKey = false;
				colvarMaDoituongKcb.IsForeignKey = false;
				colvarMaDoituongKcb.IsReadOnly = false;
				colvarMaDoituongKcb.DefaultSetting = @"";
				colvarMaDoituongKcb.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaDoituongKcb);
				
				TableSchema.TableColumn colvarIdPhikemtheo = new TableSchema.TableColumn(schema);
				colvarIdPhikemtheo.ColumnName = "id_phikemtheo";
				colvarIdPhikemtheo.DataType = DbType.Int32;
				colvarIdPhikemtheo.MaxLength = 0;
				colvarIdPhikemtheo.AutoIncrement = false;
				colvarIdPhikemtheo.IsNullable = true;
				colvarIdPhikemtheo.IsPrimaryKey = false;
				colvarIdPhikemtheo.IsForeignKey = false;
				colvarIdPhikemtheo.IsReadOnly = false;
				colvarIdPhikemtheo.DefaultSetting = @"";
				colvarIdPhikemtheo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdPhikemtheo);
				
				TableSchema.TableColumn colvarIdPhikemtheongoaigio = new TableSchema.TableColumn(schema);
				colvarIdPhikemtheongoaigio.ColumnName = "id_phikemtheongoaigio";
				colvarIdPhikemtheongoaigio.DataType = DbType.Int32;
				colvarIdPhikemtheongoaigio.MaxLength = 0;
				colvarIdPhikemtheongoaigio.AutoIncrement = false;
				colvarIdPhikemtheongoaigio.IsNullable = true;
				colvarIdPhikemtheongoaigio.IsPrimaryKey = false;
				colvarIdPhikemtheongoaigio.IsForeignKey = false;
				colvarIdPhikemtheongoaigio.IsReadOnly = false;
				colvarIdPhikemtheongoaigio.DefaultSetting = @"";
				colvarIdPhikemtheongoaigio.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdPhikemtheongoaigio);
				
				TableSchema.TableColumn colvarNhomBaocao = new TableSchema.TableColumn(schema);
				colvarNhomBaocao.ColumnName = "nhom_baocao";
				colvarNhomBaocao.DataType = DbType.String;
				colvarNhomBaocao.MaxLength = 20;
				colvarNhomBaocao.AutoIncrement = false;
				colvarNhomBaocao.IsNullable = true;
				colvarNhomBaocao.IsPrimaryKey = false;
				colvarNhomBaocao.IsForeignKey = false;
				colvarNhomBaocao.IsReadOnly = false;
				colvarNhomBaocao.DefaultSetting = @"";
				colvarNhomBaocao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNhomBaocao);
				
				TableSchema.TableColumn colvarTuTuc = new TableSchema.TableColumn(schema);
				colvarTuTuc.ColumnName = "tu_tuc";
				colvarTuTuc.DataType = DbType.Byte;
				colvarTuTuc.MaxLength = 0;
				colvarTuTuc.AutoIncrement = false;
				colvarTuTuc.IsNullable = true;
				colvarTuTuc.IsPrimaryKey = false;
				colvarTuTuc.IsForeignKey = false;
				colvarTuTuc.IsReadOnly = false;
				colvarTuTuc.DefaultSetting = @"";
				colvarTuTuc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTuTuc);
				
				TableSchema.TableColumn colvarSttHthi = new TableSchema.TableColumn(schema);
				colvarSttHthi.ColumnName = "stt_hthi";
				colvarSttHthi.DataType = DbType.Int16;
				colvarSttHthi.MaxLength = 0;
				colvarSttHthi.AutoIncrement = false;
				colvarSttHthi.IsNullable = true;
				colvarSttHthi.IsPrimaryKey = false;
				colvarSttHthi.IsForeignKey = false;
				colvarSttHthi.IsReadOnly = false;
				colvarSttHthi.DefaultSetting = @"";
				colvarSttHthi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSttHthi);
				
				TableSchema.TableColumn colvarMotaThem = new TableSchema.TableColumn(schema);
				colvarMotaThem.ColumnName = "mota_them";
				colvarMotaThem.DataType = DbType.String;
				colvarMotaThem.MaxLength = 255;
				colvarMotaThem.AutoIncrement = false;
				colvarMotaThem.IsNullable = true;
				colvarMotaThem.IsPrimaryKey = false;
				colvarMotaThem.IsForeignKey = false;
				colvarMotaThem.IsReadOnly = false;
				colvarMotaThem.DefaultSetting = @"";
				colvarMotaThem.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMotaThem);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("dmuc_dichvukcb",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdDichvukcb")]
		[Bindable(true)]
		public int IdDichvukcb 
		{
			get { return GetColumnValue<int>(Columns.IdDichvukcb); }
			set { SetColumnValue(Columns.IdDichvukcb, value); }
		}
		  
		[XmlAttribute("MaDichvukcb")]
		[Bindable(true)]
		public string MaDichvukcb 
		{
			get { return GetColumnValue<string>(Columns.MaDichvukcb); }
			set { SetColumnValue(Columns.MaDichvukcb, value); }
		}
		  
		[XmlAttribute("TenDichvukcb")]
		[Bindable(true)]
		public string TenDichvukcb 
		{
			get { return GetColumnValue<string>(Columns.TenDichvukcb); }
			set { SetColumnValue(Columns.TenDichvukcb, value); }
		}
		  
		[XmlAttribute("IdKieukham")]
		[Bindable(true)]
		public short IdKieukham 
		{
			get { return GetColumnValue<short>(Columns.IdKieukham); }
			set { SetColumnValue(Columns.IdKieukham, value); }
		}
		  
		[XmlAttribute("IdKhoaphong")]
		[Bindable(true)]
		public short IdKhoaphong 
		{
			get { return GetColumnValue<short>(Columns.IdKhoaphong); }
			set { SetColumnValue(Columns.IdKhoaphong, value); }
		}
		  
		[XmlAttribute("IdBacsy")]
		[Bindable(true)]
		public short IdBacsy 
		{
			get { return GetColumnValue<short>(Columns.IdBacsy); }
			set { SetColumnValue(Columns.IdBacsy, value); }
		}
		  
		[XmlAttribute("IdDoituongKcb")]
		[Bindable(true)]
		public short IdDoituongKcb 
		{
			get { return GetColumnValue<short>(Columns.IdDoituongKcb); }
			set { SetColumnValue(Columns.IdDoituongKcb, value); }
		}
		  
		[XmlAttribute("IdPhongkham")]
		[Bindable(true)]
		public short IdPhongkham 
		{
			get { return GetColumnValue<short>(Columns.IdPhongkham); }
			set { SetColumnValue(Columns.IdPhongkham, value); }
		}
		  
		[XmlAttribute("DonGia")]
		[Bindable(true)]
		public decimal DonGia 
		{
			get { return GetColumnValue<decimal>(Columns.DonGia); }
			set { SetColumnValue(Columns.DonGia, value); }
		}
		  
		[XmlAttribute("PhuthuDungtuyen")]
		[Bindable(true)]
		public decimal? PhuthuDungtuyen 
		{
			get { return GetColumnValue<decimal?>(Columns.PhuthuDungtuyen); }
			set { SetColumnValue(Columns.PhuthuDungtuyen, value); }
		}
		  
		[XmlAttribute("PhuthuTraituyen")]
		[Bindable(true)]
		public decimal? PhuthuTraituyen 
		{
			get { return GetColumnValue<decimal?>(Columns.PhuthuTraituyen); }
			set { SetColumnValue(Columns.PhuthuTraituyen, value); }
		}
		  
		[XmlAttribute("DongiaNgoaigio")]
		[Bindable(true)]
		public decimal? DongiaNgoaigio 
		{
			get { return GetColumnValue<decimal?>(Columns.DongiaNgoaigio); }
			set { SetColumnValue(Columns.DongiaNgoaigio, value); }
		}
		  
		[XmlAttribute("PhuthuNgoaigio")]
		[Bindable(true)]
		public decimal? PhuthuNgoaigio 
		{
			get { return GetColumnValue<decimal?>(Columns.PhuthuNgoaigio); }
			set { SetColumnValue(Columns.PhuthuNgoaigio, value); }
		}
		  
		[XmlAttribute("MaDoituongKcb")]
		[Bindable(true)]
		public string MaDoituongKcb 
		{
			get { return GetColumnValue<string>(Columns.MaDoituongKcb); }
			set { SetColumnValue(Columns.MaDoituongKcb, value); }
		}
		  
		[XmlAttribute("IdPhikemtheo")]
		[Bindable(true)]
		public int? IdPhikemtheo 
		{
			get { return GetColumnValue<int?>(Columns.IdPhikemtheo); }
			set { SetColumnValue(Columns.IdPhikemtheo, value); }
		}
		  
		[XmlAttribute("IdPhikemtheongoaigio")]
		[Bindable(true)]
		public int? IdPhikemtheongoaigio 
		{
			get { return GetColumnValue<int?>(Columns.IdPhikemtheongoaigio); }
			set { SetColumnValue(Columns.IdPhikemtheongoaigio, value); }
		}
		  
		[XmlAttribute("NhomBaocao")]
		[Bindable(true)]
		public string NhomBaocao 
		{
			get { return GetColumnValue<string>(Columns.NhomBaocao); }
			set { SetColumnValue(Columns.NhomBaocao, value); }
		}
		  
		[XmlAttribute("TuTuc")]
		[Bindable(true)]
		public byte? TuTuc 
		{
			get { return GetColumnValue<byte?>(Columns.TuTuc); }
			set { SetColumnValue(Columns.TuTuc, value); }
		}
		  
		[XmlAttribute("SttHthi")]
		[Bindable(true)]
		public short? SttHthi 
		{
			get { return GetColumnValue<short?>(Columns.SttHthi); }
			set { SetColumnValue(Columns.SttHthi, value); }
		}
		  
		[XmlAttribute("MotaThem")]
		[Bindable(true)]
		public string MotaThem 
		{
			get { return GetColumnValue<string>(Columns.MotaThem); }
			set { SetColumnValue(Columns.MotaThem, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMaDichvukcb,string varTenDichvukcb,short varIdKieukham,short varIdKhoaphong,short varIdBacsy,short varIdDoituongKcb,short varIdPhongkham,decimal varDonGia,decimal? varPhuthuDungtuyen,decimal? varPhuthuTraituyen,decimal? varDongiaNgoaigio,decimal? varPhuthuNgoaigio,string varMaDoituongKcb,int? varIdPhikemtheo,int? varIdPhikemtheongoaigio,string varNhomBaocao,byte? varTuTuc,short? varSttHthi,string varMotaThem)
		{
			DmucDichvukcb item = new DmucDichvukcb();
			
			item.MaDichvukcb = varMaDichvukcb;
			
			item.TenDichvukcb = varTenDichvukcb;
			
			item.IdKieukham = varIdKieukham;
			
			item.IdKhoaphong = varIdKhoaphong;
			
			item.IdBacsy = varIdBacsy;
			
			item.IdDoituongKcb = varIdDoituongKcb;
			
			item.IdPhongkham = varIdPhongkham;
			
			item.DonGia = varDonGia;
			
			item.PhuthuDungtuyen = varPhuthuDungtuyen;
			
			item.PhuthuTraituyen = varPhuthuTraituyen;
			
			item.DongiaNgoaigio = varDongiaNgoaigio;
			
			item.PhuthuNgoaigio = varPhuthuNgoaigio;
			
			item.MaDoituongKcb = varMaDoituongKcb;
			
			item.IdPhikemtheo = varIdPhikemtheo;
			
			item.IdPhikemtheongoaigio = varIdPhikemtheongoaigio;
			
			item.NhomBaocao = varNhomBaocao;
			
			item.TuTuc = varTuTuc;
			
			item.SttHthi = varSttHthi;
			
			item.MotaThem = varMotaThem;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varIdDichvukcb,string varMaDichvukcb,string varTenDichvukcb,short varIdKieukham,short varIdKhoaphong,short varIdBacsy,short varIdDoituongKcb,short varIdPhongkham,decimal varDonGia,decimal? varPhuthuDungtuyen,decimal? varPhuthuTraituyen,decimal? varDongiaNgoaigio,decimal? varPhuthuNgoaigio,string varMaDoituongKcb,int? varIdPhikemtheo,int? varIdPhikemtheongoaigio,string varNhomBaocao,byte? varTuTuc,short? varSttHthi,string varMotaThem)
		{
			DmucDichvukcb item = new DmucDichvukcb();
			
				item.IdDichvukcb = varIdDichvukcb;
			
				item.MaDichvukcb = varMaDichvukcb;
			
				item.TenDichvukcb = varTenDichvukcb;
			
				item.IdKieukham = varIdKieukham;
			
				item.IdKhoaphong = varIdKhoaphong;
			
				item.IdBacsy = varIdBacsy;
			
				item.IdDoituongKcb = varIdDoituongKcb;
			
				item.IdPhongkham = varIdPhongkham;
			
				item.DonGia = varDonGia;
			
				item.PhuthuDungtuyen = varPhuthuDungtuyen;
			
				item.PhuthuTraituyen = varPhuthuTraituyen;
			
				item.DongiaNgoaigio = varDongiaNgoaigio;
			
				item.PhuthuNgoaigio = varPhuthuNgoaigio;
			
				item.MaDoituongKcb = varMaDoituongKcb;
			
				item.IdPhikemtheo = varIdPhikemtheo;
			
				item.IdPhikemtheongoaigio = varIdPhikemtheongoaigio;
			
				item.NhomBaocao = varNhomBaocao;
			
				item.TuTuc = varTuTuc;
			
				item.SttHthi = varSttHthi;
			
				item.MotaThem = varMotaThem;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdDichvukcbColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MaDichvukcbColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TenDichvukcbColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKieukhamColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKhoaphongColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn IdBacsyColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn IdDoituongKcbColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn IdPhongkhamColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn DonGiaColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn PhuthuDungtuyenColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn PhuthuTraituyenColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn DongiaNgoaigioColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn PhuthuNgoaigioColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn MaDoituongKcbColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn IdPhikemtheoColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn IdPhikemtheongoaigioColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn NhomBaocaoColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn TuTucColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn SttHthiColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn MotaThemColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdDichvukcb = @"id_dichvukcb";
			 public static string MaDichvukcb = @"ma_dichvukcb";
			 public static string TenDichvukcb = @"ten_dichvukcb";
			 public static string IdKieukham = @"id_kieukham";
			 public static string IdKhoaphong = @"id_khoaphong";
			 public static string IdBacsy = @"id_bacsy";
			 public static string IdDoituongKcb = @"id_doituong_kcb";
			 public static string IdPhongkham = @"id_phongkham";
			 public static string DonGia = @"don_gia";
			 public static string PhuthuDungtuyen = @"phuthu_dungtuyen";
			 public static string PhuthuTraituyen = @"phuthu_traituyen";
			 public static string DongiaNgoaigio = @"dongia_ngoaigio";
			 public static string PhuthuNgoaigio = @"phuthu_ngoaigio";
			 public static string MaDoituongKcb = @"ma_doituong_kcb";
			 public static string IdPhikemtheo = @"id_phikemtheo";
			 public static string IdPhikemtheongoaigio = @"id_phikemtheongoaigio";
			 public static string NhomBaocao = @"nhom_baocao";
			 public static string TuTuc = @"tu_tuc";
			 public static string SttHthi = @"stt_hthi";
			 public static string MotaThem = @"mota_them";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
