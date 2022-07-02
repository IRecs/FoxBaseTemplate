using Engine;
using Main;
using UnityEngine;
using Engine.DI;

namespace Examples
{
    public class AudioExample : MonoBehaviour, ILevelCompleted
    {
        private void OnEnable()
        {
            LevelStatueCompleted.Subscribe(this);
        }

        private void OnDisable()
        {
            LevelStatueCompleted.Unsubscribe(this);
        }

        public void LevelCompleted()
        {
            if (DIContainer.Bind<Engine.Senser.ISenser>().AsSingleton().isEnable)
                Handheld.Vibrate();
        }
    }
}