using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KTool.UI.MenuAnim
{
    [Serializable]
    public class DataItem
    {
        [SerializeField]
        private Item item = null;
        [SerializeField]
        private float delayShow = 0,
            delayHide = 0;

        public Item Item => item;
        public float DelayShow => delayShow;
        public float DelayHide => delayHide;
        public bool IsPlay => item.IsPlay;

        public void SetShow()
        {
            item.SetShow();
        }
        public void SetHide()
        {
            item.SetHide();
        }
        public void PlayShow(bool unScaleTime)
        {
            item.PlayShow(DelayShow, unScaleTime);
        }
        public void PlayHide(bool unScaleTime)
        {
            item.PlayHide(DelayHide, unScaleTime);
        }
    }
}
