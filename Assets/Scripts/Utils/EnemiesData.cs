using Core.Gameplay.Enemies;
using UnityEngine;

namespace Utils
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "SettingAsset/EnemiesSettings", order = 1)]

    public sealed class EnemiesData : ScriptableObject
    {
        public float SpawnCooldown;

        [Header(nameof(Asteroid))]
        public Asteroid AsteroidPrefab;
        public int      AsteroidsStartCount;
        public float    AsteroidsMoveSpeed;

        [Header(nameof(FlyingSaucer))]
        public FlyingSaucer FlyingSaucerPrefab;
        public int          FlyingSauserMaxCount;
        public float        FlyingSaucerMoveSpeed;
        public float        FlyingSaucerSpawnChance;
    }
}
