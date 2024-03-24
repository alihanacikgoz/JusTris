using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameDynamics
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Transform _mainMenuPannel, _settingsMenuPanel;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource[] _fxSources;
        [SerializeField] private Slider _musicSlider, _fxSlider;

        private void Start()
        {
            if (!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", .5f);
                _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            }
            else
            {
                _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            }
            _fxSlider.value = 1;
        }

        public void OpenSettingsMenuFNC()
        {
            SoundManager.instance.PlaySoundEffectFNC(0);
            _mainMenuPannel.GetComponent<RectTransform>().DOLocalMoveX(-1500, 0.5f);
            _settingsMenuPanel.GetComponent<RectTransform>().DOLocalMoveX(0, 0.5f);
        }

        public void CloseSettingsMenuFNC()
        {
            SoundManager.instance.PlaySoundEffectFNC(0);
            _mainMenuPannel.GetComponent<RectTransform>().DOLocalMoveX(0, 0.5f);
            _settingsMenuPanel.GetComponent<RectTransform>().DOLocalMoveX(1500, 0.5f);
        }

        public void PlayFNC()
        {
            SoundManager.instance.PlaySoundEffectFNC(0);
            SceneManager.LoadScene("GameScene");
        }

        public void ChangeMusicVolumeFNC()
        {
            _musicSource.volume = _musicSlider.value;
            PlayerPrefs.SetFloat("musicVolume", _musicSlider.value);
        }

        public void ChangeFxVolumeFNC()
        {
            PlayerPrefs.SetFloat("fxVolume", _fxSlider.value);
        }
    }
}