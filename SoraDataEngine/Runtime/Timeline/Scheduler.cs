using SoraDataEngine.Commons.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Timeline
{
    /// <summary>
    /// 计划器组件，运算主线程在此处
    /// </summary>
    public class Scheduler : IDisposable
    {
        /// <summary>
        /// 是否已经销毁
        /// </summary>
        private bool _disposed;
        /// <summary>
        /// 是否正在运行
        /// </summary>
        private bool _ticking;
        /// <summary>
        /// 已经经过的时间刻
        /// </summary>
        private ulong _elapsedTime;
        /// <summary>
        /// 时钟组件
        /// </summary>
        private IClock _clock;
        /// <summary>
        /// 工作线程
        /// </summary>
        private Thread _thread;

        /// <summary>
        /// 实例
        /// </summary>
        public static Scheduler? Instance {  get; private set; }

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool Ticking
        {
            get { return _ticking; }
        }
        /// <summary>
        /// 已经经过的时间刻
        /// </summary>
        public ulong ElapsedTime
        {
            get { return _elapsedTime; }
        }

        /// <summary>
        /// 创建实例，默认时钟组件（不建议使用）
        /// </summary>
        public Scheduler()
        {
            Instance = RuntimeCore.Scheduler;
            _disposed = false;

            _clock = new Clock();

            _thread = new Thread(_Work)
            {
                IsBackground = true,
            };
        }

        /// <summary>
        /// 创建实例，并自动创建定时时钟组件
        /// </summary>
        /// <param name="period">定时时钟组件的间隔时间</param>
        public Scheduler(int period)
        {
            Instance = RuntimeCore.Scheduler;
            _disposed = false;

            _clock = new TimerClock(period);

            _thread = new Thread(_Work)
            {
                IsBackground = true,
            };
        }

        /// <summary>
        /// 创建实例，使用自己的时钟组件
        /// </summary>
        /// <param name="clock">时钟组件实例</param>
        public Scheduler(IClock clock)
        {
            Instance = RuntimeCore.Scheduler;
            _disposed = false;

            _clock = clock;

            _thread = new Thread(_Work)
            {
                IsBackground = true,
            };
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            _ticking = true;
            _elapsedTime = 0;
            _thread.Start();
            _clock.Start();

            OnStart(new SchedulerEventArgs
            {
                ElapsedTime = 0,
                Scheduler = this
            });
        }

        /// <summary>
        /// 暂停时钟计时与运算线程工作（不是阻塞！）
        /// </summary>
        public void Pause()
        {
            _ticking = false;
            _clock.Stop();
        }

        /// <summary>
        /// 从暂停状态下恢复
        /// </summary>
        public void Restart()
        {
            _ticking = true;
            _clock.Start();

            OnRestart(new SchedulerEventArgs
            {
                ElapsedTime = _elapsedTime,
                Scheduler = this
            });
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            _ticking = false;
            OnStop(new SchedulerEventArgs
            {
                ElapsedTime = _elapsedTime,
                Scheduler = this
            });
            _elapsedTime = 0;
        }

        /// <summary>
        /// 手动重置时钟组件
        /// </summary>
        public void FlipClock()
        {
            _clock.Flip();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="event"></param>
        public void RegistEvent(IEvent @event)
        {
            RuntimeCore.EventManager?.RegistEvent(@event);
        }

        /// <summary>
        /// 工作线程
        /// </summary>
        private void _Work()
        {
            while (!_disposed)
            {
                if (_clock.IsClockFlipped)
                {
                    _Tick();
                }
            }
        }

        /// <summary>
        /// 随机刻事件
        /// </summary>
        private void _Tick()
        {
            if (_ticking)
            {
                _elapsedTime++;
                _Proceed();
                OnTick(new SchedulerEventArgs
                {
                    ElapsedTime = _elapsedTime,
                    Scheduler = this
                });
            }
        }

        /// <summary>
        /// 随机刻执行的逻辑
        /// </summary>
        private void _Proceed()
        {
            RuntimeCore.EventManager?.Tick(_elapsedTime);
            RuntimeCore.Messenger?.Tick(_elapsedTime);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            _clock.Stop();
            _ticking = false;
            _disposed = true;
            _elapsedTime = 0;
            Instance = null;
        }

        protected virtual void OnStart(SchedulerEventArgs e)
        {
            SchedulerStartEvent?.Invoke(this, e);
        }

        protected virtual void OnStop(SchedulerEventArgs e)
        {
            SchedulerStopEvent?.Invoke(this, e);
        }

        protected virtual void OnRestart(SchedulerEventArgs e)
        {
            SchedulerRestartEvent?.Invoke(this, e);
        }

        protected virtual void OnTick(SchedulerEventArgs e)
        {
            SchedulerTickEvent?.Invoke(this, e);
        }

        /// <summary>
        /// 计划器启动时引发事件
        /// </summary>
        public event EventHandler<SchedulerEventArgs>? SchedulerStartEvent;

        /// <summary>
        /// 计划器重启时引发
        /// </summary>
        public event EventHandler<SchedulerEventArgs>? SchedulerRestartEvent;

        /// <summary>
        /// 计划器停止时引发
        /// </summary>
        public event EventHandler<SchedulerEventArgs>? SchedulerStopEvent;

        /// <summary>
        /// 计时器触发随机刻时引发
        /// </summary>
        public event EventHandler<SchedulerEventArgs>? SchedulerTickEvent;
    }

    public class SchedulerEventArgs : EventArgs
    {
        public ulong ElapsedTime { get; set; }
        public Scheduler? Scheduler { get; set; }
    }

    public class ScheduledEventRaisedEventArgs : EventArgs
    {
        public ulong ElapsedTime { get; set; }
    }
}
