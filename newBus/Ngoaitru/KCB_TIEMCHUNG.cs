using System;
using System.Data;
using System.Transactions;
using System.Linq;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;

using System.Text;

using SubSonic;
using NLog;

namespace VNS.HIS.BusRule.Classes
{
    public class KCB_TIEMCHUNG
    {
        private NLog.Logger log;
        public KCB_TIEMCHUNG()
        {
            log = LogManager.GetCurrentClassLogger();
        }
       
        public static DataSet KcbTiemchungPhieuhen(long idBenhnhan, string maluotkham)
        {
            return SPs.KcbTiemchungPhieuhen(idBenhnhan, maluotkham).GetDataSet();
        }
       
    }
}
