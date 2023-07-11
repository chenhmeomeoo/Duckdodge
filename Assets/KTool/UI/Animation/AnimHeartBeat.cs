using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KTool.UI.Animation
{
    public class AnimHeartBeat : MonoBehaviour
    {
        #region Properties
        [SerializeField]
        private float[] partScale = null;
        [SerializeField]
        private bool isDelay = false,
            isRandomDelay = false;
        [SerializeField]
        private float minDelay = 0,
            maxDelay = 0;
        [SerializeField]
        private bool isRandomVelocity = false;
        [SerializeField]
        private float minVelocity = 0,
            maxVelocity = 1;
        [SerializeField]
        private bool unScaleTime = false;

        private RectTransform rect = null;
        Vector3 defaultScale = Vector3.zero,
            tagetScale = Vector3.zero;
        private int partIndex = -1;
        private float delay = 0,
            velocity = 0;
        private float timeDelay = 0;

        #endregion Properties;

        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            Animation();
        }

        private void OnValidate()
        {
            if(isRandomVelocity)
            {
                minVelocity = Mathf.Max(0.01f, minVelocity);
                maxVelocity = Mathf.Max(minVelocity, maxVelocity);
            }
            else
            {
                minVelocity = Mathf.Max(0.01f, minVelocity);
                maxVelocity = minVelocity;
            }
            if(isDelay)
            {
                if(isRandomDelay)
                {
                    minDelay = Mathf.Max(0, minDelay);
                    maxDelay = Mathf.Max(minDelay, maxDelay);
                }
                else
                {
                    minDelay = Mathf.Max(0, minDelay);
                    maxDelay = minDelay;
                }
            }
            else
            {
                minDelay = maxDelay = 0;
            }
            
        }
        private void Init()
        {
            rect = GetComponent<RectTransform>();
            defaultScale = rect.localScale;
            RadnomDelay();
        }

        private void Animation()
        {
            float deltaTime = unScaleTime ? Time.unscaledDeltaTime : Time.deltaTime;
            if (partScale == null || partScale.Length <= 0)
                return;
            if (timeDelay < delay)
            {
                timeDelay += deltaTime;
                return;
            }
            //
            if (partIndex < 0 || rect.localScale == tagetScale)
            {

                if (isRandomVelocity)
                    velocity = Random.Range(minVelocity, maxVelocity);
                else
                    velocity = minVelocity;
                //
                if (partIndex < 0 || partIndex > partScale.Length)
                {
                    partIndex = 0;
                    tagetScale = new Vector3(partScale[partIndex], partScale[partIndex], 1);
                    RadnomDelay();
                    return;
                }
                else
                {
                    partIndex++;
                    if (partIndex < partScale.Length)
                    {
                        tagetScale = new Vector3(partScale[partIndex], partScale[partIndex], 1);
                    }
                    else
                    {
                        tagetScale = defaultScale;
                    }
                }
            }
            rect.localScale = Vector3.MoveTowards(rect.localScale, tagetScale, velocity * deltaTime);
        }

        private void RadnomDelay()
        {
            if(isDelay)
            {
                if (isRandomDelay)
                    delay = Random.Range(minDelay, maxDelay);
                else
                    delay = minDelay;
                timeDelay = 0;
            }
            else
            {
                delay = timeDelay = 0;
            }
        }
    }
}

