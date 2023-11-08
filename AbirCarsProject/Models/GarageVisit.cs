using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AbirCarsProject.Models;

public partial class GarageVisit
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int VisitId { get; set; }

    public int? CustomerId { get; set; }

    public int? GarageId { get; set; }

    public DateTime? VisitDate { get; set; }

    public string? ServiceDescription { get; set; }

    public decimal? TotalCost { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Garage? Garage { get; set; }
}
