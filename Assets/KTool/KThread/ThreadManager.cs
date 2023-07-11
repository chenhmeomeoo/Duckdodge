using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

namespace KTool.KThread
{
    public class ThreadManager : MonoBehaviour
    {
        #region Properties
        private const string NAME_THREAD_MANAGER = "Thread_Manager",
            THREAD_CANCELED = "Thread Canceled";

        private static ThreadManager instance;
        public static ThreadManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject newObject = new GameObject(NAME_THREAD_MANAGER);
                    DontDestroyOnLoad(newObject);
                    instance = newObject.AddComponent<ThreadManager>();
                    instance.Init();
                }
                return instance;
            }
        }
        #endregion Properties

        #region UnityEvent
        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }
        #endregion UnityEvent

        #region Method
        private void Init()
        {
        }

        public void RunTask(Action func, object state = null, Action<Resoult> onComplete = null)
        {
            Task task = Task.Run(func);
            StartCoroutine(IE_Task(task, state, onComplete));
        }
        public void RunTask<T>(Func<T> func, object state = null, Action<Resoult<T>> onComplete = null)
        {
            Task<T> task = Task.Run(func);
            StartCoroutine(IE_Task<T>(task, state, onComplete));
        }
        private IEnumerator IE_Task(Task task, object state = null, Action<Resoult> onComplete = null)
        {
            Resoult resoult;
            while (true)
            {
                if (task.IsCanceled)
                {
                    resoult = Resoult.CreateFail(THREAD_CANCELED, state);
                    break;
                }
                else if (task.IsFaulted)
                {
                    resoult = Resoult.CreateFail(task.Exception.Message, state);
                    break;
                }
                else if(task.IsCompleted)
                {
                    resoult = Resoult.CreateSuccess(state);
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
            //
            onComplete?.Invoke(resoult);
        }
        private IEnumerator IE_Task<T>(Task<T> task, object state = null, Action<Resoult<T>> onComplete = null)
        {
            Resoult<T> resoult;
            while (true)
            {
                if (task.IsCanceled)
                {
                    resoult = Resoult<T>.CreateFail(THREAD_CANCELED, state);
                    break;
                }
                else if (task.IsFaulted)
                {
                    resoult = Resoult<T>.CreateFail(task.Exception.Message, state);
                    break;
                }
                else if (task.IsCompleted)
                {
                    resoult = Resoult<T>.CreateSuccess(task.Result, state);
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
            //
            onComplete?.Invoke(resoult);
        }
        #endregion Method
    }
}
