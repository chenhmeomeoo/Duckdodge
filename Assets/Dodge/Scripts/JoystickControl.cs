using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KTool.KInput;

public class JoystickControl : MonoBehaviour
{
    public static JoystickControl Instance;
    [SerializeField]
    private SpriteRenderer imgBonder,
        imgStick;
    [SerializeField]
    private float location;
    [SerializeField]
    private float range;

    public CamjoyStick camControl;
    private MouseInput mouseInput;

    public Vector2 Position
    {
        private set
        {
            transform.position = new Vector3(value.x, value.y, transform.position.z);
            imgBonder.transform.position = transform.position;
        }
        get
        {
            return transform.position;
        }
    }
    private Vector2 PositionStick
    {
        set
        {
            imgStick.transform.position = new Vector3(value.x, value.y, imgStick.transform.position.z);
        }
        get
        {
            return imgStick.transform.position;
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    private void Start()
    {
        Init(); 
        //Game_Start();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void Init()
    {
        //this.camControl = camControl;
        mouseInput = GetComponent<MouseInput>();
        mouseInput.Init();
        camControl.Init();
        Joystick_ToDefault();
        //
        mouseInput.Mouse.onDown += Mouse_OnDown;
        mouseInput.Mouse.onUp += Mouse_OnUp;
        mouseInput.Mouse.onMove += Mouse_OnMove;
        mouseInput.Mouse.onStand += Mouse_onStand;
    }

    private void Joystick_ToDefault()
    {
        float posX = camControl.Position.x,
            posY = camControl.Bot + location / camControl.PixelPerUnit;
        Position = new Vector2(posX, posY);
        //
        float bonderSizeX = imgBonder.sprite.rect.width / imgBonder.sprite.pixelsPerUnit,
            bonderSizeY = imgBonder.sprite.rect.height / imgBonder.sprite.pixelsPerUnit,
            scaleX = (range * 2) / bonderSizeX,
            scaleY = (range * 2) / bonderSizeY,
            scaleZ = imgBonder.transform.localScale.z;
        imgBonder.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }
    public void Game_Start()
    {
        Joystick_ToDefault();
        mouseInput.Mouse.IsActive = true;
        imgBonder.gameObject.SetActive(true);
        imgStick.gameObject.SetActive(true);
    }
    public void Game_End()
    {
        Joystick_ToDefault();
        mouseInput.Mouse.IsActive = false;
        imgBonder.gameObject.SetActive(false);
        imgStick.gameObject.SetActive(false);
    }

    private void Mouse_OnDown(Mouse mouse)
    {
        imgBonder.gameObject.SetActive(true);
        imgStick.gameObject.SetActive(true);
        Vector2 mousePosition = camControl.ScreenToWorld(mouse.Position);
        Position = mousePosition;
        PositionStick = mousePosition;
    }

    private void Mouse_OnUp(Mouse mouse)
    {
        imgBonder.gameObject.SetActive(false);
        imgStick.gameObject.SetActive(false);
        direct = Vector2.zero;
    }

    private void Mouse_OnMove(Mouse mouse, Vector2 delta)
    {
        Vector2 mousePosition = camControl.ScreenToWorld(mouse.Position);
        PositionStick = mousePosition;
        //
        float distance = Vector2.Distance(Position, mousePosition);
        if (distance > range)
        {
            float minX = camControl.Left + range,
                maxX = camControl.Right - range,
                minY = camControl.Bot + range,
                maxY = camControl.Top - range;
            float move = distance - range;
            Vector2 newPosition = Vector2.MoveTowards(Position, mousePosition, move);
            float posX = Mathf.Clamp(newPosition.x, minX, maxX),
                posY = Mathf.Clamp(newPosition.y, minY, maxY);
            Position = new Vector2(posX, posY);
        }
        //
        Update_Joystick();
    }


    private void Mouse_onStand(Mouse mouse)
    {
        Update_Joystick();
    }
    public Vector2 direct;
    private void Update_Joystick()
    {
        if (PositionStick == Position)
            return;
        direct = PositionStick - Position;
        direct =  direct.normalized;
        //dogNose.Line_NextPoint(direct.normalized);
    }
}
