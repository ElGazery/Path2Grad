using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Path2Grad.Migrations
{
    /// <inheritdoc />
    public partial class inti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    FieldId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.FieldId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfTeam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Projects__761ABED08C04AC41", x => x.ProjectID);
                });

            migrationBuilder.CreateTable(
                name: "ProjectsAdmin",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AdminEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AdminPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Pic = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Projects__719FE4E87D23BB9C", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "ProjectsBank",
                columns: table => new
                {
                    ProjectBankID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectSpecification = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Projects__2952D07D1CE7D6B7", x => x.ProjectBankID);
                });

            migrationBuilder.CreateTable(
                name: "Supervisors",
                columns: table => new
                {
                    SupervisorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupervisorName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SupervisorEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SupervisorPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Pic = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Supervis__6FAABDAF944797EE", x => x.SupervisorID);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    TrackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.TrackID);
                });

            migrationBuilder.CreateTable(
                name: "ProjectField",
                columns: table => new
                {
                    ProjectFieldID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    FieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProjectF__489DEBAE6509B789", x => x.ProjectFieldID);
                    table.ForeignKey(
                        name: "FK_ProjectField_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "FieldId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectField_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectFiles",
                columns: table => new
                {
                    ProjectFileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFiles", x => x.ProjectFileId);
                    table.ForeignKey(
                        name: "FK_ProjectFiles_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRequirements",
                columns: table => new
                {
                    RequirementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequirementName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PdfContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRequirements", x => x.RequirementId);
                    table.ForeignKey(
                        name: "FK_ProjectRequirements_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamMember",
                columns: table => new
                {
                    TeamMemberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamMember = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TeamMemb__C7C092855F89A1D1", x => x.TeamMemberID);
                    table.ForeignKey(
                        name: "FK__TeamMembe__Proje__71D1E811",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectsBankProjectField",
                columns: table => new
                {
                    ProjectFieldID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectField = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProjectBankID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Projects__489DEBAEF90731C3", x => x.ProjectFieldID);
                    table.ForeignKey(
                        name: "FK__ProjectsB__Proje__7E37BEF6",
                        column: x => x.ProjectBankID,
                        principalTable: "ProjectsBank",
                        principalColumn: "ProjectBankID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupervisorProjectJoinRequests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorProjectJoinRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_SupervisorProjectJoinRequests_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectID");
                    table.ForeignKey(
                        name: "FK_SupervisorProjectJoinRequests_Supervisors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisors",
                        principalColumn: "SupervisorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupervisorProjects",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorProjects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SupervisorProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupervisorProjects_Supervisors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisors",
                        principalColumn: "SupervisorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StudentEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StudentPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Pic = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    AcademicYear = table.Column<int>(type: "int", nullable: false),
                    ProjectID = table.Column<int>(type: "int", nullable: true),
                    TrackId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Students__32C52A79DEEA706D", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Students_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__Students__Projec__48CFD27E",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TrackItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackItem_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatBotConversation",
                columns: table => new
                {
                    ConversationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChatBotC__C050D89705F688F9", x => x.ConversationID);
                    table.ForeignKey(
                        name: "FK__ChatBotCo__Stude__60A75C0F",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CV",
                columns: table => new
                {
                    CVID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CVName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CVFile = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV", x => x.CVID);
                    table.ForeignKey(
                        name: "FK_CV_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID");
                });

            migrationBuilder.CreateTable(
                name: "Internships",
                columns: table => new
                {
                    InternshipID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternshipName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    InternshipLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StudentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Internsh__01ADE59BBFE1C5AD", x => x.InternshipID);
                    table.ForeignKey(
                        name: "FK__Internshi__Stude__4F7CD00D",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID");
                });

            migrationBuilder.CreateTable(
                name: "StudentProjectJoinRequests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StudentId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProjectJoinRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_StudentProjectJoinRequests_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectID");
                    table.ForeignKey(
                        name: "FK_StudentProjectJoinRequests_Students_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProjectJoinRequests_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProjectJoinRequests_Students_StudentId1",
                        column: x => x.StudentId1,
                        principalTable: "Students",
                        principalColumn: "StudentID");
                });

            migrationBuilder.CreateTable(
                name: "SupervisorStudent",
                columns: table => new
                {
                    SupervisorID = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Supervis__EC86EF08C0CB1384", x => new { x.SupervisorID, x.StudentID });
                    table.ForeignKey(
                        name: "FK__Superviso__Stude__6B24EA82",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Superviso__Super__6A30C649",
                        column: x => x.SupervisorID,
                        principalTable: "Supervisors",
                        principalColumn: "SupervisorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tasks__7C6949D133BB04F3", x => x.TaskID);
                    table.ForeignKey(
                        name: "FK__Tasks__ProjectID__6E01572D",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Tasks__StudentID__6EF57B66",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackTest",
                columns: table => new
                {
                    TrackTestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackTest", x => x.TrackTestID);
                    table.ForeignKey(
                        name: "FK_TrackTest_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemLesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsComplet = table.Column<bool>(type: "bit", nullable: true),
                    TrackItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemLesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemLesson_TrackItem_TrackItemId",
                        column: x => x.TrackItemId,
                        principalTable: "TrackItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sender = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ConversationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Messages__C87C037CF8EF836E", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK__Messages__Conver__6477ECF3",
                        column: x => x.ConversationID,
                        principalTable: "ChatBotConversation",
                        principalColumn: "ConversationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternshipCertificates",
                columns: table => new
                {
                    InternshipCertificatesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Certificate = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    InternshipID = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Internsh__D378C5AEFB3D51DD", x => x.InternshipCertificatesID);
                    table.ForeignKey(
                        name: "FK_InternshipCertificates_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Internshi__Inter__5535A963",
                        column: x => x.InternshipID,
                        principalTable: "Internships",
                        principalColumn: "InternshipID");
                });

            migrationBuilder.CreateTable(
                name: "InternshipWorkFiles",
                columns: table => new
                {
                    InternshipWorkFilesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    InternshipID = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Internsh__A50A41255C06CC0C", x => x.InternshipWorkFilesID);
                    table.ForeignKey(
                        name: "FK_InternshipWorkFiles_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Internshi__Inter__52593CB8",
                        column: x => x.InternshipID,
                        principalTable: "Internships",
                        principalColumn: "InternshipID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatBotConversation_StudentID",
                table: "ChatBotConversation",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_CV_StudentID",
                table: "CV",
                column: "StudentID",
                unique: true,
                filter: "[StudentID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipCertificates_InternshipID",
                table: "InternshipCertificates",
                column: "InternshipID");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipCertificates_StudentId",
                table: "InternshipCertificates",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_StudentID",
                table: "Internships",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipWorkFiles_InternshipID",
                table: "InternshipWorkFiles",
                column: "InternshipID");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipWorkFiles_StudentId",
                table: "InternshipWorkFiles",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemLesson_TrackItemId",
                table: "ItemLesson",
                column: "TrackItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationID",
                table: "Messages",
                column: "ConversationID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectField_FieldId",
                table: "ProjectField",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectField_ProjectID",
                table: "ProjectField",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFiles_ProjectId",
                table: "ProjectFiles",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequirements_ProjectId",
                table: "ProjectRequirements",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "UQ__Projects__F2AA7AD96AD4F7D5",
                table: "ProjectsAdmin",
                column: "AdminEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsBankProjectField_ProjectBankID",
                table: "ProjectsBankProjectField",
                column: "ProjectBankID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProjectJoinRequests_ProjectId",
                table: "StudentProjectJoinRequests",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProjectJoinRequests_SenderId",
                table: "StudentProjectJoinRequests",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProjectJoinRequests_StudentId",
                table: "StudentProjectJoinRequests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProjectJoinRequests_StudentId1",
                table: "StudentProjectJoinRequests",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProjectID",
                table: "Students",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_TrackId",
                table: "Students",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "UQ__Students__3569CFDBFD684B18",
                table: "Students",
                column: "StudentEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Students__5C7E359E925BF2D5",
                table: "Students",
                column: "Phone",
                unique: true,
                filter: "([Phone] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorProjectJoinRequests_ProjectId",
                table: "SupervisorProjectJoinRequests",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorProjectJoinRequests_SupervisorId",
                table: "SupervisorProjectJoinRequests",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorProjects_ProjectId",
                table: "SupervisorProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorProjects_SupervisorId",
                table: "SupervisorProjects",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "UQ__Supervis__5C7E359E33217C14",
                table: "Supervisors",
                column: "Phone",
                unique: true,
                filter: "([Phone] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ__Supervis__FAB2DC03A7DD1066",
                table: "Supervisors",
                column: "SupervisorEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorStudent_StudentID",
                table: "SupervisorStudent",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectID",
                table: "Tasks",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StudentID",
                table: "Tasks",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_ProjectID",
                table: "TeamMember",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TrackItem_TrackId",
                table: "TrackItem",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackTest_StudentID",
                table: "TrackTest",
                column: "StudentID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CV");

            migrationBuilder.DropTable(
                name: "InternshipCertificates");

            migrationBuilder.DropTable(
                name: "InternshipWorkFiles");

            migrationBuilder.DropTable(
                name: "ItemLesson");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ProjectField");

            migrationBuilder.DropTable(
                name: "ProjectFiles");

            migrationBuilder.DropTable(
                name: "ProjectRequirements");

            migrationBuilder.DropTable(
                name: "ProjectsAdmin");

            migrationBuilder.DropTable(
                name: "ProjectsBankProjectField");

            migrationBuilder.DropTable(
                name: "StudentProjectJoinRequests");

            migrationBuilder.DropTable(
                name: "SupervisorProjectJoinRequests");

            migrationBuilder.DropTable(
                name: "SupervisorProjects");

            migrationBuilder.DropTable(
                name: "SupervisorStudent");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TeamMember");

            migrationBuilder.DropTable(
                name: "TrackTest");

            migrationBuilder.DropTable(
                name: "Internships");

            migrationBuilder.DropTable(
                name: "TrackItem");

            migrationBuilder.DropTable(
                name: "ChatBotConversation");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "ProjectsBank");

            migrationBuilder.DropTable(
                name: "Supervisors");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
