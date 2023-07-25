using DG.Tweening;
using QuangDM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float steeringPower = 3f;

    public float SpeedMultiplier = 20f;

    public int Score = 1;

    private Vector3 movement;

    public bool isLive;
    public bool isAttack;
    public float lifeDuration = 20;
    public float timeLifeDuration;

    public Animator anim;

    private void Awake()
    {
       
        isLive = true;
        isAttack = false;
    }
    private void Update()
    {

        if (GameManager.Instance.isGameRuning)
        {
            if (isLive && !isAttack)
            {
                transform.Translate(transform.forward * SpeedMultiplier * Time.deltaTime, Space.World);

                Vector3 target = PlayerController.Instance.transform.position - base.transform.position;
                float maxRadiansDelta = this.steeringPower * Time.deltaTime;
                Vector3 forward = Vector3.RotateTowards(base.transform.forward, target, maxRadiansDelta, 0f);
                forward.y = 0;
                base.transform.rotation = Quaternion.LookRotation(forward);


                if (transform.position.z >= GameManager.Instance.currentMap.TopLimit.position.z ||
                   transform.position.z <= GameManager.Instance.currentMap.BotLimit.position.z ||
                   transform.position.x >= GameManager.Instance.currentMap.RightLimit.position.x ||
                   transform.position.x <= GameManager.Instance.currentMap.LeftLimit.position.x)
                {
                    transform.DOLookAt(Vector3.zero, 0.3f);
                }
                timeLifeDuration += Time.deltaTime;
                if(timeLifeDuration>=lifeDuration)
                {
                    if (isLive)
                    {
                        isLive = false;
                        GetComponent<BoxCollider>().enabled = false;
                        SoundManager.Instance.PlaySFX(SoundTag.SFX_Enemy_Destroyed);
                        anim.SetTrigger("die");
                    }
                }
            }
            else if (isAttack)
            {
                transform.DOLookAt(PlayerController.Instance.transform.position, 0.2f);
            }
        }
    }
    private void OnEnable()
    {
        GetComponent<BoxCollider>().enabled = true;
        isLive = true;
        isAttack = false;
        timeLifeDuration = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            if (isLive)
            {
                isLive = false;
                GetComponent<BoxCollider>().enabled = false;
                SoundManager.Instance.PlaySFX(SoundTag.SFX_Enemy_Destroyed);
                anim.SetTrigger("die");
            }
        }
        else if (other.gameObject.tag == "Player")
        {
            if (!isAttack)
            {
                CameraController.Instance.ZoomIn();
                isAttack = true;
                anim.SetTrigger("attack");
                Debug.LogError("attack");
            }
        }
    }

}
