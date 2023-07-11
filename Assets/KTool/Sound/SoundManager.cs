using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.Sound
{
    public class SoundManager : MonoBehaviour
    {
        #region Properties
        private static SoundManager instance = null;
        public static SoundManager Instance => instance;

        [SerializeField]
        private SoundItemPooling soundItemPooling = null;

        private AudioSource musicSource = null;
        private AudioClip currentMusic;
        private float currentVolume;

        private AudioSource BackgroundAudio
        {
            get
            {
                if (musicSource == null)
                    musicSource = GetComponent<AudioSource>();
                return musicSource;
            }
        }
        public AudioClip ClipMusic
        {
            private set
            {
                BackgroundAudio.clip = value;
            }
            get
            {
                return BackgroundAudio.clip;
            }
        }
        public bool IsMusicPlay => BackgroundAudio.isPlaying;
        public bool isMusicLoop
        {
            set
            {
                BackgroundAudio.loop = value;
            }
            get
            {
                return BackgroundAudio.loop;
            }
        }
        public float MusicVolume
        {
            set
            {
                BackgroundAudio.volume = Mathf.Clamp(value, 0, 1);
                currentVolume = BackgroundAudio.volume;
            }
            get
            {
                return BackgroundAudio.volume;
            }
        }
        public bool MusicMute
        {
            set
            {
                BackgroundAudio.mute = value;
            }
            get
            {
                return BackgroundAudio.mute;
            }
        }
        public bool SoundMute
        {
            set
            {
                soundItemPooling.Mute = value;
            }
            get
            {
                return soundItemPooling.Mute;
            }
        }
        #endregion Properties

        #region UnityEvent
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance.GetInstanceID() != GetInstanceID())
            {
                Destroy(gameObject);
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            if (instance != null && instance.GetInstanceID() == GetInstanceID())
                instance = null;
        }
        #endregion UnityEvent

        #region Background

        public void Background_Play(AudioClip clip)
        {
            currentMusic = clip;
            ClipMusic = clip;
            BackgroundAudio.Play();
        }
        public void Background_StarSub(AudioClip clip, float volume)
        {
            ClipMusic = clip;
            BackgroundAudio.volume = Mathf.Clamp(volume, 0, 1);
            BackgroundAudio.Play();
        }
        public void Background_EndSub()
        {
            ClipMusic = currentMusic;
            MusicVolume = currentVolume;
            BackgroundAudio.Play();
        }

        public void Background_Stop()
        {
            if (!IsMusicPlay)
                return;
            BackgroundAudio.Stop();
        }
        public void Background_Pause()
        {
            if (!IsMusicPlay)
                return;
            BackgroundAudio.Pause();
        }
        public void Background_Unpause()
        {
            if (IsMusicPlay)
                return;
            BackgroundAudio.UnPause();
        }
        #endregion Background

        public SoundItem PlaySound(AudioClip clip, float volume = 1, Action onComplete = null)
        {
            if(clip == null)
            {
                onComplete?.Invoke();
                return null;
            }
            //
            SoundItem newItem = soundItemPooling.CreateItem();
            newItem.Init(clip, volume, onComplete);
            return newItem;
        }
    }
}
