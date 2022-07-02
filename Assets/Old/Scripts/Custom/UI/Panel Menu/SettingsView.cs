using Engine.Senser;
using UnityEngine;
using UnityEngine.UI;

namespace Main.UI
{
    public class SettingsView : MonoBehaviour, IPanel
    {
        [Header("Panels")]
        [SerializeField] private GameObject _settingPanel;
        [SerializeField] private GameObject _settingHead;

        [Header("Audio")]
        [SerializeField] private Button  _audioOn;
        [SerializeField] private Button _audioOff;

        [Header("Vibration")]
        [SerializeField] private Button _vibrateOn;
        [SerializeField] private Button _vibrateOff;

        [SerializeField] private SenserInfo _audioInfo;
        [SerializeField] private SenserInfo _vibrationInfo;

        public bool isShowed { get; private set; }
        public bool isVisible { get; private set; }

        protected void Start()
        {
            _audioOn.onClick.AddListener(() => SwitchAudio());
            _audioOff.onClick.AddListener(() => SwitchAudio());

            _vibrateOn.onClick.AddListener(() => SwitchVibrate());
            _vibrateOff.onClick.AddListener(() => SwitchVibrate());
        }

        public void SwitchPanel()
        {
            if (!isShowed)
                Show();
            else
                Hide();
        }

        public void Show()
        {
            isVisible = true;
            isShowed = true;
            _settingHead.SetActive(!isShowed);
            _settingPanel.SetActive(isShowed);

            OnSwitchedAudio(_audioInfo.isEnable);
            OnSwitchedVibrate(_vibrationInfo.isEnable);
        }

        public void Hide()
        {
            isVisible = false;
            isShowed = false;
            _settingPanel.SetActive(isShowed);
            _settingHead.SetActive(!isShowed);
        }

        public void SwitchAudio()
        {
            _audioInfo.SwitchEnable();
            OnSwitchedAudio(_audioInfo.isEnable);
        }

        public void SwitchVibrate()
        {
            _vibrationInfo.SwitchEnable();
            OnSwitchedVibrate(_vibrationInfo.isEnable);
        }

        public void OnSwitchedAudio(bool enable)
        {
            _audioOn.gameObject.SetActive(enable);
            _audioOff.gameObject.SetActive(!enable);
        }

        public void OnSwitchedVibrate(bool enable)
        {
            _vibrateOn.gameObject.SetActive(enable);
            _vibrateOff.gameObject.SetActive(!enable);
        }
    }
}
