using DG.Tweening;
using QuangDM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("----------Value Player----------------")]
    public float steeringPower = 180f;
    public float Speed = 5f;
    private Vector3 smoothMovement;
    public Vector3 basePos;
    public bool isLive;

    [Header("----------Connect----------------")]
    private Gamepad gamepad;
    private Transform CameraTrans;
    public Animator anim;
    public Joystick joystick;
    private void Awake()
    {
        Instance = this;
        CameraTrans = Camera.main.gameObject.transform;
        //anim = GetComponent<Animator>();
    }
    private void Start()
    {
        basePos = transform.position;
        isLive = true;
    }
    private void Update()
    {
        if (GameManager.Instance.isGameRuning && isLive)
        {
            float x = joystick.Direction.x;
            float y = joystick.Direction.y;

            Vector3 camPosition = new Vector3(CameraTrans.position.x, transform.position.y, CameraTrans.position.z);
            Vector3 direction = (transform.position - camPosition).normalized;

            Vector3 forwardMovement = direction * y;
            Vector3 horizontalMovement = CameraTrans.right * x;
            Vector3 movement = Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1f);

            //smooth rate
            smoothMovement = Vector3.Lerp(transform.forward, movement, 0.05f);

            if (y > -0.0001f && y < 0.0001f && x > -0.0001f && x < 0.0001f)
            {
                transform.Translate(transform.forward * Speed * Time.deltaTime, Space.World);
            }
            else
            {
                transform.Translate(smoothMovement * Speed * Time.deltaTime, Space.World);
                transform.forward = smoothMovement;
            }

            if (transform.position.z >= GameManager.Instance.currentMap.TopLimit.position.z ||
               transform.position.z <= GameManager.Instance.currentMap.BotLimit.position.z ||
               transform.position.x >= GameManager.Instance.currentMap.RightLimit.position.x ||
               transform.position.x <= GameManager.Instance.currentMap.LeftLimit.position.x)
            {
                transform.DOLookAt(new Vector3(0,transform.position.y,0), 0.3f);
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            PowerItem item = other.gameObject.GetComponent<PowerItem>();
            item.GetItem();
        }
    }
    public void Die()
    {
        GameManager.Instance.GameOver();
    }
    public void FootStepSound()
    {
        SoundManager.Instance.PlaySFX(SoundTag.SFX_Main_Run);
    }
    public void DieSound()
    {
        SoundManager.Instance.PlaySFX(SoundTag.SFX_Main_Die);
    }
}
