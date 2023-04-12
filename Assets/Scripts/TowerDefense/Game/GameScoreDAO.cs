using TowerDefense.Serialization;
using UnityEngine;

namespace TowerDefense.Game
{
    /// <summary>
    /// Data Access Object to read and write persistent data based on file serializer
    /// </summary>
    public class GameScoreDAO: MonoBehaviour,IPersistence<GameScore>
    {
        [SerializeField] private FileSerializer _fileSerializer;
        
        public void Save(GameScore dto)
        {
            _fileSerializer.Write(dto);
        }

        public GameScore Retrieve()
        {
            _fileSerializer.Read<GameScore>(out var dto);
            return dto;
        }
    }
}