using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuangDM.Common
{
    public enum SoundTag
    {
        Bgm_home,
        Bgm_ingame,
        SFX_Button,
        SFX_Enemy_Attack,
        SFX_Enemy_Destroyed,
        SFX_Enemy_Run,
        SFX_Enemy_Spawn,
        SFX_Lose_Screen,
        SFX_Main_Collect,
        SFX_Main_Run,
        SFX_Main_Die
    }

    [System.Serializable]
    public class Sound
    {
        public SoundTag tag;
        public AudioClip clip;
        public float volume;
    }
}