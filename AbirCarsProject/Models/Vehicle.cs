using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AbirCarsProject.Models;

public partial class Vehicle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int VehicleId { get; set; }

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int? Year { get; set; }

    public string? Vin { get; set; }

    public int? Mileage { get; set; }

    public DateTime? LastServiceDate { get; set; }

    public int? OwnerId { get; set; }

    public virtual Customer? Owner { get; set; }
}
