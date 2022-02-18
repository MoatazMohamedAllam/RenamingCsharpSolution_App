using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace ConsoleApp7
{


    class Program
    {
        static void Main(string[] args)
        {
            var baseDir = @"D:\retention\OracleSendMail";
            
            SolutionRenaming.RenameFilesAndDirectories(baseDir, "OracleSendMail", "newName");
        }
    }
}
