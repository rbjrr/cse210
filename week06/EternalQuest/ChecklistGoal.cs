using System;

namespace EternalQuest
{
    public class ChecklistGoal : Goal
    {
        private int _amountCompleted;
        private int _target;
        private int _bonus;

        public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted = 0)
            : base(name, description, points)
        {
            _target = target;
            _bonus = bonus;
            _amountCompleted = amountCompleted;
        }

        public override int RecordEvent()
        {
            if (_amountCompleted >= _target)
                return 0;

            _amountCompleted++;
            if (_amountCompleted == _target)
            {
                // give regular points plus bonus
                return _points + _bonus;
            }
            return _points;
        }

        public override bool IsComplete()
        {
            return _amountCompleted >= _target;
        }

        public override string GetDetailsString()
        {
            string mark = IsComplete() ? "[X]" : "[ ]";
            return $"{mark} {_name} ({_description}) -- Completed {_amountCompleted}/{_target}";
        }

        public override string GetStringRepresentation()
        {
            // Checklist|name|description|points|target|bonus|amountCompleted
            return $"Checklist|{_name}|{_description}|{_points}|{_target}|{_bonus}|{_amountCompleted}";
        }
    }
}
