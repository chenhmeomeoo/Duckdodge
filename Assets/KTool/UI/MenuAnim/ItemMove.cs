using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace KTool.UI.MenuAnim
{
    [RequireComponent(typeof(RectTransform))]
    public class ItemMove : Item
    {
        #region Properties
        [SerializeField]
        private bool isShow = true;
        [SerializeField]
        private Vector2 hidePos = Vector2.zero;
        [SerializeField]
        private float hideTime = 0.3f;
        [SerializeField]
        private Ease hideEase = Ease.InQuad;
        [SerializeField]
        private Vector2 showPos = Vector2.zero;
        [SerializeField]
        private float showTime = 0.3f;
        [SerializeField]
        private Ease showEase = Ease.OutQuad;

        private RectTransform rectTF = null;
        private bool isPlay = false;
        private Sequence currentSequence;

        private RectTransform RectTF
        {
            get
            {
                if (rectTF == null)
                    rectTF = GetComponent<RectTransform>();
                return rectTF;
            }
        }
        public override bool IsShow => isShow;
        public override bool IsPlay => isPlay;

        public Vector2 HidePos
        {
            set
            {
                hidePos = value;
            }
            get
            {
                return hidePos;
            }
        }
        public Vector2 ShowPos
        {
            set
            {
                showPos = value;
            }
            get
            {
                return showPos;
            }
        }
        #endregion Properties

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void SetShow()
        {
            KillCurrentSequence();
            //
            isShow = true;
            isPlay = false;
            RectTF.anchoredPosition = showPos;
            gameObject.SetActive(true);

        }
        public override void SetHide()
        {
            KillCurrentSequence();
            //
            isShow = false;
            isPlay = false;
            RectTF.anchoredPosition = hidePos;
            gameObject.SetActive(false);

        }
        public override void PlayShow(float delay, bool unScaleTime)
        {
            KillCurrentSequence();
            isShow = true;
            gameObject.SetActive(true);
            //
            isPlay = true;
            currentSequence = DOTween.Sequence();
            if (unScaleTime)
                currentSequence.SetUpdate(true);
            if (delay > 0)
                currentSequence.AppendInterval(delay);
            currentSequence.Append(RectTF.DOAnchorPos(showPos, showTime).SetEase(showEase));
            currentSequence.onComplete += (() =>
            {
                currentSequence = null;
                isPlay = false;
            });
        }

        public override void PlayHide(float delay, bool unScaleTime)
        {
            KillCurrentSequence();
            isShow = false;
            //
            isPlay = true;
            currentSequence = DOTween.Sequence();
            if (unScaleTime)
                currentSequence.SetUpdate(true);
            if (delay > 0)
                currentSequence.AppendInterval(delay);
            currentSequence.Append(RectTF.DOAnchorPos(hidePos, hideTime).SetEase(hideEase));
            currentSequence.onComplete += (() =>
            {
                currentSequence = null;
                gameObject.SetActive(false);
                isPlay = false;
            });
        }
        private void KillCurrentSequence()
        {
            if (currentSequence == null)
                return;
            if (currentSequence.IsPlaying())
                currentSequence.Kill(false);
            currentSequence = null;
        }
    }
}
