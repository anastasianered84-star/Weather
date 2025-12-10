using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Models
{
    public class DataResponse
    {
        public List<Forecast> forecasts {  get; set; }
    }

    public class Forecast
    {
       public DateTime date {  get; set; }
        public List<HourOfDay>
    }

}
