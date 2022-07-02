namespace Engine.Store
{
    public enum ProductStatue { Non = -1, Selected = 0, Bought = 1, ForBuy = 2 }

    public abstract class Product : UnityEngine.ScriptableObject, IProduct
    {
        public abstract void Initialize(IStore store, int id);

        public abstract bool AllowReward();
        public abstract bool Reward();

        public abstract bool Selected();
        public abstract bool AllowSelect();
        public abstract bool Deselect();

        public abstract bool UpdateState();
    }
}
