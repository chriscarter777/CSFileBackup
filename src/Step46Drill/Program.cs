using System;
using System.Collections.Generic;
using System.IO;


namespace Step46Drill
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //what time is it
            DateTime now = DateTime.Now;
            DateTime since = now.AddDays(-1);

            //set paths
            string desktop = @"C:\Users\Chris\Desktop\";
            string workFolder = desktop + @"Working\";
            string backupFolder = desktop + @"Backup\";
            string logFile = desktop + @"Backup\ log.txt";
            DirectoryInfo workDir = new DirectoryInfo(workFolder);

            //generate a list of recently modified or created files
            List<FileInfo> filesToBackup = BuildBackupList(workDir, since);

            //copy each new file to the backup directory
            foreach (FileInfo fileToBackup in filesToBackup)
            {
                CopyFile(fileToBackup, backupFolder);
            }
        //end Main method
        }

        public static List<FileInfo> BuildBackupList(DirectoryInfo dir, DateTime since)
        {
            Console.WriteLine("Building list of files to backup since " + since + ":");
            List<FileInfo> fileList = new List<FileInfo>();
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo f in files)
            {
                Console.WriteLine(f.Name + " created: " + f.CreationTime + "\tlast written: " + f.LastWriteTime + " compared with " + since);
                if (f.CreationTime <= since || f.LastWriteTime <= since)
                {
                    Console.WriteLine(" Added ");
                    fileList.Add(f);
                }
                if (true)
                {
                    Console.WriteLine(" Added anyway.");
                    fileList.Add(f);
                }
            }
            return fileList;
        }

        public static void CopyFile (FileInfo fileToBackup, string backupFolder)
        {
            string destination = backupFolder + fileToBackup.Name;
            fileToBackup.CopyTo(destination, true);
            Console.WriteLine("Copying " + fileToBackup.Name + " to " + destination);
        }
    //end Program class
    }
}
