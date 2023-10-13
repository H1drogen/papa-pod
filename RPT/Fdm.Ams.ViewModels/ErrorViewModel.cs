using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fdm.Ams.ViewModels
{
    public class ErrorViewModel
    {
        public string ErrorMessage { get; set; }
        public HttpStatusCode ErrorCode { get; set; }
        public string ErrorDetails { get; set; }
        public DateTime DateTime { get; set; }

    }
}
