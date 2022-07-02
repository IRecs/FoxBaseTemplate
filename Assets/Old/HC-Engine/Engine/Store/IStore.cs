﻿namespace Engine.Store
{
    public interface IStore
    {
        bool DeselectProduct();
        bool AllowSelect(int idProduct);
        bool SelectProduct(int idProduct);

        bool AllowBuyProduct(int idProduct);
        bool BuyProduct(int idProduct);

        bool AllowRewardProduct(int idProduct);
        bool RewardProduct(int idProduct);

        int GetTotalProducts();
        int GetIDSelectedProduct();
        IProduct GetProduct(int idProduct);
        ProductStatue GetProductState(int idProduct);
    }
}
