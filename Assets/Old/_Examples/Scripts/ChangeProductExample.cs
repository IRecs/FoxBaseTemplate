using UnityEngine;
using Engine.Store;

public class ChangeProductExample : MonoBehaviour, IProductUpdated
{
    public Store store;

    // Start is called before the first frame update
    void OnEnable()
    {
        store.OnProductUpdated.Subscribe(this);
    }

    // Start is called before the first frame update
    void OnDisable()
    {
        store.OnProductUpdated.Unsubscribe(this);
    }

    public void OnProductUpdated(IProduct product, ProductStatue statue)
    {
        Debug.Log("The product is updated!...");
    }

}
