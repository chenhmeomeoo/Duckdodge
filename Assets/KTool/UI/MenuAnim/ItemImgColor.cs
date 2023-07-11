using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace KTool.UI.MenuAnim
{
    [RequireComponent(typeof(Image))]
    public class ItemImgColor : Item
    {
        #region Properties

        [SerializeField]
        private bool isShow = true;
        [SerializeField]
        [Range(0, 1)]
        private Color hideColor;
        [SerializeField]
        private float hideTime = 0.3f;
        [SerializeField]
        private Ease hideEase = Ease.InQuad;
        [SerializeField]
        [Range(0, 1)]
        private Color showColor;
        [SerializeField]
        private float showTime = 0.3f;
        [SerializeField]
        private Ease showEase = Ease.OutQuad;


        private Image image = null;
        private bool isPlay = false;
        private Sequence currentSequence;


        private Image Image
        {
            get
            {
                if (image == null)
                    image = GetComponent<Image>();
                return image;
            }
        }
        public Color Color
        {
            set
            {
                Image.color = value;
            }
            get
            {
                return Image.color;
            }
        }
        public override bool IsShow => isShow;
        public override bool IsPlay => isPlay;
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
        public void Init()
        {

        }

        public override void SetShow()
        {
            KillCurrentSequence();
            //
            isShow = true;
            isPlay = false;
            Color = showColor;
            gameObject.SetActive(true);
        }

        public override void SetHide()
        {
            KillCurrentSequence();
            //
            isShow = false;
            isPlay = false;
            Color = hideColor;
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
            currentSequence.Append(Image.DOColor(showColor, showTime).SetEase(showEase));
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
            currentSequence.Append(Image.DOColor(hideColor, hideTime).SetEase(hideEase));
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
        #endregion Method
    }
}
