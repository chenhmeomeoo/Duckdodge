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
    public void Die() // thực hiện player thua cuộc
    {
        GameManager.Instance.GameOver();
    }
    public void FootStepSound() // phát âm thanh di chuyển của player
    {
        SoundManager.Instance.PlaySFX(SoundTag.SFX_Main_Run);
    }
    public void DieSound() // phát âm thanh die của player
    {
        SoundManager.Instance.PlaySFX(SoundTag.SFX_Main_Die);
    }
}
