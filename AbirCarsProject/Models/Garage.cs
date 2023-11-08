using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AbirCarsProject.Models;

public partial class Garage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GarageId { get; set; }

    public string GarageName { get; set; } = null!;

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<GaragePermission> GaragePermissions { get; set; } = new List<GaragePermission>();

    public virtual ICollection<GarageVisit> GarageVisits { get; set; } = new List<GarageVisit>();
}
