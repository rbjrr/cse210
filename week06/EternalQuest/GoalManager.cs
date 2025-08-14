using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    public class GoalManager
    {
        private List<Goal> _goals;
        private int _score;

        public GoalManager()
        {
            _goals = new List<Goal>();
            _score = 0;
        }

        public int Score => _score;

        public IReadOnlyList<Goal> Goals => _goals.AsReadOnly();

        public void AddGoal(Goal g)
        {
            _goals.Add(g);
        }

        public void ListGoals()
        {
            Console.WriteLine("Goals:");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i+1}. {_goals[i].GetDetailsString()}");
            }
        }

        public int RecordEvent(int goalIndex)
        {
            if (goalIndex < 0 || goalIndex >= _goals.Count)
                throw new ArgumentOutOfRangeException(nameof(goalIndex));
            int gained = _goals[goalIndex].RecordEvent();
            _score += gained;
            return gained;
        }

        public void Save(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(_score);
                foreach (var g in _goals)
                {
                    sw.WriteLine(g.GetStringRepresentation());
                }
            }
        }

        public void Load(string path)
        {
            _goals.Clear();
            _score = 0;
            if (!File.Exists(path)) return;

            using (StreamReader sr = new StreamReader(path))
            {
                string line = sr.ReadLine();
                if (line != null)
                {
                    int.TryParse(line, out _score);
                }

                while ((line = sr.ReadLine()) != null)
                {
                    // Parse lines by type
                    var parts = line.Split('|');
                    if (parts.Length == 0) continue;
                    var type = parts[0];
                    try
                    {
                        if (type == "Simple" && parts.Length == 5)
                        {
                            string name = parts[1];
                            string desc = parts[2];
                            int pts = int.Parse(parts[3]);
                            bool isComplete = bool.Parse(parts[4]);
                            _goals.Add(new SimpleGoal(name, desc, pts, isComplete));
                        }
                        else if (type == "Eternal" && parts.Length == 4)
                        {
                            string name = parts[1];
                            string desc = parts[2];
                            int pts = int.Parse(parts[3]);
                            _goals.Add(new EternalGoal(name, desc, pts));
                        }
                        else if (type == "Checklist" && parts.Length == 7)
                        {
                            string name = parts[1];
                            string desc = parts[2];
                            int pts = int.Parse(parts[3]);
                            int target = int.Parse(parts[4]);
                            int bonus = int.Parse(parts[5]);
                            int amt = int.Parse(parts[6]);
                            _goals.Add(new ChecklistGoal(name, desc, pts, target, bonus, amt));
                        }
                    }
                    catch
                    {
                        // ignore malformed lines
                    }
                }
            }
        }
    }
}
