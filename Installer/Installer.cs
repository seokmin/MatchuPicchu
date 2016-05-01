using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Installer
{
	class Installer
	{
		static void OverwriteFile(string fileName)
		{
			string rootPath = Environment.SystemDirectory;
			rootPath = rootPath.Substring(0, rootPath.IndexOf('\\'));
			string currentPath = System.Reflection.Assembly.GetEntryAssembly().Location;
			currentPath = currentPath.Substring(0, currentPath.LastIndexOf('\\'));

			string from = currentPath + "\\" + fileName;
			string to = rootPath + "\\MatchuPicchu\\" + fileName;

			if (File.Exists(to))
				File.Delete(to);
			File.Copy(from, to);

		}
		static void Main(string[] args)
		{	
			string rootPath = Environment.SystemDirectory;
			rootPath = rootPath.Substring(0, rootPath.IndexOf('\\'));
			Console.WriteLine("루트 디렉토리 확인 - " + rootPath);
			Process[] processList = Process.GetProcessesByName("explorer");
			Console.WriteLine("explorer.exe 확인중");
			if (processList.Length > 0)
			{
				processList[0].Kill();
				Console.WriteLine("explorer.exe 종료");
			}

			string sDirPath;  //Dir value to save  
			sDirPath = rootPath + "\\MatchuPicchu";  //Make Dir Uri from Appliction Startup Path and Dir name  
			DirectoryInfo di = new DirectoryInfo(sDirPath);  //Create Directoryinfo value by sDirPath  
			if (!di.Exists)
			di.Create();

			OverwriteFile("MatchuPicchu.dll");
			OverwriteFile("SharpShell.dll");

			Console.WriteLine("MatchuPicchu.dll 등록");
			Process.Start("srm.exe", "install " + rootPath + "\\MatchuPicchu\\MatchuPicchu.dll -codebase");

			Console.WriteLine("explorer.exe 재실행");
			Process.Start("explorer.exe");
			MessageBox.Show("설치 완료");
		}
	}
}