using System.Diagnostics;

namespace Kamen.Editor
{
    public partial class KamenEditorTool
    {
#if UNITY_EDITOR
        public static void RunBat(string batFilePath)
        {
            // 创建一个新的进程
            Process process = new Process();
            // 配置进程
            process.StartInfo.FileName = batFilePath;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.UseShellExecute = true;
            // 启动进程
            process.Start();
        }
        public static string FormatPath(string path)
        {
            return path.Replace("/", "\\");
        }
#endif
    }
}