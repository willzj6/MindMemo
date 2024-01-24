using System;
using System.Collections.Generic;

namespace MindMemo.Models;

public partial class NumberMemoryScore
{
    public string Username { get; set; } = null!;

    public int? Score { get; set; }

    public DateTime Time { get; set; }
}
