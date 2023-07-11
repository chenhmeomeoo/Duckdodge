using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamjoyStick : MonoBehaviour
{
    #region Properties
    private Camera cam;
    private float width,
        height,
        pixelPerUnit;
    private float left,
        right,
        top,
        bot;

    public float Ortho
    {
        set
        {
            cam.orthographicSize = value;
            Cacl();
        }
        get
        {
            return cam.orthographicSize;
        }
    }
    public float Aspect => cam.aspect;
    public float PixelWidth => cam.pixelWidth;
    public float PixelHeight => cam.pixelHeight;
    public Vector2 Position
    {
        set
        {
            transform.position = new Vector3(value.x, value.y, transform.position.z);
            Cacl_Pos();
        }
        get
        {
            return transform.position;
        }
    }
    public float Width => width;
    public float Height => height;
    public float PixelPerUnit => pixelPerUnit;
    public float Left => left;
    public float Right => right;
    public float Top => top;
    public float Bot => bot;
    #endregion Properties

    #region UnityEvent
    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {

    }
    #endregion UnityEvent

    #region Method
    public void Init()
    {
        cam = GetComponent<Camera>();
        FixScreen();
        Cacl();
    }
    public void GameStart()
    {

    }
    private void FixScreen()
    {
        float defaultAspect = 9 / 16f,
            defaultOrtho = 5,
            currentAspect = cam.aspect;
        if (currentAspect < defaultAspect)
            Ortho = defaultOrtho * (defaultAspect / currentAspect);
    }

    public void Cacl()
    {
        height = Ortho * 2;
        width = height * Aspect;
        pixelPerUnit = PixelHeight / height;
        Cacl_Pos();
    }
    private void Cacl_Pos()
    {
        Vector2 position = Position;
        left = position.x - width / 2;
        right = position.x + width / 2;
        bot = position.y - height / 2;
        top = position.y + height / 2;
    }

    public Vector2 ScreenToWorld(Vector2 point)
    {
        return cam.ScreenToWorldPoint(point);
    }
    public Vector2 WorldToScreen(Vector2 world)
    {
        return cam.WorldToScreenPoint(world);
    }
    #endregion Method

    #region Game

    #endregion
}

