// BreathingActivity.cs
using System;

namespace Mindfulness
{
    public class BreathingActivity : Activity
    {
        public BreathingActivity()
            : base(
                "Breathing Activity",
                "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        { }

        protected override void ExecuteCore()
        {
            int elapsed = 0;
            bool inhale = true;

            while (elapsed < DurationSeconds)
            {
                int remaining = DurationSeconds - elapsed;
                int phase = Math.Min(4, Math.Max(2, remaining)); // 2-4s per phase
                if (inhale)
                {
                    Console.Write("\nBreathe in... ");
                    Countdown(phase);
                    BreathingBar(phase, inhale: true);
                }
                else
                {
                    Console.Write("\nBreathe out... ");
                    Countdown(phase);
                    BreathingBar(phase, inhale: false);
                }
                inhale = !inhale;
                elapsed += phase;
            }
        }
    }
}
