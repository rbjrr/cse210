using System;

namespace EternalQuest
{
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points) { }

        public override int RecordEvent()
        {
            // Eternal goals never complete; each record gives points
            return _points;
        }

        public override bool IsComplete()
        {
            return false;
        }

        public override string GetDetailsString()
        {
            return $"[~] {_name} ({_description})";
        }

        public override string GetStringRepresentation()
        {
            // Eternal|name|description|points
            return $"Eternal|{_name}|{_description}|{_points}";
        }
    }
}
