using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool
{
    public class CameraRect : MonoBehaviour
    {
        #region Properties
        private static CameraRect instance = null;
        public static CameraRect Instance => instance;

#if UNITY_EDITOR
        [SerializeField]
        private bool isDrawCam = false;
#endif

        private Camera cam = null;

        public Camera Cam
        {
            get
            {
                if (cam == null)
                    cam = GetComponent<Camera>();
                return cam;
            }
        }
        public float OrthographicSize => Cam == null ? 0 : Cam.orthographicSize;
        public float Height => OrthographicSize * 2;
        public float Width => Cam.pixelWidth / (Cam.pixelHeight / Height);

        public float Top => Position.y + Height / 2;
        public float Bot => Position.y - Height / 2;
        public float Left => Position.x - Width / 2;
        public float Right => Position.x + Width / 2;

        public Vector2 TopLeft => new Vector2(Position.x - Width / 2, Position.y + Height / 2);
        public Vector2 TopRight => new Vector2(Position.x + Width / 2, Position.y + Height / 2);
        public Vector2 BotLeft => new Vector2(Position.x - Width / 2, Position.y - Height / 2);
        public Vector2 BotRight => new Vector2(Position.x + Width / 2, Position.y - Height / 2);
        public Vector2 Position => transform.position;
        #endregion Properties


        private void Awake()
        {
            instance = this;
        }
        private void OnDestroy()
        {
            instance = null;
        }
        // Start is called before the first frame update
        void Start()
        {
        }
        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR
            if (isDrawCam)
                DrawCam();
#endif
        }

#if UNITY_EDITOR
        private void DrawCam()
        {
            Debug.DrawLine(TopLeft, TopRight, Color.red);
            Debug.DrawLine(TopRight, BotRight, Color.red);
            Debug.DrawLine(BotRight, BotLeft, Color.red);
            Debug.DrawLine(BotLeft, TopLeft, Color.red);
        }
#endif

    }
}
