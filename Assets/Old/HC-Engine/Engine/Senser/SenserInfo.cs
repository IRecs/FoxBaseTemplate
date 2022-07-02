using Data;
using System;
using UnityEngine;

namespace Engine.Senser
{
    [CreateAssetMenu(fileName = "New Senser", menuName = "Add/More/New Senser", order = 11)]
    public class SenserInfo : ScriptableObject, IData, IAwake, ISenser, DI.IDependency
    {
        [Serializable]
        public struct Data
        {
            [Tooltip("In true case the on senser.")]
            public bool isEnable;

            /// <summary>
            /// Restore the data to the default values.
            /// </summary>
            public void Reset()
            {
                isEnable = true;
            }
        }

        private event Action<bool> _onSwitch;
        public event Action<bool> onSwitch
        {
            add
            {
                _onSwitch += value;
            }
            remove
            {
                _onSwitch -= value;
            }
        }

        [SerializeField] private int _idData;
        [SerializeField] private Data _data;
        public bool isEnable => _data.isEnable;

        public void Inject()
        {
            DI.DIContainer.Register<ISenser>(this);
        }

        public void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            _data = ES3.Load(GetKey(), ObjectSaver.GetSavingPathFile<Data>(GetKey()), _data);
        }

        public void SaveData()
        {
            ES3.Save(GetKey(), _data, ObjectSaver.GetSavingPathFile<Data>(GetKey()));
        }

        public string GetKey()
        {
            return "Senser." + _idData;
        }

        public void ResetData(int idData)
        {
            _idData = idData;
            _data.Reset();
        }

        /// <summary>
        /// Switch the Senser enable if the Senser was false you can switch it to true and opposite.
        /// </summary>
        public void SwitchEnable()
        {
            _data.isEnable = !_data.isEnable;
            SaveData();
            _onSwitch?.Invoke(_data.isEnable);
        }

        public void SetEnable(bool enable)
        {
            if (enable != _data.isEnable)
            {
                _data.isEnable = enable;
                SaveData();
                _onSwitch?.Invoke(enable);
            }
        }

    }
}
