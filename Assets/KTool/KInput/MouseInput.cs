using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.KInput
{
    public class MouseInput : MonoBehaviour
    {
        #region Properties
        [SerializeField]
        private LayerMask layerIgnore;
        [SerializeField]
        private float zoomScaleStandalone,
            zoomScaleMobie;

        private bool isInit;
        private Mouse mouse;

        public Mouse Mouse => mouse;
        #endregion Properties

        #region UnityEvent
        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            if (!isInit)
                return;
            mouse.Update();
        }
        #endregion UnityEvent

        #region Method
        public void Init()
        {
            isInit = true;
            mouse = CreateMouseNative();
        }
        #endregion Method

        #region Static Method
        public Mouse CreateMouseNative()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return new MouseStandalone(layerIgnore, zoomScaleStandalone);
#elif UNITY_IOS || UNITY_ANDROID
			return new MouseMobie(layerIgnore, zoomScaleMobie);
#endif
        }
        #endregion Static Method
    }
}
