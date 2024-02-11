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
        private bool _isClockFlipped;

        public bool IsClockFlipped 
        {
            get 
            {
                if (_isClockFlipped)
                {
                    _isClockFlipped = false;
                    return true;
                }
                else { return false; }
            } 
            private set
            {
                _isClockFlipped = value;
            }
        }

        public Clock() 
        {
            IsRunning = false;
            _isClockFlipped = false;
        }

        public void Flip()
        {
            if (IsRunning) _isClockFlipped = true;
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
