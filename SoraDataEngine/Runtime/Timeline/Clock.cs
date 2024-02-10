using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Timeline
{
    /// <summary>
    /// 基础时钟的实现
    /// </summary>
    public class Clock : IClock
    {
        public bool IsRunning { get; private set; }
        public bool IsClockFlipped 
        {
            get 
            {
                if (IsClockFlipped)
                {
                    IsClockFlipped = false;
                    return true;
                }
                else { return false; }
            } 
            private set
            {
                IsClockFlipped = value;
            }
        }

        public Clock() 
        {
            IsRunning = false;
            IsClockFlipped = false;
        }

        public void Flip()
        {
            if (IsRunning) IsClockFlipped = true;
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void Stop() 
        { 
            IsRunning = false;
        }
    }
}
