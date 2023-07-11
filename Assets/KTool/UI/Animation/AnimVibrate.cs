using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KTool.UI.Animation
{
    public class AnimVibrate : MonoBehaviour
    {
        #region Properties
        [SerializeField]
        private float angle = 10;
        [SerializeField]
        private bool isDelay = true,
            isRandomDelay = false;
        [SerializeField]
        private float minDelay = 0,
            maxDelay = 1;
        [SerializeField]
        private bool isRandomVelocity = false;
        [SerializeField]
        private float minVelocity = 0,
            maxVelocity = 1;
        [SerializeField]
        private bool unScaleTime = false;

        Quaternion defaultAngle = Quaternion.identity,
            tagetAngle = Quaternion.identity;
        private Quaternion[] partAngle = null;
        private int partIndex = -1;
        private float delay = 0,
            velocity = 0;
        private float timeDelay = 0;

        #endregion Properties
        // Start is called before the first frame update
        void Start()
        {
            defaultAngle = transform.localRotation;
            float tmpAngle = defaultAngle.eulerAngles.z;
            partAngle = new Quaternion[4] {
                Quaternion.Euler(defaultAngle.eulerAngles.x, defaultAngle.eulerAngles.y, tmpAngle + angle),
                Quaternion.Euler(defaultAngle.eulerAngles.x, defaultAngle.eulerAngles.y, tmpAngle - angle),
                Quaternion.Euler(defaultAngle.eulerAngles.x, defaultAngle.eulerAngles.y, tmpAngle + angle),
                Quaternion.Euler(defaultAngle.eulerAngles.x, defaultAngle.eulerAngles.y, tmpAngle - angle)
            };
            RadnomDelay();
        }

        // Update is called once per frame
        void Update()
        {
            Animation();
        }

        private void OnValidate()
        {
            minDelay = Mathf.Max(0, minDelay);
            if (isRandomDelay)
                maxDelay = Mathf.Max(minDelay, maxDelay);
            else
                maxDelay = minDelay;
            //
            minVelocity = Mathf.Max(0, minVelocity);
            if (isRandomVelocity)
                maxVelocity = Mathf.Max(minVelocity, maxVelocity);
            else
                maxVelocity = minVelocity;
        }

        private void Animation()
        {
            float deltaTime = unScaleTime ? Time.unscaledDeltaTime : Time.deltaTime;
            if (timeDelay < delay)
            {
                timeDelay += deltaTime;
                return;
            }
            //
            if (partIndex < 0 || transform.localRotation == tagetAngle)
            {

                if (isRandomVelocity)
                    velocity = Random.Range(minVelocity, maxVelocity);
                else
                    velocity = minVelocity;
                //
                if (partIndex < 0 || partIndex > partAngle.Length)
                {
                    partIndex = 0;
                    tagetAngle = partAngle[partIndex];
                    RadnomDelay();
                    return;
                }
                else
                {
                    partIndex++;
                    if (partIndex < partAngle.Length)
                    {
                        tagetAngle = partAngle[partIndex];
                    }
                    else
                    {
                        tagetAngle = defaultAngle;
                    }
                }
            }
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, tagetAngle, velocity * deltaTime);
        }

        private void RadnomDelay()
        {
            if (isDelay)
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
