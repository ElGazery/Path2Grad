using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Path2Grad.Models;

[Table("ProjectsBank")]

public partial class ProjectsBank
{
    [Key]
    [Column("ProjectBankID")]
    public int ProjectBankId { get; set; }

    [StringLength(255)]
    public string ProjectName { get; set; } = null!;

    public string? Description { get; set; }

    public string? ProjectSpecification { get; set; }


    [InverseProperty("ProjectBank")]
    public virtual ICollection<ProjectsBankProjectField> ProjectsBankProjectFields { get; set; } = new List<ProjectsBankProjectField>();
    // add the team composition
}
