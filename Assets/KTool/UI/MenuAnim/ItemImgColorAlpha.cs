using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace KTool.UI.MenuAnim
{
    [RequireComponent(typeof(Image))]
    public class ItemImgColorAlpha : Item
    {
        #region Properties

        [SerializeField]
        private bool isShow = true;
        [SerializeField]
        [Range(0, 1)]
        private float hideAlpha;
        [SerializeField]
        private float hideTime = 0.3f;
        [SerializeField]
        private Ease hideEase = Ease.InQuad;
        [SerializeField]
        [Range(0, 1)]
        private float showAlpha;
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
        public float Alpha
        {
            set
            {
                float alpha = Mathf.Clamp(value, 0, 1);
                Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, alpha);
            }
            get
            {
                return Image.color.a;
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
            Alpha = showAlpha;
            gameObject.SetActive(true);
        }

        public override void SetHide()
        {
            KillCurrentSequence();
            //
            isShow = false;
            isPlay = false;
            Alpha = hideAlpha;
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
            Color tagetColor = new Color(Image.color.r, Image.color.g, Image.color.b, showAlpha);
            currentSequence.Append(Image.DOColor(tagetColor, showTime).SetEase(showEase));
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
            Color tagetColor = new Color(Image.color.r, Image.color.g, Image.color.b, hideAlpha);
            currentSequence.Append(Image.DOColor(tagetColor, hideTime).SetEase(hideEase));
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
