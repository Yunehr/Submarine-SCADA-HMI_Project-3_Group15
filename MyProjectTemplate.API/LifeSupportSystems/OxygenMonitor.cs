using System.IO;
using FileHandler;

namespace DeviceMonitor
{
    public class OxygenMonitor
    {
        private double ppmPercentage;
        private int currentDataLine = 0;
        public double PpmPercentage
        {
            get => ppmPercentage;
            set
            {
                if (value < 0)
                {
                    ppmPercentage = 0.0;
                }
                else if (value > 100)
                {
                    ppmPercentage = 100.0;
                }
                else
                {
                    ppmPercentage = value;
                }
            }
        }
        public int CurrentDataLine
        {
            get => currentDataLine;
            set
            {
                if (value < 0)
                {
                    currentDataLine = 0;
                }
                else
                {
                    currentDataLine = value;
                }
            }
        }

        public void UpdateOxygenFromFile(string fp)
        {
            // Reading double value from a file and updating PpmPercentage
            string content = File.ReadAllText(fp);
            if (double.TryParse(content, out double value))
            {
                PpmPercentage = value;
            }
        }

        public void TakeOxygenReading(string fp)
        {
            var fr = new FileHandler.FileReader();
            PpmPercentage = fr.ReadDoubleFromNextLine(fp, CurrentDataLine);
            CurrentDataLine++;
        }
    }
}