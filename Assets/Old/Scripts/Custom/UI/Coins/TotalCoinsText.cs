using Engine.Normal;
using Engine.Coin;
using TMPro;
using UnityEngine;

namespace Main.UI.Coin
{
    public class TotalCoinsText : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] protected TextMeshProUGUI _textTotalCoins;

        protected ICoinsData m_CoinsInfo;

        private void Awake()
        {
            m_CoinsInfo = Engine.DI.DIContainer.GetAsSingle<ICoinsData>();
        }

        protected virtual void OnEnable()
        {
            _textTotalCoins.text = Normalization.NormalizeScore(m_CoinsInfo.totalCoins);
            m_CoinsInfo.onUpdate += OnUpdateCoins;
        }

        protected virtual void OnDisable()
        {
            m_CoinsInfo.onUpdate -= OnUpdateCoins;
        }

        protected virtual void OnUpdateCoins(ParametersUpdate data)
        {
            _textTotalCoins.text = data.total.ToString();
        }
    }
}