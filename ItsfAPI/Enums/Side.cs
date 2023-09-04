using System.ComponentModel;

namespace ItsfAPI.Enums;

public enum Side
{
    [Description("Host")]
    HOST = 0,

    [Description("Guest")]
    GUEST = 1
}