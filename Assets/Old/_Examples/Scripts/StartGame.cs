using UnityEngine;

namespace Examples
{
    public class StartGame : MonoBehaviour
    {
        public void MakeStarted()
        {
            Engine.DI.DIContainer.GetAsSingle<Engine.IMakeStarted>().MakeStarted();
        }
    }
}