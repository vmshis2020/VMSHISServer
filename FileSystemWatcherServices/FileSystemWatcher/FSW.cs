using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;


namespace VMS.FSW
{
    public class FSW : FileWatcherServer
    {

        private FileSystemWatcher _fileWatcher;
        string WatchedPath = "";
        #region Contructor

        public FSW(string WatchPath)
        {
            this.WatchedPath = WatchedPath;
            _fileWatcher = new FileSystemWatcher(WatchedPath);
            _fileWatcher.Created += _fileWatcher_Created;
            _fileWatcher.Changed += _fileWatcher_Changed;
            _fileWatcher.Renamed += _fileWatcher_Renamed;
            _fileWatcher.Deleted += _fileWatcher_Deleted;
        }

        void _fileWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            
        }

        void _fileWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            
        }

        void _fileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            
        }

        void _fileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            
        }

        #endregion

        
    }
}
