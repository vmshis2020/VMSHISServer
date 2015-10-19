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
namespace VNS.HIS.DAL{
    /// <summary>
    /// Strongly-typed collection for the VDanhmucbenh class.
    /// </summary>
    [Serializable]
    public partial class VDanhmucbenhCollection : ReadOnlyList<VDanhmucbenh, VDanhmucbenhCollection>
    {        
        public VDanhmucbenhCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the v_danhmucbenh view.
    /// </summary>
    [Serializable]
    public partial class VDanhmucbenh : ReadOnlyRecord<VDanhmucbenh>, IReadOnlyRecord
    {
    
	    #region Default Settings
	    protected static void SetSQLProps() 
	    {
		    GetTableSchema();
	    }
	    #endregion
        #region Schema Accessor
	    public static TableSchema.Table Schema
        {
            get
            {
                if (BaseSchema == null)
                {
                    SetSQLProps();
                }
                return BaseSchema;
            }
        }
    	
        private static void GetTableSchema() 
        {
            if(!IsSchemaInitialized)
            {
                //Schema declaration
                TableSchema.Table schema = new TableSchema.Table("v_danhmucbenh", TableType.View, DataService.GetInstance("ORM"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarIdBenh = new TableSchema.TableColumn(schema);
                colvarIdBenh.ColumnName = "id_benh";
                colvarIdBenh.DataType = DbType.Int16;
                colvarIdBenh.MaxLength = 0;
                colvarIdBenh.AutoIncrement = false;
                colvarIdBenh.IsNullable = false;
                colvarIdBenh.IsPrimaryKey = false;
                colvarIdBenh.IsForeignKey = false;
                colvarIdBenh.IsReadOnly = false;
                
                schema.Columns.Add(colvarIdBenh);
                
                TableSchema.TableColumn colvarMaBenh = new TableSchema.TableColumn(schema);
                colvarMaBenh.ColumnName = "ma_benh";
                colvarMaBenh.DataType = DbType.AnsiString;
                colvarMaBenh.MaxLength = 20;
                colvarMaBenh.AutoIncrement = false;
                colvarMaBenh.IsNullable = false;
                colvarMaBenh.IsPrimaryKey = false;
                colvarMaBenh.IsForeignKey = false;
                colvarMaBenh.IsReadOnly = false;
                
                schema.Columns.Add(colvarMaBenh);
                
                TableSchema.TableColumn colvarMotaThem = new TableSchema.TableColumn(schema);
                colvarMotaThem.ColumnName = "mota_them";
                colvarMotaThem.DataType = DbType.String;
                colvarMotaThem.MaxLength = 255;
                colvarMotaThem.AutoIncrement = false;
                colvarMotaThem.IsNullable = true;
                colvarMotaThem.IsPrimaryKey = false;
                colvarMotaThem.IsForeignKey = false;
                colvarMotaThem.IsReadOnly = false;
                
                schema.Columns.Add(colvarMotaThem);
                
                TableSchema.TableColumn colvarTenBenh = new TableSchema.TableColumn(schema);
                colvarTenBenh.ColumnName = "ten_benh";
                colvarTenBenh.DataType = DbType.String;
                colvarTenBenh.MaxLength = 300;
                colvarTenBenh.AutoIncrement = false;
                colvarTenBenh.IsNullable = false;
                colvarTenBenh.IsPrimaryKey = false;
                colvarTenBenh.IsForeignKey = false;
                colvarTenBenh.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenBenh);
                
                TableSchema.TableColumn colvarViettat = new TableSchema.TableColumn(schema);
                colvarViettat.ColumnName = "viettat";
                colvarViettat.DataType = DbType.String;
                colvarViettat.MaxLength = 50;
                colvarViettat.AutoIncrement = false;
                colvarViettat.IsNullable = true;
                colvarViettat.IsPrimaryKey = false;
                colvarViettat.IsForeignKey = false;
                colvarViettat.IsReadOnly = false;
                
                schema.Columns.Add(colvarViettat);
                
                TableSchema.TableColumn colvarTenLoaibenh = new TableSchema.TableColumn(schema);
                colvarTenLoaibenh.ColumnName = "ten_loaibenh";
                colvarTenLoaibenh.DataType = DbType.String;
                colvarTenLoaibenh.MaxLength = 255;
                colvarTenLoaibenh.AutoIncrement = false;
                colvarTenLoaibenh.IsNullable = false;
                colvarTenLoaibenh.IsPrimaryKey = false;
                colvarTenLoaibenh.IsForeignKey = false;
                colvarTenLoaibenh.IsReadOnly = false;
                
                schema.Columns.Add(colvarTenLoaibenh);
                
                TableSchema.TableColumn colvarMaLoaibenh = new TableSchema.TableColumn(schema);
                colvarMaLoaibenh.ColumnName = "ma_loaibenh";
                colvarMaLoaibenh.DataType = DbType.String;
                colvarMaLoaibenh.MaxLength = 20;
                colvarMaLoaibenh.AutoIncrement = false;
                colvarMaLoaibenh.IsNullable = false;
                colvarMaLoaibenh.IsPrimaryKey = false;
                colvarMaLoaibenh.IsForeignKey = false;
                colvarMaLoaibenh.IsReadOnly = false;
                
                schema.Columns.Add(colvarMaLoaibenh);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("v_danhmucbenh",schema);
            }
        }
        #endregion
        
        #region Query Accessor
	    public static Query CreateQuery()
	    {
		    return new Query(Schema);
	    }
	    #endregion
	    
	    #region .ctors
	    public VDanhmucbenh()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public VDanhmucbenh(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public VDanhmucbenh(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public VDanhmucbenh(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("IdBenh")]
        [Bindable(true)]
        public short IdBenh 
	    {
		    get
		    {
			    return GetColumnValue<short>("id_benh");
		    }
            set 
		    {
			    SetColumnValue("id_benh", value);
            }
        }
	      
        [XmlAttribute("MaBenh")]
        [Bindable(true)]
        public string MaBenh 
	    {
		    get
		    {
			    return GetColumnValue<string>("ma_benh");
		    }
            set 
		    {
			    SetColumnValue("ma_benh", value);
            }
        }
	      
        [XmlAttribute("MotaThem")]
        [Bindable(true)]
        public string MotaThem 
	    {
		    get
		    {
			    return GetColumnValue<string>("mota_them");
		    }
            set 
		    {
			    SetColumnValue("mota_them", value);
            }
        }
	      
        [XmlAttribute("TenBenh")]
        [Bindable(true)]
        public string TenBenh 
	    {
		    get
		    {
			    return GetColumnValue<string>("ten_benh");
		    }
            set 
		    {
			    SetColumnValue("ten_benh", value);
            }
        }
	      
        [XmlAttribute("Viettat")]
        [Bindable(true)]
        public string Viettat 
	    {
		    get
		    {
			    return GetColumnValue<string>("viettat");
		    }
            set 
		    {
			    SetColumnValue("viettat", value);
            }
        }
	      
        [XmlAttribute("TenLoaibenh")]
        [Bindable(true)]
        public string TenLoaibenh 
	    {
		    get
		    {
			    return GetColumnValue<string>("ten_loaibenh");
		    }
            set 
		    {
			    SetColumnValue("ten_loaibenh", value);
            }
        }
	      
        [XmlAttribute("MaLoaibenh")]
        [Bindable(true)]
        public string MaLoaibenh 
	    {
		    get
		    {
			    return GetColumnValue<string>("ma_loaibenh");
		    }
            set 
		    {
			    SetColumnValue("ma_loaibenh", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string IdBenh = @"id_benh";
            
            public static string MaBenh = @"ma_benh";
            
            public static string MotaThem = @"mota_them";
            
            public static string TenBenh = @"ten_benh";
            
            public static string Viettat = @"viettat";
            
            public static string TenLoaibenh = @"ten_loaibenh";
            
            public static string MaLoaibenh = @"ma_loaibenh";
            
	    }
	    #endregion
	    
	    
	    #region IAbstractRecord Members
        public new CT GetColumnValue<CT>(string columnName) {
            return base.GetColumnValue<CT>(columnName);
        }
        public object GetColumnValue(string columnName) {
            return base.GetColumnValue<object>(columnName);
        }
        #endregion
	    
    }
}