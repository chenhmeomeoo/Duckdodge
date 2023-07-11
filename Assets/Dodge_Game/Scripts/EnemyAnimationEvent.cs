using QuangDM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAnimationEvent : MonoBehaviour
{
    private int Score;
    public void Attack()
    {
        SoundManager.Instance.PlaySFX(SoundTag.SFX_Enemy_Attack);
        SoundManager.Instance.StopCurrentBG();
        PlayerController.Instance.anim.SetTrigger("die");
        PlayerController.Instance.isLive = false;
        PlayerController.Instance.GetComponent<Collider>().enabled = false;
        GameManager.Instance.isGameRuning = false;
    }
    public void Die()
    {
        EnemyController enemy = GetComponentInParent<EnemyController>();
        Score = enemy.Score;
        enemy.gameObject.SetActive(false);
        GameManager.Instance.EnemyDie(Score);
    }
    public void FootStepSound()
    {
        SoundManager.Instance.PlaySFX(SoundTag.SFX_Enemy_Run);
    }
}
