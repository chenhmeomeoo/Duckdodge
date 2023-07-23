using QuangDM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Die()
    {
        GameManager.Instance.GameOver();
        Debug.LogError("die");
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
