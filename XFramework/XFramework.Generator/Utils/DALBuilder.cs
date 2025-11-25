using System.Diagnostics;

namespace XFramework.Generator.Utils
{
    public static class DALBuilder
    {
        public static void Build(string dalCsprojFullPath)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"build \"{dalCsprojFullPath}\" -c Debug --nologo",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = psi };
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Console.WriteLine(error);
                throw new Exception($"XFramework.DAL build başarısız oldu.");
            }

        }
    }
}
