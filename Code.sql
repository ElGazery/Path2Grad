CREATE TABLE ChatBotConversation (
    ConversationID INT PRIMARY KEY IDENTITY(1,1),
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    StudentID INT NOT NULL   
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE

);

CREATE TABLE Messages (
    MessageID INT PRIMARY KEY IDENTITY(1,1),
    Sender NVARCHAR(100) NOT NULL,  
    MessageText NVARCHAR(MAX) NOT NULL,
    MessageTime DATETIME NOT NULL DEFAULT GETDATE(),
    ConversationID INT NOT NULL,
    FOREIGN KEY (ConversationID) REFERENCES ChatBotConversation(ConversationID) ON DELETE CASCADE
);
CREATE TABLE CV (
    CVID INT PRIMARY KEY IDENTITY(1,1),
    Link NVARCHAR(255) NULL,  -- Assuming a URL link to the CV
    CVFile VARBINARY(MAX) NULL,  -- Storing the CV file as binary (e.g., PDF)
    StudentID INT NOT NULL,
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE
);
CREATE TABLE Tracts (
    TrackID INT PRIMARY KEY IDENTITY(1,1),
    TrackName NVARCHAR(255) NOT NULL,
    Link NVARCHAR(255) NULL,  -- Assuming a URL link related to the track
    Description NVARCHAR(MAX) NULL,  -- Optional description of the track
    StudentID INT NOT NULL,
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE
);
CREATE TABLE Internships (
    InternshipID INT PRIMARY KEY IDENTITY(1,1),
    InternshipName NVARCHAR(255) NOT NULL,
    InternshipLink NVARCHAR(255) NULL,  -- Assuming a URL for the internship details
    StudentID INT NOT NULL,
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE
);

CREATE TABLE InternshipWorkFiles (
    InternshipWorkFilesID INT PRIMARY KEY IDENTITY(1,1),
    WorkFile VARBINARY(MAX) NOT NULL,  -- Storing work-related files (PDF)
    InternshipID INT NOT NULL,
    FOREIGN KEY (InternshipID) REFERENCES Internships(InternshipID) ON DELETE CASCADE
);

CREATE TABLE InternshipCertificates (
    InternshipCertificatesID INT PRIMARY KEY IDENTITY(1,1),
    Certificate VARBINARY(MAX) NOT NULL,  -- Storing certificate files (PDF, Image)
    InternshipID INT NOT NULL,
    FOREIGN KEY (InternshipID) REFERENCES Internships(InternshipID) ON DELETE CASCADE
);
CREATE TABLE Students (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    StudentName NVARCHAR(255) NOT NULL,
    StudentEmail NVARCHAR(255) UNIQUE NOT NULL,  -- Ensures unique emails
    StudentPassword NVARCHAR(255) NOT NULL,  -- Storing hashed passwords is recommended
    Phone NVARCHAR(20) UNIQUE NULL,  -- Allows NULL but ensures unique phone numbers
    AcademicYear INT NOT NULL,  
    ProjectID INT NULL,  -- Assuming a student might not have a project
    FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID) ON DELETE SET NULL
);
-------------------------------------------------------------------------------------
CREATE TABLE Projects (
    ProjectID INT PRIMARY KEY IDENTITY(1,1),
    ProjectName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,  -- Optional project description
    Requirements NVARCHAR(MAX) NULL,  -- Optional project requirements
    NumberOfTeam INT NOT NULL CHECK (NumberOfTeam > 0),  -- Ensures team size is positive
    SupervisorID INT NOT NULL,  -- Assuming a supervisor exists in another table
    FOREIGN KEY (SupervisorID) REFERENCES Supervisors(SupervisorID) 
);
CREATE TABLE Supervisors (
    SupervisorID INT PRIMARY KEY IDENTITY(1,1),
    SupervisorName NVARCHAR(255) NOT NULL,
    SupervisorEmail NVARCHAR(255) UNIQUE NOT NULL,  -- Ensures unique emails
    SupervisorPassword NVARCHAR(255) NOT NULL,  -- Consider hashing for security
    Phone NVARCHAR(20) UNIQUE NULL,  -- Allows NULL but ensures unique phone numbers
    Position NVARCHAR(255) NOT NULL,  -- Job position/title
    Specialization NVARCHAR(255) NOT NULL  -- Area of expertise
);
CREATE TABLE TrackTest (
    TrackTestID INT PRIMARY KEY IDENTITY(1,1),
    Question NVARCHAR(MAX) NOT NULL,  -- Stores the test question
    Answer NVARCHAR(MAX) NOT NULL,  -- Stores the answer to the question
    StudentID INT NOT NULL,
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE
);
CREATE TABLE SupervisorStudent (
    SupervisorID INT NOT NULL,
    StudentID INT NOT NULL,
    PRIMARY KEY (SupervisorID, StudentID),  -- Composite primary key
    FOREIGN KEY (SupervisorID) REFERENCES Supervisors(SupervisorID) ON DELETE CASCADE,
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE
);
CREATE TABLE Tasks (
    TaskID INT PRIMARY KEY IDENTITY(1,1),
    TaskName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,  -- Optional task description
    Deadline DATETIME NOT NULL,  -- Stores the deadline for the task
    Status NVARCHAR(50)  NOT NULL,
    ProjectID INT NOT NULL,
    StudentID INT NOT NULL,
    FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID) ON DELETE CASCADE,
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE
);
CREATE TABLE TeamMember (
    TeamMemberID INT PRIMARY KEY IDENTITY(1,1),
    TeamMember NVARCHAR(255) NOT NULL,  -- Name of the team member
    ProjectID INT NOT NULL,
    FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID) ON DELETE CASCADE
);
CREATE TABLE ProjectField (
    ProjectFieldID INT PRIMARY KEY IDENTITY(1,1),
    ProjectField NVARCHAR(255) NOT NULL,  -- Name of the project field
    ProjectID INT NOT NULL,
    FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID) ON DELETE CASCADE
);
CREATE TABLE ProjectsAdmin (
    AdminID INT PRIMARY KEY IDENTITY(1,1),
    AdminName NVARCHAR(255) NOT NULL,
    AdminEmail NVARCHAR(255) UNIQUE NOT NULL,  -- Ensures unique emails
    AdminPassword NVARCHAR(255) NOT NULL,  -- Consider hashing for security
    ProjectID INT NOT NULL,
    FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID) ON DELETE CASCADE
);
CREATE TABLE ProjectsBank (
    ProjectBankID INT PRIMARY KEY IDENTITY(1,1),
    ProjectName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,  -- Optional project description
    Requirements NVARCHAR(MAX) NULL,  -- Optional project requirements
    AdminID INT NOT NULL,
    FOREIGN KEY (AdminID) REFERENCES ProjectsAdmin(AdminID) ON DELETE CASCADE
);
CREATE TABLE ProjectsBankProjectField (
    ProjectFieldID INT PRIMARY KEY IDENTITY(1,1),
    ProjectField NVARCHAR(255) NOT NULL,  -- Name of the project field
    ProjectBankID INT NOT NULL,
    FOREIGN KEY (ProjectBankID) REFERENCES ProjectsBank(ProjectBankID) ON DELETE CASCADE
);
