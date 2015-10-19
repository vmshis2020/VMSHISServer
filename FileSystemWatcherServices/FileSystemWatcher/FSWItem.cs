using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;


namespace VMS.FSW
{
    public class FSWItem
    {
        #region Delegates

        public delegate void OnAction(string sLogText, Color resultColor);

        #endregion

        string filePath = "";
        public bool Result = false;
        public bool isDoing = false;
        public DateTime ngaysinhbarcode = DateTime.Now;
        public FSWItem(string filePath)
        {
            this.filePath = filePath;
        }

        public event OnAction _OnAction;

        public void PhantichFile()
        {
            try
            {
                using (StreamReader _reader = new StreamReader(filePath))
                {
                    
                    _reader.BaseStream.Flush();
                    _reader.Close();
                }
                

            }
            catch (Exception ex)
            {
            }
            finally
            {
                isDoing = false;
            }
        }
    }
}