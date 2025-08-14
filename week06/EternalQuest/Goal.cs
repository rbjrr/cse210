using System;

namespace EternalQuest
{
    // Base class for all goals
    public abstract class Goal
    {
        protected string _name;
        protected string _description;
        protected int _points;

        public Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
        }

        // Record an event (e.g., user progressed this goal)
        public abstract int RecordEvent();

        // Whether the goal is completed (eternal goals always false)
        public abstract bool IsComplete();

        // A short details string for display
        public virtual string GetDetailsString()
        {
            return $"{_name} ({_description})";
        }

        // String representation suitable for saving to file
        public abstract string GetStringRepresentation();
    }
}
