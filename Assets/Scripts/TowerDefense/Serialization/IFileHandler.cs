namespace TowerDefense.Serialization
{
    /// <summary>
    /// Contract for File Serializers 
    /// </summary>
    public interface IFileHandler
    {
        public void Write(SerializableData data);
        public void Read<T>(out T data);
    }
}