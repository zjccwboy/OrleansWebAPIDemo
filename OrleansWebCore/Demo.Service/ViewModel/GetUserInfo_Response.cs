using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Service.ViewModel
{
    public class GetUserInfo_Response
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string NikeName { get; set; }
        public bool Sex { get; set; }
    }
}
