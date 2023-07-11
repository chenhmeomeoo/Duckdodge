using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.UI.MenuAnim
{
    public abstract class Item : MonoBehaviour
    {
        #region Properties
        public abstract bool IsShow
        {
            get;
        }
        public abstract bool IsPlay
        {
            get;
        }
        #endregion Properties

        public abstract void SetShow();
        public abstract void SetHide();
        public abstract void PlayShow(float delay, bool unScaleTime);
        public abstract void PlayHide(float delay, bool unScaleTime);
    }
}