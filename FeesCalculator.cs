using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot
{
    public class FeesCalculator
    {
        public int EntranceFee { get; set; }
        public int FirstHourFee { get; set; }
        public int SubsequentHourFee { get; set; }
        public string TimeFormat { get; set; }

        public DateTime EnteredTime { get; set; }
        public DateTime LeftTime { get; set; }

        public FeesCalculator()
        {

        }

        public void Initialize()
        {
            int entranceFee = -1;
            int firstHourFee = -1;
            int subsequentHourFee = -1;

            if (int.TryParse(ConfigurationManager.AppSettings["EntranceFee"], out entranceFee))
                EntranceFee = entranceFee;

            if (int.TryParse(ConfigurationManager.AppSettings["FirstHourFee"], out firstHourFee))
                FirstHourFee = firstHourFee;

            if (int.TryParse(ConfigurationManager.AppSettings["SubsequentHourFee"], out subsequentHourFee))
                SubsequentHourFee = subsequentHourFee;

            TimeFormat = ConfigurationManager.AppSettings["TimeFormat"];
        }

        public int GetParkingFee(string TimeEnteredStr, string TimeLeftStr)
        {
            if (string.IsNullOrEmpty(TimeEnteredStr))
                throw new ArgumentNullException("Invalid Time Entered");

            if (string.IsNullOrEmpty(TimeLeftStr))
                throw new ArgumentNullException("Invalid Time Left");

            int cost = EntranceFee;
            EnteredTime = DateTime.ParseExact(TimeEnteredStr, TimeFormat, CultureInfo.InvariantCulture);
            LeftTime = DateTime.ParseExact(TimeLeftStr, TimeFormat, CultureInfo.InvariantCulture);

            var totalHours = (LeftTime - EnteredTime).TotalHours;
            if (totalHours > 0)
                cost += FirstHourFee;

            if (totalHours >= 2)
            {
                var hourstAfterFirst = totalHours - 1;
                hourstAfterFirst = Math.Ceiling(hourstAfterFirst);
                cost += (int)hourstAfterFirst * SubsequentHourFee;
            }

            return cost;
        }

       
    }
}
