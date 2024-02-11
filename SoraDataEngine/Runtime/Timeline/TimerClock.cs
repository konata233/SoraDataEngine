using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Timeline
{
    /// <summary>
    /// 基础计时时钟的实现
    /// </summary>
    public class TimerClock : IClock, IDisposable
    {
        const int DEFAULT_PERIOD = 1000; // ms
        private Timer _timer;
        public bool IsRunning {  get; private set; }
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

        public TimerClock() 
        { 
            IsRunning = false;
            _isClockFlipped = false;
            _timer = new Timer(this._Flip, null, 0, DEFAULT_PERIOD);
        }

        public TimerClock(int period)
        {
            IsRunning = false;
            _isClockFlipped = false;
            _timer = new Timer(this._Flip, null, 0, period);
        }

        private void _Flip(object? obj)
        {
            if (IsRunning) Flip();
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

        public void Dispose()
        {
            IsRunning = false;
            _isClockFlipped = false;
            _timer.Dispose();
        }
    }
}
