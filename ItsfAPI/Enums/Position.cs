using System.ComponentModel;

namespace ItsfAPI.Enums;

public enum Position
{
    [Description("Attacker")]
    ATTACKER = 0,

    [Description("Defender")]
    DEFENDER = 1,
}