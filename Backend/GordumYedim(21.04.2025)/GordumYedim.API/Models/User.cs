using System;
using System.Collections.Generic;

namespace GordumYedim.API.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? UserCity { get; set; }

    public string? Email { get; set; }

    public string? UserCommentId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
