using System;

namespace Engine.Coin
{
    public interface ICoinsData
    {
        int totalCoins { get; }

        event Action<ParametersUpdate> onUpdate;

        void AddCoins(int amount);

        void RemoveCoins(int amount);
    }
}
