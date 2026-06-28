using System;
using System.Collections.Generic;
using System.Linq;
using Path2Grad.Helper;
using Path2Grad.Models;

public enum CareerTrack
{
    AI_Engineer,
    Frontend_Developer,
    Backend_Developer,
    Flutter_Developer,
    Cybersecurity_Specialist,
    Data_Scientist,
    FullStack_Developer,
    DevOps_Engineer,
    Undetermined
}

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

        // AI Engineer patterns
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

        // Frontend Developer patterns
        AddTrackVariations(new SurveyResponse
        {
            FieldOfInterest = "Web Development",
            DevelopmentPreference = "Front-end",
            GraduationProjectType = "Web application"
        }, CareerTrack.Frontend_Developer);

        // Backend Developer patterns
        AddTrackVariations(new SurveyResponse
        {
            FieldOfInterest = "Software Development",
            DevelopmentPreference = "Back-end",
            PythonComfortLevel = "Good",
            DatabaseExperience = "Good",
            GraduationProjectType = "Web application"
        }, CareerTrack.Backend_Developer);

        // Flutter Developer patterns (considered as frontend/mobile)
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

        // Cybersecurity Specialist patterns
        AddTrackVariations(new SurveyResponse
        {
            FieldOfInterest = "Cybersecurity",
            CybersecurityPassion = "Yes",
            ProblemSolvingSkills = "Very strong",
            GraduationProjectType = "Network security system"
        }, CareerTrack.Cybersecurity_Specialist);

        // Data Scientist patterns
        AddTrackVariations(new SurveyResponse
        {
            FieldOfInterest = "Data Science",
            PythonComfortLevel = "Good",
            DatabaseExperience = "Good",
            DataAnalysisEnjoyment = "Yes",
            DataToolsEnjoyment = "Yes",
            GraduationProjectType = "Data analysis"
        }, CareerTrack.Data_Scientist);

        // FullStack Developer patterns
        AddTrackVariations(new SurveyResponse
        {
            FieldOfInterest = "Web Development",
            DevelopmentPreference = "Both",
            PythonComfortLevel = "Good",
            DatabaseExperience = "Good",
            GraduationProjectType = "Web application"
        }, CareerTrack.FullStack_Developer);

        // DevOps Engineer patterns
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
        // Generate variations of the base response by allowing some properties to be flexible
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

        // Create variations by making some properties more flexible
        var properties = typeof(SurveyResponse).GetProperties();
        foreach (var prop in properties)
        {
            if (prop.Name == "FieldOfInterest" || prop.Name == "GraduationProjectType")
                continue; // These are key identifiers we want to keep strict

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
        // Try exact match first
        if (_responseToTrackMap.TryGetValue(userResponse, out var exactMatch))
        {
            return exactMatch;
        }

        // If no exact match, find the most similar response
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

        return highestScore > 5 ? bestMatch : null; // Minimum threshold
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
                // Key properties get higher weights
                if (prop.Name == "FieldOfInterest" || prop.Name == "GraduationProjectType")
                    score += 3;
                else
                    score += 1;
            }
        }

        return score;
    }
}
