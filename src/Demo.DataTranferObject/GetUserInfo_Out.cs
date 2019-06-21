using Bond;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DataTranferObject
{
    [Schema]
    [Serializable]
    public class GetUserInfo_Out
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string NikeName { get; set; }
        public bool Sex { get; set; }
    }
}
