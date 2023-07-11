using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool
{
    public class Resoult<T> : Resoult
    {
        #region Properties
        private T data;

        public T Data => data;
        #endregion Properties

        #region Method

        private Resoult(T data, object state) : base(state)
        {
            this.data = data;
        }
        private Resoult(string message, object state) : base(message, state)
        {

        }

        public static Resoult<T> CreateSuccess(T data, object state)
        {
            return new Resoult<T>(data, state);
        }
        public new static Resoult<T> CreateFail(string message, object state)
        {
            return new Resoult<T>(message, state);
        }
        #endregion Method
    }
}
