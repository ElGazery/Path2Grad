using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path2Grad.Domain.Entities;

public partial class Message
{
    [Key]
    [Column("MessageID")]
    public int MessageId { get; set; }

    [StringLength(100)]
    public string Sender { get; set; } = null!;

    public string MessageText { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime MessageTime { get; set; }

    [Column("ConversationID")]
    public int ConversationId { get; set; }

    [ForeignKey("ConversationId")]
    [InverseProperty("Messages")]
    public virtual ChatBotConversation Conversation { get; set; } = null!;
}
