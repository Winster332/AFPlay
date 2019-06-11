using System.Linq;

namespace AFPlay_MacOs
{
 public class AFInfo
    {
        public string File { get; set; }
        public string FileTypeId { get; set; }
        public int TracksCount { get; set; }
        public double EstimatedDuration { get; set; }
        public int AudioBytes { get; set; }
        public int AudioPackets { get; set; }
        public int BitRate { get; set; }
        public int PacketSizeUpperBound { get; set; }
        public int MaximumPacketSize { get; set; }
        public int AudioDataFileOffset { get; set; }

        internal void Parse(string source)
        {
            var segments = source.Split(':');

            if (segments.Length == 2)
            {
                var key = segments.FirstOrDefault();
                var value = ClipValue(segments.LastOrDefault());

                switch (key)
                {
                    case "File": File = value; break;
                    case "Num Tracks": TracksCount = int.Parse(value); break;
                    case "File type ID": FileTypeId = value; break;
                    case "estimated duration": EstimatedDuration = ParseDouble(value); break;
                    case "audio bytes": AudioBytes = int.Parse(value); break;
                    case "audio packets": AudioPackets = int.Parse(value); break;
                    case "bit rate": BitRate = int.Parse(value.Split(' ').FirstOrDefault()); break;
                    case "packet size upper bound": PacketSizeUpperBound = int.Parse(value); break;
                    case "audio data file offset": AudioDataFileOffset = int.Parse(value); break;
                    case "maximum packet size": MaximumPacketSize = int.Parse(value); break;
                }
            }
        }

        private double ParseDouble(string value)
        {
            if (double.TryParse(value.Replace("sec", "").Replace(" ", ""), out var result))
            {
                return result;
            }

            return 0;
        }

        private string ClipValue(string value)
        {
            for (var i = 0; i < value.Length; i++)
            {
                var symbol = value[i];

                if (symbol != ' ')
                {
                    value = value.Substring(i, value.Length - i);
                    break;
                }
            }

            return value;
        }
    }
}