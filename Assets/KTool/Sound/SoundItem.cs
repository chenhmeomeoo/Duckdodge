using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.Sound
{
    public class SoundItem : MonoBehaviour
    {
        [SerializeField]
        private SoundItemPooling soundItemPooling = null;

        private AudioSource audioSource = null;
        private bool isInit = false,
            isPlay = false,
            isComplete = false;
        private Action onComplete = null;

        private AudioSource AudioSource
        {
            get
            {
                if (audioSource == null)
                    audioSource = GetComponent<AudioSource>();
                return audioSource;
            }
        }
        public bool IsPlay => isPlay;
        public bool IsComplete => isComplete;
        public bool IsLoop
        {
            set
            {
                AudioSource.loop = value;
            }
            get
            {
                return AudioSource.loop;
            }
        }
        public bool Mute
        {
            set
            {
                AudioSource.mute = value;
            }
            get
            {
                return AudioSource.mute;
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isPlay)
            {
                if (isComplete || !AudioSource.isPlaying)
                {
                    isComplete = true;
                    isPlay = false;
                    onComplete?.Invoke();
                    soundItemPooling.DestroyItem(this);
                }
            }
        }
        public void Pause(bool isPause)
        {
            if (isComplete)
                return;
            if(isPause)
            {
                isPlay = false;
                AudioSource.Pause();
            }
            else
            {
                isPlay = true;
                AudioSource.UnPause();
            }
        }
        public void Stop()
        {
            if (isComplete)
                return;
            AudioSource.Stop();
            isComplete = true;
            isPlay = false;
            soundItemPooling.DestroyItem(this);
        }
        public void ToDefault()
        {
            isInit = false;
            isComplete = false;
            isPlay = false;
            IsLoop = false;
            onComplete = null;
            if (AudioSource != null)
            {
                AudioSource.Stop();
                AudioSource.clip = null;
            }
        }
        public void Init(AudioClip clip, float volume = 1, Action onComplete = null)
        {
            if (isInit)
                return;
            isInit = true;
            isPlay = true;
            this.onComplete = onComplete;
            //
            if (clip == null || AudioSource == null)
            {
                isComplete = true;
            }
            else
            {
                isComplete = false;
                AudioSource.volume = volume;
                AudioSource.clip = clip;
                AudioSource.Play();
            }
        }
    }
}
