using Data;
using Engine.DI;
using System;
using UnityEngine;

namespace Engine.Coin
{
    public enum OperationType { Add, Minus }

    public struct ParametersUpdate
    {
        public int total;
        public int amount;
        public OperationType operation;
    }

    [Serializable]
    public struct Data
    {
        [Tooltip("Total saving coins in the game.")]
        public int totalCoins;
    }

    [CreateAssetMenu(fileName = "New Coins Data", menuName = "Add/Coins Data", order = 3)]
    public class CoinsData : ScriptableObject, IData, IAwake, ICoinsData, IDependency
    {
        private event Action<ParametersUpdate> _onUpdate;
        /// <summary>
        /// On add or minus coins. We will call this delegate.
        /// First parameter content the total and second content the amount adding.
        /// </summary>
        public event Action<ParametersUpdate> onUpdate
        {
            add
            {
                _onUpdate += value;
            }
            remove
            {
                _onUpdate -= value;
            }
        }

        [Header("Data")]
        [Tooltip("Currect data saving values.")]
        [SerializeField] private int _idData;
        [SerializeField] private Data _data;

        [Header("Settings")]
        [Tooltip("Initialize total coins on start the game first time.")]
        public int initCoins = 0;

        public int totalCoins => _data.totalCoins;

        public void Inject()
        {
            DIContainer.RegisterAsSingle<ICoinsData>(this);
        }

        public void Awake()
        {
            Initialize();
        }

        public string GetKey()
        {
            return "Coins." + _idData;
        }

        public void Initialize()
        {
            _data = ES3.Load(GetKey(), ObjectSaver.GetSavingPathFile<Data>(GetKey()), _data);
        }

        public void SaveData()
        {
            ES3.Save(GetKey(), _data, ObjectSaver.GetSavingPathFile<Data>(GetKey()));
        }

        public void ResetData(int idData)
        {
            _idData = idData;
            _data.totalCoins = initCoins;
        }

        public void AddCoins(int amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount can't be nigative...");

            /// Update data.
            _data.totalCoins += amount;

            /// Fill data delegate.
            ParametersUpdate dData = new ParametersUpdate();
            dData.total = _data.totalCoins;
            dData.amount = amount;
            dData.operation = OperationType.Add;

            SaveData();
            // Execute delegate.
            _onUpdate?.Invoke(dData);
        }

        public void RemoveCoins(int amount)
        {
            if (amount <= 0 || _data.totalCoins < amount) return;

            /// Update data.
            _data.totalCoins = Mathf.Clamp(_data.totalCoins - amount, 0, _data.totalCoins);

            /// Fill data delegate.
            ParametersUpdate dData = new ParametersUpdate();
            dData.total = _data.totalCoins;
            dData.amount = amount;
            dData.operation = OperationType.Minus;

            SaveData();
            // Execute delegate.
            _onUpdate?.Invoke(dData);
        }

    }
}