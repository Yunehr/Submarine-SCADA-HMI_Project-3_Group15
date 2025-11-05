using System.IO;

namespace DeviceMonitor
{
    public class OxygenMonitor
    {
        private double ppmPercentage;
        public double PpmPercentage { get => ppmPercentage; set => ppmPercentage = value; }

        public void UpdateOxygenFromFile(string fp)
        {
            // Reading double value from a file and updating PpmPercentage
            string content = File.ReadAllText(fp);
            if (double.TryParse(content, out double value))
            {
                PpmPercentage = value;
            }
        }
    }
}