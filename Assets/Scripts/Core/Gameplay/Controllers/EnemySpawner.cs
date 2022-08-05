using Core.Gameplay.Enemies;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Core.Gameplay.Controllers
{
    public sealed class EnemySpawner
    {
        private readonly Camera _camera;
        private EnemiesData _enemiesData;
        private CustomTimer _timer;


        private int _spawnedFlyingSaucerCount;


        private readonly Vector2[] _coordsArray =
        {
            new(0, 0),
            new(0, 1),
            new(1, 0),
            new(1, 1),
        };


        public EnemySpawner(Camera camera, EnemiesData enemiesData)
        {
            _camera      = camera;
            _enemiesData = enemiesData;

            _timer = new CustomTimer(enemiesData.SpawnCooldown);

            EventManager.Subscribe<EnemyDead>(this, OnEnemyDead);
        }

        public void OnStart()
        {
            for (var i = 0; i < _enemiesData.AsteroidsStartCount; i++)
            {
                Spawn(_enemiesData.AsteroidPrefab);
            }
        }

        public void TrySpawnEnemy()
        {
            if (_timer.Tick())
            {
                if (_spawnedFlyingSaucerCount < _enemiesData.FlyingSauserMaxCount && RandomUtils.GetRandomBool(_enemiesData.FlyingSaucerSpawnChance))
                {
                    Spawn(_enemiesData.FlyingSaucerPrefab);
                    _spawnedFlyingSaucerCount++;
                    return;
                }
                Spawn(_enemiesData.AsteroidPrefab);
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            var startCoord = _coordsArray[Random.Range(0, _coordsArray.Length)];
            Vector2 position;

            if (startCoord == Vector2.one)
            {
                position = RandomUtils.GetRandomBool() ? new Vector2(Random.value, 1) :
                    new Vector2(1, Random.value);
            }
            else
            {
                position = new Vector3(Random.Range(0f, startCoord.x), Random.Range(0f, startCoord.y));
            }

            return _camera.ViewportToWorldPoint(position);
        }

        private void Spawn(Enemy enemy)
        {
            var spawnPosition = GetRandomSpawnPosition();
            var spawnedEnemy = Object.Instantiate(enemy, spawnPosition, Quaternion.identity);
            spawnedEnemy.InitInternal();
        }

        private void OnEnemyDead(EnemyDead enemyDead)
        {
            if (enemyDead.EnemyType is FlyingSaucer)
            {
                _spawnedFlyingSaucerCount--;
                Debug.LogError(_spawnedFlyingSaucerCount);
            }
        }

        public void Deinit()
        {
            EventManager.Unsubscribe<EnemyDead>(OnEnemyDead);
        }
    }
}
