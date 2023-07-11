using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace KTool.KThread
{
    public abstract class TaskBase
    {
        #region Properties
        protected const int THREAD_SLEEP_DEFAULT = 10;
        private int interComplete,
            interPlaying,
            interError;
        private string error;

        public bool IsSuccess
        {
            private set
            {
                Interlocked.Exchange(ref interComplete, (value ? 1 : 0));
            }
            get
            {
                return (Interlocked.Add(ref interComplete, 0) == 1);
            }
        }
        public bool IsPlaying
        {
            private set
            {
                Interlocked.Exchange(ref interPlaying, (value ? 1 : 0));
            }
            get
            {
                return (Interlocked.Add(ref interPlaying, 0) == 1);
            }
        }
        public string Error
        {
            private set
            {
                while (true)
                {
                    if (Interlocked.Exchange(ref interError, 1) == 0)
                    {
                        error = value;
                        Interlocked.Exchange(ref interError, 0);
                        break;
                    }
                    Thread.Sleep(THREAD_SLEEP_DEFAULT);
                }
            }
            get
            {
                if (Interlocked.Exchange(ref interError, 1) == 0)
                {
                    string tmp = error;
                    Interlocked.Exchange(ref interError, 0);
                    return tmp;
                }
                return string.Empty;
            }
        }
        #endregion Properties

        #region Constructors
        public TaskBase()
        {

        }
        #endregion Constructors

        #region Method
        public void Start()
        {
            IsPlaying = true;
            ThreadStart threadStart = new ThreadStart(Run);
            Thread thread = new Thread(threadStart);
            thread.Start();
        }
        private void Run()
        {
            try
            {
                Action();
                IsSuccess = true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                IsSuccess = false;
            }
            IsPlaying = false;
        }
        protected abstract void Action();
        #endregion Method
    }
}
