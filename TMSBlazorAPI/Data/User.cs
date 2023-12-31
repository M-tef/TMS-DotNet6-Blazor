﻿using System;
using System.Collections.Generic;

namespace TMSBlazorAPI.Data;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime? MembershipStartDate { get; set; }

    public string? MembershipStatus { get; set; }

    public virtual ICollection<Club> Clubs { get; set; } = new List<Club>();
}
