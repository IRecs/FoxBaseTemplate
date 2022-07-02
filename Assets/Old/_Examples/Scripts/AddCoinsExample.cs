using Engine;
using Engine.Coin;
using Main;
using UnityEngine;

namespace Examples
{
    public class AddCoinsExample : MonoBehaviour, ILevelCompleted, ILevelFailed
    {
        public ICoinsData coinsData;
        public int coinsTests;

        void OnEnable()
        {
            coinsData = Engine.DI.DIContainer.GetAsSingle<ICoinsData>();

            LevelStatueCompleted.Subscribe(this);
            LevelStatueFailed.Subscribe(this);

            coinsData.onUpdate += OnCoinsUpdated;
        }

        void OnDisable()
        {
            LevelStatueCompleted.Unsubscribe(this);
            LevelStatueFailed.Unsubscribe(this);

            coinsData.onUpdate -= OnCoinsUpdated;
        }

        private void OnCoinsUpdated(ParametersUpdate obj)
        {
            Debug.Log("Coins is updated...");
        }

        [NaughtyAttributes.Button("On Level Failed")]
        public void LevelFailed()
        {
            coinsData.RemoveCoins(500);
            coinsTests -= 500;

            Debug.Log("TotalCoins: " + coinsData.totalCoins);
        }

        [NaughtyAttributes.Button("On Level Completed")]
        public void LevelCompleted()
        {
            coinsData.AddCoins(1000);
            coinsTests += 1000;

            Debug.Log("TotalCoins: " + coinsData.totalCoins);
        }
    }
}