using Core.Gameplay.Controllers;
using UnityEngine;

namespace Core.Gameplay.Managers
{
    public sealed class EnemyManager : MonoBehaviour
    {
        private EnemySpawner _enemySpawner;

        public void Init( EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
            _enemySpawner.OnStart();
        }

        private void Update()
        {
            TrySpawnEnemy();
        }

        private void TrySpawnEnemy()
        {
            _enemySpawner.TrySpawnEnemy();
        }

    }
}
