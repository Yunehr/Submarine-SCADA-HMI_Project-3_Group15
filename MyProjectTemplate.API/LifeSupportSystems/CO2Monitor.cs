using System.IO;
using FileHandler;

namespace DeviceMonitor
{
    public class CO2Monitor
    {
        private double ppmPercentage;
        private int currentDataLine = 0;
        public double PpmPercentage
        {
            get => ppmPercentage;
            set => ppmPercentage = value < 0 ? 0.0 : value > 100 ? 100.0 : value;
            // get => ppmPercentage;
            // set
            // {
            //     if (value < 0)
            //     {
            //         ppmPercentage = 0.0;
            //     }
            //     else if (value > 100)
            //     {
            //         ppmPercentage = 100.0;
            //     }
            //     else
            //     {
            //         ppmPercentage = value;
            //     }
            // }
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
        public void ResetDataLine()
        {
            CurrentDataLine = 0;
        }


        public void UpdateCO2FromFile(string fp)
        {
            // Reading double value from a file and updating PpmPercentage
            string content = File.ReadAllText(fp);
            if (double.TryParse(content, out double value))
            {
                PpmPercentage = value;
            }
        }
        public void TakeCO2Reading(string fp)
        {
            var fr = new FileReader();
            PpmPercentage = fr.ReadDoubleFromNextLine(fp, CurrentDataLine);
            CurrentDataLine++;
        }

        public void LogCo2Reading(string fp)
        {
            var fw = new FileWriter();
            fw.AppendDoubleToFile(fp, PpmPercentage);
        }
    }
}