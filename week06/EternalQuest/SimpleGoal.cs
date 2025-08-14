using System;

namespace EternalQuest
{
    public class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string name, string description, int points, bool isComplete = false)
            : base(name, description, points)
        {
            _isComplete = isComplete;
        }

        public override int RecordEvent()
        {
            if (_isComplete)
                return 0;

            _isComplete = true;
            return _points;
        }

        public override bool IsComplete()
        {
            return _isComplete;
        }

        public override string GetDetailsString()
        {
            string mark = _isComplete ? "[X]" : "[ ]";
            return $"{mark} {_name} ({_description})";
        }

        public override string GetStringRepresentation()
        {
            // Simple|name|description|points|isComplete
            return $"Simple|{_name}|{_description}|{_points}|{_isComplete}";
        }
    }
}
