using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp7
{
    public class SolutionRenaming
    {
        public static void RenameFilesAndDirectories(string basePath, string oldName, string newName)
        {
            RenameDirectories(basePath, oldName, newName);
            RenameFilesAndInnerFiles(basePath, oldName, newName);
        }

        private static void RenameDirectories(string path, string oldName, string newName)
        {
            var directories = new DirectoryInfo(path).GetDirectories("*", SearchOption.AllDirectories);
            foreach (var dic in directories)
            {
                if (dic.Name.Contains(oldName))
                {
                    var idx = dic.FullName.LastIndexOf('\\');
                    var dicName = dic.FullName.Substring(idx + 1);
                    dicName = dicName.Replace(oldName, newName);
                    var dm = dic.FullName.Substring(0, idx);
                    Directory.Move(dic.FullName, dm + "\\" + dicName);
                }
            }
        }

        private static void RenameFilesAndInnerFiles(string path, string oldName, string newName)
        {
            foreach (var file in new DirectoryInfo(path).GetFiles("*", SearchOption.AllDirectories))
            {
                try
                {
                    //to replace old name with new one in file
                    RenameInFile(file, oldName, newName);
                    //to rename file with new name
                    RenameFile(file, oldName, newName);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("you dont have an access to this file" + file.FullName);
                }
            }
        }

        private static void RenameFile(FileInfo file, string oldName, string newName)
        {
            if (file.Name.Contains(oldName))
            {
                var idx = file.FullName.LastIndexOf('\\');
                var fileName = file.FullName.Substring(idx + 1);
                fileName = fileName.Replace(oldName, newName);
                var newFullName = file.FullName.Substring(0, idx) + "\\" + fileName;
                File.Move(file.FullName, newFullName);
            }
        }

        private static void RenameInFile(FileInfo file, string oldName, string newName)
        {
            string fileText = File.ReadAllText(file.FullName);
            fileText = fileText.Replace(oldName, newName);
            File.WriteAllText(file.FullName, fileText);
        }
    }
}
