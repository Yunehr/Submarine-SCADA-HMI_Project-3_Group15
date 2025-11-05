namespace DeviceMonitor
{
    public class CO2Monitor
    {
        private double ppmPercentage;
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
        
        public void UpdateCO2FromFile(string fp)
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