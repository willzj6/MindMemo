using System;
using System.Collections.Generic;

namespace MindMemo.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string? Password { get; set; }
}
