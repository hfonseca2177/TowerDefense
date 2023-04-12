namespace TowerDefense.Serialization
{
    /// <summary>
    /// Sets a object to support game scale and persistence in the game 
    /// </summary>
    public interface IPersistence<T> where T: SerializableData
    {
        void Save(T dto);
        T Retrieve();
    }
}