using Bond;
using System;

namespace Demo.DataTranferObject
{
    [Schema]
    [Serializable]
    public class GetUserInfo_Input
    {
        public long UserId { get; set; }
    }
}
