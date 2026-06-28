using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Path2Grad.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<SupervisorProject> SupervisorProjects { get; set; }
    public virtual DbSet<SupervisorProjectJoinRequest> SupervisorProjectJoinRequests    { get; set; }
    public virtual DbSet<StudentProjectJoinRequest> StudentProjectJoinRequests { get; set; }
    public virtual DbSet<ChatBotConversation> ChatBotConversations { get; set; }

    public virtual DbSet<Cv> Cvs { get; set; }

    public virtual DbSet<Internship> Internships { get; set; }

    public virtual DbSet<InternshipCertificate> InternshipCertificates { get; set; }

    public virtual DbSet<InternshipWorkFile> InternshipWorkFiles { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectField> ProjectFields { get; set; }

    public virtual DbSet<ProjectsAdmin> ProjectsAdmins { get; set; }

    public virtual DbSet<ProjectsBank> ProjectsBanks { get; set; }

    public virtual DbSet<ProjectsBankProjectField> ProjectsBankProjectFields { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Supervisor> Supervisors { get; set; }

    public virtual DbSet<ProjectTask> Tasks { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }
    public virtual DbSet<TrackItem> TrackItem { get; set; }
    public virtual DbSet<ItemLesson> ItemLesson { get; set; }
    public virtual DbSet<TrackTest> TrackTests { get; set; }
    public virtual DbSet<ProjectRequirement> ProjectRequirements {  get; set; }
    public virtual DbSet<ProjectFile> ProjectFiles { get; set; }
    public virtual DbSet<Field> Fields { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=db20028.public.databaseasp.net; Database=db20028; User Id=db20028; Password=5j_P?Ak8x9T%; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatBotConversation>(entity =>
        {
            entity.HasKey(e => e.ConversationId).HasName("PK__ChatBotC__C050D89705F688F9");

            entity.HasOne(d => d.Student).WithMany(p => p.ChatBotConversations).HasConstraintName("FK__ChatBotCo__Stude__60A75C0F");
        });

        modelBuilder.Entity<Internship>(entity =>
        {
            entity.HasKey(e => e.InternshipId).HasName("PK__Internsh__01ADE59BBFE1C5AD");

            entity.HasOne(d => d.Student).WithMany(p => p.Internships).HasConstraintName("FK__Internshi__Stude__4F7CD00D");
        });

        modelBuilder.Entity<InternshipCertificate>(entity =>
        {
            entity.HasKey(e => e.InternshipCertificatesId).HasName("PK__Internsh__D378C5AEFB3D51DD");

            entity.HasOne(d => d.Internship).WithMany(p => p.InternshipCertificates).HasConstraintName("FK__Internshi__Inter__5535A963");
        });

        modelBuilder.Entity<InternshipWorkFile>(entity =>
        {
            entity.HasKey(e => e.InternshipWorkFilesId).HasName("PK__Internsh__A50A41255C06CC0C");

            entity.HasOne(d => d.Internship).WithMany(p => p.InternshipWorkFiles).HasConstraintName("FK__Internshi__Inter__52593CB8");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C037CF8EF836E");

            entity.Property(e => e.MessageTime).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Messages).HasConstraintName("FK__Messages__Conver__6477ECF3");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABED08C04AC41");
        });

        modelBuilder.Entity<ProjectField>(entity =>
        {
            entity.HasKey(e => e.ProjectFieldId).HasName("PK__ProjectF__489DEBAE6509B789");

            modelBuilder.Entity<ProjectField>()
                .HasOne(pf => pf.Project)
                .WithMany(p => p.ProjectFields)
                .HasForeignKey(pf => pf.ProjectId);

            modelBuilder.Entity<ProjectField>()
                .HasOne(pf => pf.Field)
                .WithMany(f => f.ProjectFields)
                .HasForeignKey(pf => pf.FieldId);
        });

        modelBuilder.Entity<ProjectsAdmin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Projects__719FE4E87D23BB9C");

        });

        modelBuilder.Entity<ProjectsBank>(entity =>
        {
            entity.HasKey(e => e.ProjectBankId).HasName("PK__Projects__2952D07D1CE7D6B7");

        });

        modelBuilder.Entity<ProjectsBankProjectField>(entity =>
        {
            entity.HasKey(e => e.ProjectFieldId).HasName("PK__Projects__489DEBAEF90731C3");

            entity.HasOne(d => d.ProjectBank).WithMany(p => p.ProjectsBankProjectFields).HasConstraintName("FK__ProjectsB__Proje__7E37BEF6");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52A79DEEA706D");

            entity.HasIndex(e => e.Phone, "UQ__Students__5C7E359E925BF2D5")
                .IsUnique()
                .HasFilter("([Phone] IS NOT NULL)");

            entity.HasOne(d => d.Project).WithMany(p => p.Students)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Students__Projec__48CFD27E");
           entity.HasOne(s => s.Track)
        .WithMany(t => t.Students)
        .HasForeignKey(s => s.TrackId)
        .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Supervisor>(entity =>
        {
            entity.HasKey(e => e.SupervisorId).HasName("PK__Supervis__6FAABDAF944797EE");

            entity.HasIndex(e => e.Phone, "UQ__Supervis__5C7E359E33217C14")
                .IsUnique()
                .HasFilter("([Phone] IS NOT NULL)");

            entity.HasMany(d => d.Students).WithMany(p => p.Supervisors)
                .UsingEntity<Dictionary<string, object>>(
                    "SupervisorStudent",
                    r => r.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK__Superviso__Stude__6B24EA82"),
                    l => l.HasOne<Supervisor>().WithMany()
                        .HasForeignKey("SupervisorId")
                        .HasConstraintName("FK__Superviso__Super__6A30C649"),
                    j =>
                    {
                        j.HasKey("SupervisorId", "StudentId").HasName("PK__Supervis__EC86EF08C0CB1384");
                        j.ToTable("SupervisorStudent");
                        j.HasIndex(new[] { "StudentId" }, "IX_SupervisorStudent_StudentID");
                        j.IndexerProperty<int>("SupervisorId").HasColumnName("SupervisorID");
                        j.IndexerProperty<int>("StudentId").HasColumnName("StudentID");
                    });
        });

        modelBuilder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949D133BB04F3");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks).HasConstraintName("FK__Tasks__ProjectID__6E01572D");

            entity.HasOne(d => d.Student).WithMany(p => p.Tasks).HasConstraintName("FK__Tasks__StudentID__6EF57B66");
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => e.TeamMemberId).HasName("PK__TeamMemb__C7C092855F89A1D1");

            entity.HasOne(d => d.Project).WithMany(p => p.TeamMembers).HasConstraintName("FK__TeamMembe__Proje__71D1E811");
        });
        modelBuilder.Entity<StudentProjectJoinRequest>()
         .HasOne(r => r.Sender)
             .WithMany()
             .HasForeignKey(r => r.SenderId)
             .OnDelete(DeleteBehavior.Restrict); // or .NoAction

        modelBuilder.Entity<StudentProjectJoinRequest>()
            .HasOne(r => r.Student)
            .WithMany()
            .HasForeignKey(r => r.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
