using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CG_Lab1V2
{
    internal class LoopingThread
    {
        private Action _loopAction;
        private Thread _thread;

        private AutoResetEvent _stopEvent;
        private AutoResetEvent _pauseEvent;
        private AutoResetEvent _waitEvent;
        private AutoResetEvent _resumeEvent;
        public int _pauseVal { get; set; }

        private bool _isRunning;
        public LoopingThread(Action action)
        {
            _loopAction = action;
            _thread = new Thread(Loop);

            _stopEvent = new AutoResetEvent(false);
            _pauseEvent = new AutoResetEvent(false);
            _waitEvent = new AutoResetEvent(false);
            _resumeEvent = new AutoResetEvent(false);

            _isRunning = false;
        }

        private void Loop()
        {
            do
            {
                _loopAction();

                if (_pauseEvent.WaitOne(_pauseVal))
                {
                    _waitEvent.Set();
                    _resumeEvent.WaitOne(Timeout.Infinite);
                }
            } while (!_stopEvent.WaitOne(0));
        }

        public void Start()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _thread.Start();
            }
            else
            {
                Resume();
            }
        }

        public void Resume()
        {
            _resumeEvent.Set();
        }

        public void Pause()
        {
            _pauseEvent.Set();
            _waitEvent.WaitOne(0);
        }
    }
}
