using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AbirCarsProject.Models;

public partial class GaragePermission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PermissionId { get; set; }

    public int? UserId { get; set; }

    public int? GarageId { get; set; }

    public bool? CanView { get; set; }

    public bool? CanEdit { get; set; }

    public virtual Garage? Garage { get; set; }

    public virtual User? User { get; set; }
}
