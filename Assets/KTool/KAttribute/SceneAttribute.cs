using System;
using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;

namespace KTool.KAttribute
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class SceneAttribute : PropertyAttribute
    {
        #region Properties

        #endregion Properties

        #region Constructor
        public SceneAttribute() : base()
        {

        }
        #endregion Constructor

        #region Method

        #endregion Method
    }
}
