using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Path2Grad.Models;

[Table("ProjectsBankProjectField")]
[Index("ProjectBankId", Name = "IX_ProjectsBankProjectField_ProjectBankID")]
public partial class ProjectsBankProjectField
{
    [Key]
    [Column("ProjectFieldID")]
    public int ProjectFieldId { get; set; }

    [StringLength(255)]
    public string ProjectField { get; set; } = null!;

    [Column("ProjectBankID")]
    public int ProjectBankId { get; set; }

    [ForeignKey("ProjectBankId")]
    [InverseProperty("ProjectsBankProjectFields")]
    public virtual ProjectsBank ProjectBank { get; set; } = null!;
}
