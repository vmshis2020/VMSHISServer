using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace VMS.FSW
{
    public partial class FSWService : ServiceBase
    {
       
        public FSWService()
        {
            InitializeComponent();
            
        }
       
        protected override void OnStart(string[] args)
        {
            try
            {
                
              

            }
            catch (Exception ex)
            {
                AppLogger.LogAction.LogSCPService(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "-->OnStart.Exception-->" + ex.Message);
            }
        }

        protected override void OnStop()
        {
            try
            {
              
            }
            catch (Exception ex)
            {
            }
        }

       
       
    }
}


