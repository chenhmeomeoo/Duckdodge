using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BecameVisible : MonoBehaviour
    {
        #region Properties
        [SerializeField]
        private bool isEnableRender = true,
            isCheckVisible = true;
        [SerializeField]
        private Vector2 arena = Vector2.one;

        private SpriteRenderer render = null;

        public bool IsEnableRender
        {
            set
            {
                isEnableRender = value;
                if (!isEnableRender)
                    Render.enabled = false;
            }
            get
            {
                return isEnableRender;
            }
        }
        public bool IsCheckVisible
        {
            set
            {
                isCheckVisible = value;
            }
            get
            {
                return isCheckVisible;
            }
        }
        private SpriteRenderer Render
        {
            get
            {
                if (render == null)
                    render = GetComponent<SpriteRenderer>();
                return render;
            }
        }
        private Vector2 Position => transform.position;
        #endregion Properties
        #region UnityEvent
        // Start is called before the first frame update
        void Start()
        {
            UpdateVisible();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateVisible();
        }

        private void OnValidate()
        {
            arena = new Vector2(Mathf.Max(0, arena.x), Mathf.Max(0, arena.y));
        }
        #endregion UnityEvent

        private void UpdateVisible()
        {
            if (!IsEnableRender || !isCheckVisible)
                return;
            bool isVisible = CheckVisible();
            if (Render.enabled != isVisible)
                Render.enabled = isVisible;
        }

        private bool CheckVisible()
        {
            if (Render.sprite == null)
                return false;
            if (Render.size.x == 0 || Render.size.y == 0)
                return false;
            Sprite sprite = Render.sprite;
            Vector2 size = Render.size,
                spriteSie = sprite.rect.size,
                spritePivot = sprite.pivot;
            if (spriteSie.x == 0 || spriteSie.y == 0)
                return false;
            float deltaWidth = size.x * spritePivot.x / spriteSie.x,
                deltaHeight = size.y * spritePivot.y / spriteSie.y;
            float bot = Position.y - deltaHeight,
                top = bot + size.y,
                left = Position.x - deltaWidth,
                right = left + size.x,
                camTop = CameraRect.Instance.Top + arena.y,
                camBot = CameraRect.Instance.Bot - arena.y,
                camLeft = CameraRect.Instance.Left - arena.x,
                camRight = CameraRect.Instance.Right + arena.x;
            if ((CheckClamp(top, camBot, camTop) || CheckClamp(bot, camBot, camTop) || CheckClamp(camBot, bot, top)) &&
                (CheckClamp(left, camLeft, camRight) || CheckClamp(right, camLeft, camRight) || CheckClamp(camLeft, left, right)))
            {
                return true;
            }
            return false;
        }

        private bool CheckClamp(float value, float min, float max)
        {
            return value >= min && value <= max;
        }
    }
}
