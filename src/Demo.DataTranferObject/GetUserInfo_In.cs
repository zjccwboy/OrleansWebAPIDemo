using Bond;
using System;

namespace Demo.DataTranferObject
{
    [Schema]
    [Serializable]
    public class GetUserInfo_In
    {
        public long UserId { get; set; }
    }
}
