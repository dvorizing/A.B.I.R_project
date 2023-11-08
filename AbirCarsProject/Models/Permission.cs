using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AbirCarsProject.Models;

public partial class Permission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PermissionId { get; set; }

    public int? UserId { get; set; }

    public int? CustomerId { get; set; }

    public bool? CanView { get; set; }

    public bool? CanEdit { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual User? User { get; set; }
}
