using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace VMS.FSW
{
    public class FSWManager
    {
            public bool AutoStopWhenError = false;
            bool ClearItemsAfterFinished = false;
            public delegate void OnQueueChanged(int QueueCount);
            public event OnQueueChanged _OnQueueChanged;
            public delegate void OnAction( string sLogText, Color sActionColor);
            public event OnAction _OnAction;
            public FSWManager()
            {

            }
            bool isSending = true;
            bool IsSuspending = false;
            Queue queue = new Queue();
            public void StartUp()
            {
                BatdauPhantich();
            }
            
            public void AddItems2Queue(FSWItem _newItem)
            {
                try
                {
                    queue.Enqueue(_newItem);
                }
                catch
                {
                }
            }
            
            public void BatdauPhantich()
            {
                try
                {
                    using (BackgroundWorker _BackgroundWorker = new BackgroundWorker())
                    {
                        _BackgroundWorker.WorkerReportsProgress = true;
                        _BackgroundWorker.WorkerSupportsCancellation = true;
                        _BackgroundWorker.DoWork += new DoWorkEventHandler(_BackgroundWorker_DoWork);
                        _BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(_BackgroundWorker_ProgressChanged);
                        _BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BackgroundWorker_RunWorkerCompleted);
                        if (!_BackgroundWorker.IsBusy)
                            _BackgroundWorker.RunWorkerAsync();
                    }
                }
                catch
                {
                }
            }

            void _BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {

            }

            void _BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
            {
                try
                {
                }
                catch
                {
                }
            }

            void _BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
            {
                try
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        _OnQueueChanged(queue.Count);
                        if (queue.Count > 0)
                        {
                            FSWItem item = queue.Dequeue() as FSWItem;
                            item._OnAction += new FSWItem.OnAction(item__OnAction);
                            item.PhantichFile();
                            while (item.isDoing)
                                Application.DoEvents();

                        }
                    }
                }
                catch
                {
                }
            }
            
            void item__OnAction( string sLogText, Color sActionColor)
            {
                _OnAction( sLogText, sActionColor);
            }
           

           
    }
}
