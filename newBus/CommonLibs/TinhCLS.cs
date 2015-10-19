﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;

namespace VNS.Libs
{
   public class TinhCLS
    {
       public static void GB_TinhPhtramBHYT(KcbChidinhclsChitiet objChidinhChitiet, KcbLichsuDoituongKcb objLichsu, bool noitru, decimal PTramBHYT)
       {
           byte TrangthaiBhyt = 1;
           decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
           bool b_ExistPtramBHYT = false;
           if (!THU_VIEN_CHUNG.IsBaoHiem(objLichsu.IdLoaidoituongKcb.Value))//Đối tượng DV
           {
               TrangthaiBhyt = (byte)0;
               objChidinhChitiet.TuTuc = 0;
           }
           else
               TrangthaiBhyt = (byte)(globalVariables.gv_blnApdungChedoDuyetBHYT ? 0 : 1);
           if (Utility.Int32Dbnull(objChidinhChitiet.TrangthaiHuy, -1) == -1) objChidinhChitiet.TrangthaiHuy = 0;
           DmucDichvuclsChitiet obServiceDetail =
               DmucDichvuclsChitiet.FetchByID(Utility.Int32Dbnull(objChidinhChitiet.IdChitietdichvu));
           if (obServiceDetail != null)
           {
               objChidinhChitiet.GiaDanhmuc = Utility.DecimaltoDbnull(obServiceDetail.DonGia);
           }
           objChidinhChitiet.PtramBhyt = PTramBHYT;
           objChidinhChitiet.PtramBhytGoc = objLichsu.PtramBhytGoc;
           objChidinhChitiet.LoaiChietkhau = 0;
           objChidinhChitiet.TrangthaiBhyt = TrangthaiBhyt;
           objChidinhChitiet.IdLoaichidinh = 0;//Chưa hiểu trường này-->Cần xem lại
           if (Utility.Int32Dbnull(objChidinhChitiet.TuTuc, 0) == 1)
           {
               objChidinhChitiet.BhytChitra = 0;
               objChidinhChitiet.BnhanChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0);
               objChidinhChitiet.PtramBhyt = 0;
           }
           else
           {
               //Mục tính BHYT đặc biệt để dành sử dụng trong tương lai
               PtramBHYTDacBiet(objChidinhChitiet, objLichsu, 2, ref b_ExistPtramBHYT);
               if (b_ExistPtramBHYT)
               {
                   objChidinhChitiet.BhytChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia) *
                                          Utility.DecimaltoDbnull(objChidinhChitiet.PtramBhyt) / 100;
                   objChidinhChitiet.BnhanChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) -
                                         Utility.DecimaltoDbnull(objChidinhChitiet.BhytChitra);
               }
               else//99% rơi vào nhánh này
               {

                   PTramBHYT = Utility.DecimaltoDbnull(objLichsu.PtramBhyt);
                   decimal BHCT = 0m;
                   if (objLichsu.DungTuyen == 1)//BHYT đúng tuyến rơi vào nhánh này dù nội trú ngay ngoại trú
                   {
                       BHCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) * (Utility.DecimaltoDbnull(objLichsu.PtramBhyt, 0) / 100);
                   }
                   else//DV và BHYT trái tuyến
                   {
                       if (objLichsu.TrangthaiNoitru <= 0 || !noitru)//Đối tượng ngoại trú hoặc Chỉ định ngoại trú-->Lấy phần trăm ngoại trú
                           BHCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) * (Utility.DecimaltoDbnull(objLichsu.PtramBhyt, 0) / 100);
                       else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                           BHCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia.Value, 0) * (Utility.DecimaltoDbnull(objLichsu.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                   }
                   decimal BNCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) - BHCT;
                   objChidinhChitiet.BhytChitra = BHCT;
                   objChidinhChitiet.BnhanChitra = BNCT;
               }

           }
       }
       /// <summary>
       /// hàm thực hienj việc tính phâm trăm bảo hiểm
       /// </summary>
       /// <param name="objChidinhChitiet"></param>
       /// <param name="PTramBHYT"></param>
       public static void GB_TinhPhtramBHYT(KcbChidinhclsChitiet objChidinhChitiet, KcbLuotkham objLuotkham,bool noitru, decimal PTramBHYT)
       {
           byte TrangthaiBhyt = 1;
           decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
           bool b_ExistPtramBHYT = false;
           if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))//Đối tượng DV
           {
               TrangthaiBhyt = (byte)0;
               objChidinhChitiet.TuTuc = 0;
           }
           else
               TrangthaiBhyt = (byte)(globalVariables.gv_blnApdungChedoDuyetBHYT ? 0 : 1);
           if (Utility.Int32Dbnull(objChidinhChitiet.TrangthaiHuy, -1) == -1) objChidinhChitiet.TrangthaiHuy = 0;
           DmucDichvuclsChitiet obServiceDetail =
               DmucDichvuclsChitiet.FetchByID(Utility.Int32Dbnull(objChidinhChitiet.IdChitietdichvu));
           if (obServiceDetail != null)
           {
               objChidinhChitiet.GiaDanhmuc = Utility.DecimaltoDbnull(obServiceDetail.DonGia);
           }
           objChidinhChitiet.PtramBhyt = PTramBHYT;
           objChidinhChitiet.PtramBhytGoc = objLuotkham.PtramBhytGoc;
           objChidinhChitiet.LoaiChietkhau = 0;
           objChidinhChitiet.TrangthaiBhyt = TrangthaiBhyt;
           objChidinhChitiet.IdLoaichidinh = 0;//Chưa hiểu trường này-->Cần xem lại
           if (Utility.Int32Dbnull(objChidinhChitiet.TuTuc, 0) == 1)
           {
               objChidinhChitiet.BhytChitra = 0;
               objChidinhChitiet.BnhanChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0);
               objChidinhChitiet.PtramBhyt = 0;
           }
           else
           {
               //Mục tính BHYT đặc biệt để dành sử dụng trong tương lai
               PtramBHYTDacBiet(objChidinhChitiet, objLuotkham, 2, ref b_ExistPtramBHYT);
               if (b_ExistPtramBHYT)
               {
                   objChidinhChitiet.BhytChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia) *
                                          Utility.DecimaltoDbnull(objChidinhChitiet.PtramBhyt) / 100;
                   objChidinhChitiet.BnhanChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) -
                                         Utility.DecimaltoDbnull(objChidinhChitiet.BhytChitra);
               }
               else//99% rơi vào nhánh này
               {
                   
                   PTramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt);
                   decimal BHCT = 0m;
                   if (objLuotkham.DungTuyen == 1)//BHYT đúng tuyến rơi vào nhánh này dù nội trú ngay ngoại trú
                   {
                       BHCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                   }
                   else//DV và BHYT trái tuyến
                   {
                       if (objLuotkham.TrangthaiNoitru <= 0 || !noitru)//Đối tượng ngoại trú hoặc Chỉ định ngoại trú-->Lấy phần trăm ngoại trú
                           BHCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                       else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                           BHCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia.Value, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                   }
                   decimal BNCT =Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) -BHCT;
                   objChidinhChitiet.BhytChitra =BHCT;
                   objChidinhChitiet.BnhanChitra = BNCT;
               }
               
           }
       }
       public static void PtramBHYTDacBiet(KcbChidinhclsChitiet objChidinhChitiet, KcbLichsuDoituongKcb objLichsu, int PaymentType_ID, ref bool b_Exist)
       {
           b_Exist = false;
           string IsDungTuyen = "DT";
           DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(objLichsu.IdDoituongKcb);
           if (objectType != null)
           {
               switch (objectType.MaDoituongKcb)
               {
                   case "BHYT":
                       if (Utility.Int32Dbnull(objLichsu.DungTuyen, "0") == 1) IsDungTuyen = "DT";
                       else
                       {
                           IsDungTuyen = "TT";
                       }
                       break;
                   default:
                       IsDungTuyen = "KHAC";
                       break;
               }

           }
           SqlQuery sqlQuery = new Select().From(DmucBhytChitraDacbiet.Schema)
               .Where(DmucBhytChitraDacbiet.Columns.IdDichvuChitiet).IsEqualTo(objChidinhChitiet.IdChitietchidinh)
               .And(DmucBhytChitraDacbiet.Columns.MaLoaithanhtoan).IsEqualTo(PaymentType_ID)
               .And(DmucBhytChitraDacbiet.Columns.DungtuyenTraituyen).IsEqualTo(IsDungTuyen)
               .And(DmucBhytChitraDacbiet.Columns.MaDoituongKcb).IsEqualTo(objLichsu.MaDoituongKcb);
           DmucBhytChitraDacbiet objDetailPtramBhyt = sqlQuery.ExecuteSingle<DmucBhytChitraDacbiet>();
           if (objDetailPtramBhyt != null)
           {
               objChidinhChitiet.IdLoaichidinh = 1;
               objChidinhChitiet.BhytChitra = Gia_BHYT(objDetailPtramBhyt.TileGiam, Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0));
               objChidinhChitiet.BnhanChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) -
                                         objChidinhChitiet.BhytChitra;
               objChidinhChitiet.PtramBhyt = Utility.DecimaltoDbnull(objDetailPtramBhyt.TileGiam, 0);
               objChidinhChitiet.LoaiChietkhau = 1;
               b_Exist = true;
               // objChidinhChitiet.DonGia =
           }
       }
       /// <summary>
       /// hàm thực hiện viec tính toán giá đặc biệt cho bệnh nhân
       /// </summary>
       /// <param name="objChidinhChitiet"></param>
       /// <param name="objLuotkham"></param>
       /// <param name="PaymentType_ID"></param>
       public static void PtramBHYTDacBiet(KcbChidinhclsChitiet objChidinhChitiet,KcbLuotkham objLuotkham,int PaymentType_ID,ref bool b_Exist)
       {
           b_Exist = false;
           string IsDungTuyen = "DT";
           DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(objLuotkham.IdDoituongKcb);
           if (objectType != null)
           {
               switch (objectType.MaDoituongKcb)
               {
                   case "BHYT":
                       if (Utility.Int32Dbnull(objLuotkham.DungTuyen, "0") == 1) IsDungTuyen = "DT";
                       else
                       {
                           IsDungTuyen = "TT";
                       }
                       break;
                   default:
                       IsDungTuyen = "KHAC";
                       break;
               }

           }
           SqlQuery sqlQuery = new Select().From(DmucBhytChitraDacbiet.Schema)
               .Where(DmucBhytChitraDacbiet.Columns.IdDichvuChitiet).IsEqualTo(objChidinhChitiet.IdChitietchidinh)
               .And(DmucBhytChitraDacbiet.Columns.MaLoaithanhtoan).IsEqualTo(PaymentType_ID)
               .And(DmucBhytChitraDacbiet.Columns.DungtuyenTraituyen).IsEqualTo(IsDungTuyen)
               .And(DmucBhytChitraDacbiet.Columns.MaDoituongKcb).IsEqualTo(objLuotkham.MaDoituongKcb);
           DmucBhytChitraDacbiet objDetailPtramBhyt = sqlQuery.ExecuteSingle<DmucBhytChitraDacbiet>();
           if(objDetailPtramBhyt!=null)
           {
               objChidinhChitiet.IdLoaichidinh = 1;
               objChidinhChitiet.BhytChitra = Gia_BHYT(objDetailPtramBhyt.TileGiam,Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0));
               objChidinhChitiet.BnhanChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) -
                                         objChidinhChitiet.BhytChitra;
               objChidinhChitiet.PtramBhyt = Utility.DecimaltoDbnull(objDetailPtramBhyt.TileGiam, 0);
               objChidinhChitiet.LoaiChietkhau = 1;
               b_Exist = true;
               // objChidinhChitiet.DonGia =
           }
       }
       private static decimal Gia_BHYT(decimal PhanTramBHYT, decimal DON_GIA)
       {
           return PhanTramBHYT * DON_GIA / 100;
       }
    }
}
