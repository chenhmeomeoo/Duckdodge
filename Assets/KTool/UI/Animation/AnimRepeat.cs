using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.UI.Animation
{
    public class AnimRepeat : MonoBehaviour
    {
        [SerializeField]
        private Vector2 StartPoint = Vector2.zero,
            EndPoint = Vector2.zero;
        [SerializeField] [Min(0)]
        private float velocity = 1;
        [SerializeField]
        private bool isLoop = true;
        [SerializeField]
        private bool unScaleTime = false;

        private RectTransform rect = null;
        private bool toEnd = true;
        // Start is called before the first frame update
        void Start()
        {
            rect = GetComponent<RectTransform>();
            rect.anchoredPosition = StartPoint;
        }

        // Update is called once per frame
        void Update()
        {
            Animation();
        }
        private void OnValidate()
        {
            velocity = Mathf.Max(0, velocity);
        }

        private void Animation()
        {
            if (StartPoint == EndPoint || velocity <= 0)
                return;
            float deltaTime = unScaleTime ? Time.unscaledDeltaTime : Time.deltaTime;
            if(toEnd)
            {
                rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, EndPoint, velocity * deltaTime);
                if(rect.anchoredPosition == EndPoint)
                {
                    if(isLoop)
                        toEnd = false;
                    else
                        rect.anchoredPosition = StartPoint;
                }
            }
            else
            {
                rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, StartPoint, velocity * deltaTime);
                if (rect.anchoredPosition == StartPoint)
                {
                    toEnd = true;
                }
            }
        }
    }
}
