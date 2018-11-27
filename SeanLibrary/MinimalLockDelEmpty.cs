﻿using log4net.Appender;
using System.IO;

namespace SeanLibrary
{
    public class MinimalLockDelEmpty : FileAppender.MinimalLock
    {
        public override void ReleaseLock()
        {
            base.ReleaseLock();

            var logFile = new FileInfo(CurrentAppender.File);
            if (logFile.Exists && logFile.Length <= 0)
            {
                logFile.Delete();
            }
        }

    }
}
