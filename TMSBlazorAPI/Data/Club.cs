using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TMSBlazorAPI.Data;

[Table("Club")]
public partial class Club
{
    [Key]
    [Column("ClubID")]
    public int ClubId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string ClubName { get; set; } = null!;

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Clubs")]
    public virtual User? User { get; set; }
}
