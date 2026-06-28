using System;
using System.Collections.Generic;
using System.Linq;
using Path2Grad.Domain.Entities;
using Path2Grad.Domain.Enums;

namespace Path2Grad.Application.Helpers
{
    public class CareerTrackMatcher
    {
        private Dictionary<SurveyResponse, CareerTrack> _responseToTrackMap;
        private Dictionary<string, List<string>> _validAnswers;

        public CareerTrackMatcher()
        {
            InitializeValidAnswers();
            InitializeResponseMap();
        }

        private void InitializeValidAnswers()
        {
            _validAnswers = new Dictionary<string, List<string>>
            {
                {"FieldOfInterest", new List<string> {"Artificial Intelligence", "Data Science", "Software Development", "Web Development", "Cybersecurity", "Other"}},
                {"PythonComfortLevel", new List<string> {"Excellent", "Good", "Average", "Weak", "I don't know it"}},
                {"DatabaseExperience", new List<string> {"Excellent", "Good", "Average", "Weak", "I don't know it"}},
                {"DevelopmentPreference", new List<string> {"Front-end", "Back-end", "Both", "Not sure"}},
                {"DataAnalysisEnjoyment", new List<string> {"Yes", "No", "To some extent"}},
                {"CybersecurityPassion", new List<string> {"Yes", "No", "To some extent"}},
                {"ProblemSolvingSkills", new List<string> {"Very strong", "Good", "Average", "Weak"}},
                {"ProjectExperience", new List<string> {"Yes", "No", "Currently working on one"}},
                {"DataToolsEnjoyment", new List<string> {"Yes", "No", "Haven't tried"}},
                {"GraduationProjectType", new List<string> {"Web application", "Data analysis", "Desktop application", "Network security system", "AI-based application", "Other"}}
            };
        }

        private void InitializeResponseMap()
        {
            _responseToTrackMap = new Dictionary<SurveyResponse, CareerTrack>();

            AddTrackVariations(new SurveyResponse
            {
                FieldOfInterest = "Artificial Intelligence",
                PythonComfortLevel = "Excellent",
                DataAnalysisEnjoyment = "Yes",
                ProblemSolvingSkills = "Very strong",
                GraduationProjectType = "AI-based application"
            }, CareerTrack.AI_Engineer);

            AddTrackVariations(new SurveyResponse
            {
                FieldOfInterest = "Artificial Intelligence",
                PythonComfortLevel = "Good",
                DataAnalysisEnjoyment = "To some extent",
                ProblemSolvingSkills = "Good",
                GraduationProjectType = "AI-based application"
            }, CareerTrack.AI_Engineer);

            AddTrackVariations(new SurveyResponse
            {
                FieldOfInterest = "Web Development",
                DevelopmentPreference = "Front-end",
                GraduationProjectType = "Web application"
            }, CareerTrack.Frontend_Developer);

            AddTrackVariations(new SurveyResponse
            {
                FieldOfInterest = "Software Development",
                DevelopmentPreference = "Back-end",
                PythonComfortLevel = "Good",
                DatabaseExperience = "Good",
                GraduationProjectType = "Web application"
            }, CareerTrack.Backend_Developer);

            AddTrackVariations(new SurveyResponse
            {
                FieldOfInterest = "Web Development",
                DevelopmentPreference = "Front-end",
                GraduationProjectType = "Desktop application"
            }, CareerTrack.Flutter_Developer);

            AddTrackVariations(new SurveyResponse
            {
                FieldOfInterest = "Software Development",
                DevelopmentPreference = "Both",
                GraduationProjectType = "Desktop application"
            }, CareerTrack.Flutter_Developer);

            AddTrackVariations(new SurveyResponse
            {
                FieldOfInterest = "Cybersecurity",
                CybersecurityPassion = "Yes",
                ProblemSolvingSkills = "Very strong",
                GraduationProjectType = "Network security system"
            }, CareerTrack.Cybersecurity_Specialist);

            AddTrackVariations(new SurveyResponse
            {
                FieldOfInterest = "Data Science",
                PythonComfortLevel = "Good",
                DatabaseExperience = "Good",
                DataAnalysisEnjoyment = "Yes",
                DataToolsEnjoyment = "Yes",
                GraduationProjectType = "Data analysis"
            }, CareerTrack.Data_Scientist);

            AddTrackVariations(new SurveyResponse
            {
                FieldOfInterest = "Web Development",
                DevelopmentPreference = "Both",
                PythonComfortLevel = "Good",
                DatabaseExperience = "Good",
                GraduationProjectType = "Web application"
            }, CareerTrack.FullStack_Developer);

            AddTrackVariations(new SurveyResponse
            {
                FieldOfInterest = "Software Development",
                DevelopmentPreference = "Back-end",
                ProblemSolvingSkills = "Very strong",
                ProjectExperience = "Yes",
                GraduationProjectType = "Other"
            }, CareerTrack.DevOps_Engineer);
        }

