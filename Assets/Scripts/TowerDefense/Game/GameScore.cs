using TowerDefense.Serialization;

namespace TowerDefense.Game
{
    /// <summary>
    /// statistics from a game run 
    /// </summary>
    public class GameScore: SerializableData
    {
        public long score;
        public int killCount;
        public float timeElapsed;
        public float damageDone;
        public int hitsTaken;
        public float damageTaken;
        public int stages;
        public int waves;

        //compare each statistic with the most recent run and update the best one
        public void UpdateRecords(GameScore recentScore)
        {
            if (recentScore.score > score) score = recentScore.score;
            if (recentScore.killCount > killCount) killCount = recentScore.killCount;
            if (recentScore.timeElapsed > timeElapsed) timeElapsed = recentScore.timeElapsed;
            if (recentScore.damageDone > damageDone) damageDone = recentScore.damageDone;
            if (recentScore.hitsTaken < hitsTaken) hitsTaken = recentScore.hitsTaken;
            if (recentScore.damageTaken < damageTaken) damageTaken = recentScore.damageTaken;
            if (recentScore.stages > stages) stages = recentScore.stages;
            if (recentScore.waves > waves) waves = recentScore.waves;
        }
    }
}