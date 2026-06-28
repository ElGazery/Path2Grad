using System.Text.Json.Serialization;

namespace Path2Grad.Models
{
    public class SurveyResponse
    {
        [JsonPropertyName("fieldOfInterest")]
        public string FieldOfInterest { get; set; }

        [JsonPropertyName("pythonComfortLevel")]
        public string PythonComfortLevel { get; set; }

        [JsonPropertyName("databaseExperience")]
        public string DatabaseExperience { get; set; }

        [JsonPropertyName("developmentPreference")]
        public string DevelopmentPreference { get; set; }

        [JsonPropertyName("dataAnalysisEnjoyment")]
        public string DataAnalysisEnjoyment { get; set; }

        [JsonPropertyName("cybersecurityPassion")]
        public string CybersecurityPassion { get; set; }

        [JsonPropertyName("problemSolvingSkills")]
        public string ProblemSolvingSkills { get; set; }

        [JsonPropertyName("projectExperience")]
        public string ProjectExperience { get; set; }

        [JsonPropertyName("dataToolsEnjoyment")]
        public string DataToolsEnjoyment { get; set; }

        [JsonPropertyName("graduationProjectType")]
        public string GraduationProjectType { get; set; }
    }
}
