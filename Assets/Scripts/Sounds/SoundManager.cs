using UnityEngine;



public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [Header("Background Music")]
    [SerializeField] private AudioClip[] _musicClips;
    [SerializeField] private AudioSource _musicSource;
    private AudioClip _randomClip;
    public bool isMusicOn = true;
    
    [Header("Sound Effects")]
    [SerializeField] private AudioSource[] _sfxSources;

    [SerializeField] private AudioSource[] _vocalClips;
    public bool isEffecsOn = true;

    public ButtonIconChangerManager musicIcon;
    public ButtonIconChangerManager effectsIcon;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _randomClip = ChoseRandomClipFNC(_musicClips);
        PlayBackgroundMusic(_randomClip);
    }
    
    public void PlayRandomVocalSoundFNC()
    {
        if (isEffecsOn)
        {
            AudioSource source = _vocalClips[Random.Range(0, _vocalClips.Length)];
            source.Stop();
            source.Play();
        }
    }

    public void PlaySoundEffectFNC(int soundIndis)
    {
        if (isEffecsOn && _sfxSources.Length > soundIndis)
        {
            _sfxSources[soundIndis].volume = PlayerPrefs.GetFloat("fxVolume");
            _sfxSources[soundIndis].Stop();
            _sfxSources[soundIndis].Play();
        }
    }

    AudioClip ChoseRandomClipFNC(AudioClip[] clips)
    {
        return _randomClip = clips[Random.Range(0, clips.Length)];
    }

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (!musicClip || !_musicSource || !isMusicOn)
            return;
        
        _musicSource.volume = PlayerPrefs.GetFloat("musicVolume");
        _musicSource.clip = musicClip;
        _musicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        if (!_musicSource || !isMusicOn)
            return;
        
        _musicSource.Stop();
    }
    
    void UpdateIsMusicOnFNC()
    {
        if (_musicSource.isPlaying != isMusicOn)
        {
            if (isMusicOn)
            {
                _randomClip = ChoseRandomClipFNC(_musicClips);
                PlayBackgroundMusic(_randomClip);
            }
            else
            {
                _musicSource.Stop();
            }
        }
    }
    
    public void MusicOnOffFNC()
    {
        isMusicOn = !isMusicOn;
        UpdateIsMusicOnFNC();
        musicIcon.IconOnOffFNC(isMusicOn);
    }
    
    public void EffectsOnOffFNC()
    {
        isEffecsOn = !isEffecsOn;
        effectsIcon.IconOnOffFNC(isEffecsOn);
    }
    
    
}
