using Path2Grad.Models;

namespace Path2Grad.Helper
{

    public class SurveyResponseComparer : IEqualityComparer<SurveyResponse>
    {
        public bool Equals(SurveyResponse x, SurveyResponse y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;

            return x.FieldOfInterest == y.FieldOfInterest &&
                   x.PythonComfortLevel == y.PythonComfortLevel &&
                   x.DatabaseExperience == y.DatabaseExperience &&
                   x.DevelopmentPreference == y.DevelopmentPreference &&
                   x.DataAnalysisEnjoyment == y.DataAnalysisEnjoyment &&
                   x.CybersecurityPassion == y.CybersecurityPassion &&
                   x.ProblemSolvingSkills == y.ProblemSolvingSkills &&
                   x.ProjectExperience == y.ProjectExperience &&
                   x.DataToolsEnjoyment == y.DataToolsEnjoyment &&
                   x.GraduationProjectType == y.GraduationProjectType;
        }

        public int GetHashCode(SurveyResponse obj)
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (obj.FieldOfInterest?.GetHashCode() ?? 0);
                hash = hash * 23 + (obj.PythonComfortLevel?.GetHashCode() ?? 0);
                hash = hash * 23 + (obj.DatabaseExperience?.GetHashCode() ?? 0);
                hash = hash * 23 + (obj.DevelopmentPreference?.GetHashCode() ?? 0);
                hash = hash * 23 + (obj.DataAnalysisEnjoyment?.GetHashCode() ?? 0);
                hash = hash * 23 + (obj.CybersecurityPassion?.GetHashCode() ?? 0);
                hash = hash * 23 + (obj.ProblemSolvingSkills?.GetHashCode() ?? 0);
                hash = hash * 23 + (obj.ProjectExperience?.GetHashCode() ?? 0);
                hash = hash * 23 + (obj.DataToolsEnjoyment?.GetHashCode() ?? 0);
                hash = hash * 23 + (obj.GraduationProjectType?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
