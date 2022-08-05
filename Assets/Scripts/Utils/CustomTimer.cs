using UnityEngine;

namespace Core
{
    public sealed class CustomTimer
    {
        public float Timer { get; private set; }
        public float Interval { get; private set; }

        public CustomTimer(float interval)
        {
            Timer = 0.0f;
            Interval = interval;
        }

        public bool Tick()
        {
            Timer += Time.deltaTime;
            if (Timer > Interval)
            {
                Timer -= Interval;
                return true;
            }
            return false;
        }

        public void Reset() => Timer = 0.0f;
    }
}
