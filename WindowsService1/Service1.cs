﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string logDirectory = @"C:\Logs";
            string logFilePath = Path.Combine(logDirectory, "MyServiceLog.txt");


            // Check if the directory exists, if not, create it
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }


            // Append the log with the timestamp
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Service Started\n";
            File.AppendAllText(logFilePath, logMessage);
        }

        protected override void OnStop()
        {
            string logDirectory = @"C:\Logs";
            string logFilePath = Path.Combine(logDirectory, "MyServiceLog.txt");


            // Ensure the directory exists before writing to the log
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }


            // Append the log with the timestamp
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Service Stopped\n";
            File.AppendAllText(logFilePath, logMessage);
        }
    }
}
