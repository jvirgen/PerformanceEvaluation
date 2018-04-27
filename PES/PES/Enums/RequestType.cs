using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Enums
{
    public enum RequestType
    {
        Normal = 0,
        IsUnpaid = 1,
        Emergency = 2,
        Pregnancy = 3
    }
}