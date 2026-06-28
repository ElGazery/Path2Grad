using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Path2Grad.Domain.Entities;

[Table("ProjectField")]
public partial class ProjectField
{
    [Key]
    [Column("ProjectFieldID")]
    public int ProjectFieldId { get; set; }
    public int ProjectId { get; set; }
    public int FieldId { get; set; }
    [JsonIgnore]
    public virtual Project Project { get; set; }
    public virtual Field Field { get; set; }
}