        private void AddTrackVariations(SurveyResponse baseResponse, CareerTrack track)
        {
            var variations = GenerateVariations(baseResponse);
            foreach (var variation in variations)
            {
                if (!_responseToTrackMap.ContainsKey(variation))
                {
                    _responseToTrackMap.Add(variation, track);
                }
            }
        }

        private List<SurveyResponse> GenerateVariations(SurveyResponse baseResponse)
        {
            var variations = new List<SurveyResponse> { baseResponse };

            var properties = typeof(SurveyResponse).GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name == "FieldOfInterest" || prop.Name == "GraduationProjectType")
                    continue;

                var newVariations = new List<SurveyResponse>();
                foreach (var variation in variations)
                {
                    var currentValue = prop.GetValue(variation)?.ToString();
                    if (currentValue != null && _validAnswers.ContainsKey(prop.Name))
                    {
                        foreach (var alternative in _validAnswers[prop.Name])
                        {
                            if (alternative != currentValue)
                            {
                                var newVar = CloneResponse(variation);
                                prop.SetValue(newVar, alternative);
                                newVariations.Add(newVar);
                            }
                        }
                    }
                }
                variations.AddRange(newVariations);
            }

            return variations.Distinct(new SurveyResponseComparer()).ToList();
        }

        private SurveyResponse CloneResponse(SurveyResponse original)
        {
            return new SurveyResponse
            {
                FieldOfInterest = original.FieldOfInterest,
                PythonComfortLevel = original.PythonComfortLevel,
                DatabaseExperience = original.DatabaseExperience,
                DevelopmentPreference = original.DevelopmentPreference,
                DataAnalysisEnjoyment = original.DataAnalysisEnjoyment,
                CybersecurityPassion = original.CybersecurityPassion,
                ProblemSolvingSkills = original.ProblemSolvingSkills,
                ProjectExperience = original.ProjectExperience,
                DataToolsEnjoyment = original.DataToolsEnjoyment,
                GraduationProjectType = original.GraduationProjectType
            };
        }

        public CareerTrack DetermineCareerTrack(SurveyResponse userResponse)
        {
            if (_responseToTrackMap.TryGetValue(userResponse, out var exactMatch))
            {
                return exactMatch;
            }

            var bestMatch = FindBestMatch(userResponse);
            return bestMatch != null ? _responseToTrackMap[bestMatch] : CareerTrack.Undetermined;
        }

        private SurveyResponse FindBestMatch(SurveyResponse userResponse)
        {
            SurveyResponse bestMatch = null;
            int highestScore = -1;

            foreach (var storedResponse in _responseToTrackMap.Keys)
            {
                int score = CalculateMatchScore(userResponse, storedResponse);
                if (score > highestScore)
                {
                    highestScore = score;
                    bestMatch = storedResponse;
                }
            }

            return highestScore > 5 ? bestMatch : null;
        }

        private int CalculateMatchScore(SurveyResponse user, SurveyResponse stored)
        {
            int score = 0;
            var properties = typeof(SurveyResponse).GetProperties();

            foreach (var prop in properties)
            {
                var userValue = prop.GetValue(user)?.ToString();
                var storedValue = prop.GetValue(stored)?.ToString();

                if (userValue == storedValue)
                {
                    if (prop.Name == "FieldOfInterest" || prop.Name == "GraduationProjectType")
                        score += 3;
                    else
                        score += 1;
                }
            }

            return score;
        }
    }
}
