using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installer
{
	class Installer
	{
		static void OverwriteToSysroot(string fileName)
		{
			string rootPath = Environment.GetEnvironmentVariable("SystemRoot");
			string currentPath = System.Reflection.Assembly.GetEntryAssembly().Location;
			currentPath = currentPath.Substring(0, currentPath.LastIndexOf('\\'));

			string from = currentPath + "\\" + fileName;
			string to = rootPath + "\\" + fileName;

			if (System.IO.File.Exists(to))
				System.IO.File.Delete(to);
			System.IO.File.Copy(from, to);

		}
		static void Main(string[] args)
		{
			Process[] processList = Process.GetProcessesByName("explorer.exe");
			Console.WriteLine("explorer.exe 확인중");
			if (processList.Length > 0)
			{
				processList[0].Kill();
				Console.WriteLine("explorer.exe 종료");
			}
			OverwriteToSysroot("MatchuPicchu.dll");
			OverwriteToSysroot("SharpShell.dll");

			Process.Start("srm.exe", "install MatchuPicchu.dll -codebase");
			Process.Start("explorer.exe");
		}
	}
}