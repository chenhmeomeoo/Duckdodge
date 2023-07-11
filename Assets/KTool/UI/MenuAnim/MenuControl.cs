using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.UI.MenuAnim
{
    public class MenuControl : MonoBehaviour
    {
        #region Properties
        [SerializeField]
        private bool isShow = false,
            unScaleTime = false;
        [SerializeField]
        private DataItem[] items = null;

        private bool isPlay;

        public bool IsShow => isShow;
        public bool IsPlay => isPlay;
        #endregion Properties
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnValidate()
        {
            if (isShow)
                SetShow();
            else
                SetHide();
        }

        public void SetShow()
        {
            isShow = true;
            isPlay = false;
            StopAllCoroutines();
            for (int i = 0; items != null && i < items.Length; i++)
                if (items[i] != null)
                    items[i].SetShow();
        }
        public void SetHide()
        {
            isShow = false;
            isPlay = false;
            StopAllCoroutines();
            for (int i = 0; items != null && i < items.Length; i++)
                if (items[i] != null)
                    items[i].SetHide();
        }

        public void PlayShow(float delay = 0, Action onComplete = null)
        {
            if (IsShow)
                return;
            StopAllCoroutines();
            StartCoroutine(IE_Play(true, delay, onComplete));
        }

        public void PlayHide(float delay = 0, Action onComplete = null)
        {
            if (!IsShow)
                return;
            StopAllCoroutines();
            StartCoroutine(IE_Play(false, delay, onComplete));
        }


        private IEnumerator IE_Play(bool state, float delay = 0, Action onComplete = null)
        {
            isShow = state;
            isPlay = true;
            //
            if (delay > 0)
            {
                if (unScaleTime)
                    yield return new WaitForSecondsRealtime(delay);
                else
                    yield return new WaitForSeconds(delay);
            }
            //
            for (int i = 0; items != null && i < items.Length; i++)
            {
                if (state)
                    items[i].PlayShow(unScaleTime);
                else
                    items[i].PlayHide(unScaleTime);
            }
            //
            while (true)
            {
                bool isBreak = true;
                for (int i = 0; items != null && i < items.Length; i++)
                {
                    if (items[i].IsPlay)
                    {
                        isBreak = false;
                        break;
                    }
                }
                if (isBreak)
                    break;
                yield return new WaitForEndOfFrame();
            }
            //
            isPlay = false;
            onComplete?.Invoke();
        }
    }
}
