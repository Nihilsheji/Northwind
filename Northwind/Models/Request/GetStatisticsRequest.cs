using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Request
{
    public class GetStatisticsRequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public StatisticsSpan Span { get; set; }
    }
}
