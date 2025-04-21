using System;
using System.Collections.Generic;

namespace GordumYedim.API.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? CommentUserId { get; set; }

    public int? CommentResId { get; set; }

    public int? Rating { get; set; }

    public string? Comment1 { get; set; }

    public byte[]? Image { get; set; }

    public DateTime? CommentTime { get; set; }

    public virtual Restaurant? CommentRes { get; set; }

    public virtual User? CommentUser { get; set; }


}
