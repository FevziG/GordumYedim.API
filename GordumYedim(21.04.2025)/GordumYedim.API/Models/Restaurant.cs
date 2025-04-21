using System;
using System.Collections.Generic;

namespace GordumYedim.API.Models;

public partial class Restaurant
{
    public int ResId { get; set; }

    public string? ResName { get; set; }

    public string? ResAddress { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? PlaceId { get; set; }

    public DateTime? CreatedTime { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
