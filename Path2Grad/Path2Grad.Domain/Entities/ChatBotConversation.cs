using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path2Grad.Domain.Entities;

[Table("ChatBotConversation")]
public partial class ChatBotConversation
{
    [Key]
    [Column("ConversationID")]
    public int ConversationId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StartTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EndTime { get; set; }

    [Column("StudentID")]
    public int StudentId { get; set; }

    [InverseProperty("Conversation")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    [ForeignKey("StudentId")]
    [InverseProperty("ChatBotConversations")]
    public virtual Student Student { get; set; } = null!;
}
