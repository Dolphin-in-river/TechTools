using System;
using System.Collections.Generic;
using Backups;
using BackupsExtra;
using BackupsExtra.CleanAlgorithmFactory;
using BackupsExtra.Logging;

namespace task5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string directory1 = "C:/Users/Иван/Desktop/1.txt";
            string directory2 = "C:/Users/Иван/Desktop/2.txt";
            var listFile = new List<string>
            {
                directory1,
                directory2,
            };

            bool localKeep = true;
            ICreateRestorePoint point = new CreateSplitRestorePoint();
            var consoleLogging = new ConsoleLogging();
            consoleLogging.Configuration = true;
            var extraBackupService = new BackupsExtraService();
            ExtraBackupJob extraBackupJob = extraBackupService.AddExtraBackupJob(listFile, point, "C:/Users/Иван/Desktop/task5/", localKeep, consoleLogging);
            extraBackupJob.Merge = false;
            
            for (int i = 0; i < 5000; i++)
            {
                extraBackupJob.CreateRestorePoint();
                extraBackupJob.CreateRestorePoint();
                extraBackupJob.ExecuteCleanAlgorithm(new AmountCleanAlgorithmFactory(1));
            }

            localKeep = false;
            ExtraBackupJob secondJob = extraBackupService.AddExtraBackupJob(listFile, point,
                "C:/Users/Иван/Desktop/task5/", localKeep, consoleLogging);
            
            for (int i = 0; i < 5000; i++)
            {
                secondJob.CreateRestorePoint();
                secondJob.CreateRestorePoint();
                secondJob.DeleteFileInBackupJob(directory1);
                secondJob.CreateRestorePoint();
            }

            Console.WriteLine(secondJob.SimpleBackupsJob.AmountStorage());
        }
    }
}