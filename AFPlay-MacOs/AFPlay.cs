using System;
using System.Diagnostics;
using System.IO;

namespace AFPlay_MacOs
{
    public class AFPlayer
    {
        private const string _afplay = "afplay";
        private const string _afinfo = "afinfo";
        private Process _process;
        private string _lastPath;
        private DateTime _pausedDateTime;
        private DateTime _startDateTime { get; set; }
        public byte Volume { get; set; }

        public AFPlayer()
        {
            Volume = 1;
        }

        public void Play(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File no such path {filePath}");
                return;
            }
            
            _lastPath = $"-v {Volume} \"{filePath}\"";
            
            _process = Process.Start(_afplay, _lastPath);
            _startDateTime = DateTime.Now;
        }

        public void Pause()
        {
            _process.Kill();
            _pausedDateTime = DateTime.Now;
        }

        public void Resume()
        {
            var time = _pausedDateTime - _startDateTime;
            
            _process = Process.Start(_afplay, $"-t {time.Seconds} {_lastPath}");
            _startDateTime = DateTime.Now;
        }

        public void Stop()
        {
            _process?.Kill();
        }

        public AFInfo GetInfo(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File no such path {filePath}");
                return null;
            }
            
            filePath = $"\"{filePath}\"";

            var startInfo = new ProcessStartInfo
            {
                FileName = _afinfo,
                Arguments = filePath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            
            var info = new AFInfo();
            _process = Process.Start(startInfo);
            while (!_process.StandardOutput.EndOfStream)
            {
                var line = _process.StandardOutput.ReadLine();
                info.Parse(line);
            }

            return info;
        }
    }
}