namespace Engine
{
    public interface IData : IResetData
    {
        /// <summary>
        /// Get the key of saving data.
        /// </summary>
        string GetKey();

        void SaveData();
    }
}