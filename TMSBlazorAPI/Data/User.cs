using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TMSBlazorAPI.Data;

[Table("User")]
public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(25)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(25)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? MembershipStartDate { get; set; }

    [Column("password")]
    [Unicode(false)]
    public string? Password { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? MembershipStatus { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Club> Clubs { get; set; } = new List<Club>();
}
