using Data;
using Engine.DI;
using Engine.Events;
using System;
using UnityEngine;

namespace Engine.Store
{
    public interface IProductUpdated
    {
        void OnProductUpdated(IProduct product, ProductStatue statue);
    }

    [CreateAssetMenu(fileName = "New Store", menuName = "Add/Store/Add Store", order = 1)]
    public class Store : ScriptableObject, IStore, IData, IAwake, IDependency
    {
        public Event<IProductUpdated> OnProductUpdated = new Event<IProductUpdated>();

        [Header("Data")]
        [SerializeField] private int _idData;
        [SerializeField] private StoreData _data;

        [Header("Settings")]
        [SerializeField] private Product[] _products;

        public void Inject()
        {
            DIContainer.Register<IStore>(this);
        }

        public void Awake()
        {
            LoadData();

            InitializeProducts();
        }

        private void InitializeProducts()
        {
            if (_products == null)
                return;

            for (int i = 0; i < _products.Length; i++)
            {
                _products[i].Initialize(this, i);
            }
        }

        public void RefreshProducts()
        {
            for (int i = 0; i < _products.Length; i++)
            {
                _products[i].UpdateState();
            }
        }

        public void LoadData()
        {
            _data = ES3.Load(GetKey(), ObjectSaver.GetSavingPathFile<Coin.Data>(GetKey()), _data);
        }

        public void SaveData()
        {
            ES3.Save(GetKey(), _data, ObjectSaver.GetSavingPathFile<Coin.Data>(GetKey()));
        }

        public void ResetData(int idData)
        {
            _idData = idData;
            if (_products != null && _products.Length != 0)
            {
                _data = new StoreData();
                _data.idSelectedProduct = 0;
                _data.isBoughtProducts = new bool[_products.Length];

                if (_data.isBoughtProducts.Length != 0) _data.isBoughtProducts[0] = true;
            }
        }

        public string GetKey()
        {
            return "Store." + _idData;
        }

        protected void OnValidate()
        {
            if (_data.isBoughtProducts.Length != _products.Length)
                ResetData(_idData);
        }

        public bool DeselectProduct()
        {
            if (_data.idSelectedProduct < 0 || _products.Length <= _data.idSelectedProduct) return false;

            _products[_data.idSelectedProduct].Deselect();
            OnProductUpdated.Events.Invoke(item => item.OnProductUpdated(_products[_data.idSelectedProduct], ProductStatue.Bought));
            _data.idSelectedProduct = -1;
            return true;
        }

        /// <summary>
        /// If user the use can select or choice this product.
        /// </summary>
        /// <param name="idProduct"> The id of the product. </param>
        /// <returns> True if product is enable for select.</returns>
        public virtual bool AllowSelect(int idProduct)
        {
            if (idProduct < 0 || _products.Length <= idProduct)
            {
                Debug.LogError("The id is out of array lenght: ID " + idProduct + ", Array Lenght: " + _products.Length);
                return false;
            }

            return _data.isBoughtProducts[idProduct] && _data.idSelectedProduct != idProduct && _products[idProduct].AllowSelect();
        }

        public bool SelectProduct(int idProduct)
        {
            if (!AllowSelect(idProduct))
                return false;
            else
            {
                // Deselect the old id.
                DeselectProduct();

                // update data product.
                _data.idSelectedProduct = idProduct;

                // Execut select on the product class.
                _products[idProduct].Selected();
                OnProductUpdated.Events.Invoke(item => item.OnProductUpdated(_products[idProduct], ProductStatue.Selected));

                // Save data.
                SaveData();
                return true;
            }
        }

        /// <summary>
        /// If user the use can Buy this product.
        /// </summary>
        /// <param name="idProduct"> The id of the product. </param>
        /// <returns> True if product is enable for buy.</returns>
        public virtual bool AllowBuyProduct(int idProduct)
        {
            if (idProduct < 0 || _products.Length <= idProduct)
            {
                throw new ArgumentOutOfRangeException("The id is out of array lenght: ID " + idProduct + ", Array Lenght: " + _products.Length);
            }

            return true; /// Here you need check if the player can buy the product or no.
        }
        
        /// <summary>
        /// If user the use can Buy this product.
        /// </summary>
        /// <param name="idProduct"> The id of the product. </param>
        /// <returns> True if product is enable for buy.</returns>
        public virtual bool AllowRewardProduct(int idProduct)
        {
            if (idProduct < 0 || _products.Length <= idProduct)
            {
                throw new ArgumentOutOfRangeException("The id is out of array lenght: ID " + idProduct + ", Array Lenght: " + _products.Length);
            }

            return !_data.isBoughtProducts[idProduct] && _products[idProduct].AllowReward();
        }

        public bool BuyProduct(int idProduct)
        {
            if (!AllowBuyProduct(idProduct))
                return false;

            return RewardProduct(idProduct);
        }

        public bool RewardProduct(int idProduct)
        {
            if (!AllowRewardProduct(idProduct))
                return false;

            DeselectProduct();

            // Execute buy the product.
            _data.isBoughtProducts[idProduct] = true;
            _products[idProduct].Reward();

            // Execute select the product.
            SelectProduct(idProduct);

            // Save data.
            SaveData();
            return true;
        }

        public IProduct GetProduct(int idProduct)
        {
            if (idProduct < 0 || _products.Length <= idProduct)
            {
                Debug.LogError("The id is out of array lenght: ID " + idProduct + ", Array Lenght: " + _products.Length);
                return null;
            }

            return _products[idProduct];
        }

        public int GetTotalProducts()
        {
            return _products.Length;
        }

        public int GetIDSelectedProduct()
        {
            return _data.idSelectedProduct;
        }

        public virtual ProductStatue GetProductState(int idProduct)
        {
            if ((uint)_data.isBoughtProducts.Length <= (uint)idProduct) throw new ArgumentOutOfRangeException();

            if (GetIDSelectedProduct() == idProduct)
                return ProductStatue.Selected;

            if (_data.isBoughtProducts[idProduct])
                return ProductStatue.Bought;

            return ProductStatue.ForBuy;
        }
    }
}
