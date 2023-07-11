using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuangDM.Common
{

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        public List<Sound> BG_Sound, Sfx_Sound;
        public AudioSource BG_Source;
        public List<AudioSource> Sfx_Source;

        private void Awake()
        {
            Instance = this;
        }
        public void PlayBG(SoundTag soundTag)
        {
            //if (!SettingData.Instance.Music)
            //{
            //    BG_Source.Stop();
            //    return;
            //}
            foreach (Sound s in BG_Sound)
            {
                if (s.tag == soundTag)
                {
                    BG_Source.volume = s.volume;
                    BG_Source.clip = s.clip;
                    BG_Source.Play();
                    break;
                }
            }
        }
        public void PlaySFX(SoundTag soundTag)
        {
            //if (!SettingData.Instance.Sound)
            //{
            //    foreach(AudioSource audio in Sfx_Source) 
            //        audio.Stop();
            //    return;
            //}
            //foreach (Sound s in Sfx_Sound)
            //{
            //    if(s.tag == SoundTag.SFX_Button)
            //    {
            //        Button_Source.volume = s.volume;
            //        Button_Source.clip = s.clip;
            //        Button_Source.Play();
            //        return;
            //    }
            //    if (s.tag == soundTag)
            //    {
            //        foreach (AudioSource audio in Sfx_Source)
            //        {
            //            if (audio.isPlaying)
            //                continue;
            //            else
            //            {
            //                audio.volume = s.volume;
            //                audio.clip = s.clip;
            //                audio.Play();
            //                return;
            //            }
            //        }
            //    }
            //}
            for (int i = 0; i < Sfx_Sound.Count; i++)
            {
                if (Sfx_Sound[i].tag == soundTag)
                {
                    for (int j = 0; j < Sfx_Source.Count; j++)
                    {
                        if (Sfx_Source[j].isPlaying && j != Sfx_Source.Count - 1)
                            continue;
                        else if(Sfx_Source[j].isPlaying && j == Sfx_Source.Count - 1)
                        {
                            GameObject SFX_Source = new GameObject();
                            SFX_Source.transform.parent = this.transform;

                            AudioSource new_source = SFX_Source.AddComponent<AudioSource>();
                            new_source.volume = Sfx_Sound[i].volume;
                            new_source.clip = Sfx_Sound[i].clip;
                            new_source.Play();
                            Sfx_Source.Add(new_source);
                            break;
                        }
                        else
                        {
                            Sfx_Source[j].volume = Sfx_Sound[i].volume;
                            Sfx_Source[j].clip = Sfx_Sound[i].clip;
                            Sfx_Source[j].Play();
                            return;
                        }
                    }
                }
            }
        }
        public void StopCurrentBG()
        {
            BG_Source.Pause();
        }
        public void ContinueCurrentBG()
        {
            BG_Source.UnPause();
        }
        public void StopCurrentSFX()
        {
            foreach (AudioSource audio in Sfx_Source)
                audio.Stop();
        }
    }
}
