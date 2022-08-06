using Core.Gameplay.Enemies;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Core.Gameplay.Controllers
{
    public sealed class EnemySpawner
    {
        private readonly Camera                         _camera;
        private readonly EnemiesData                    _enemiesData;
        private readonly AsteroidsMovementController    _asteroidsMovementController;
        private readonly FlyingSaucerMovementController _flyingSaucerMovementController;
        private readonly CustomTimer                    _timer;

        private readonly Vector2[] _coordsArray =
        {
            new(0, 0),
            new(0, 1),
            new(1, 0),
            new(1, 1),
        };

        private int _spawnedFlyingSaucerCount;

        public EnemySpawner(Camera camera, EnemiesData enemiesData, AsteroidsMovementController asteroidsMovementController, FlyingSaucerMovementController flyingSaucerMovementController)
        {
            _camera                         = camera;
            _enemiesData                    = enemiesData;
            _asteroidsMovementController    = asteroidsMovementController;
            _flyingSaucerMovementController = flyingSaucerMovementController;

            _timer = new CustomTimer(enemiesData.SpawnCooldown);

            EventManager.Subscribe<EnemyDead>(this, OnEnemyDead);
        }

        public void Init()
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
            position = _camera.ViewportToWorldPoint(position);
            return new Vector3(position.x, position.y, 0f);
        }

        private void Spawn(Enemy enemy)
        {
            var spawnPosition = GetRandomSpawnPosition();
            var spawnedEnemy = Object.Instantiate(enemy, spawnPosition, Quaternion.identity);

            BaseMovementController movementController;
            switch (spawnedEnemy)
            {
                case Asteroid:
                    movementController = _asteroidsMovementController;
                    break;
                case FlyingSaucer:
                    movementController = _flyingSaucerMovementController;
                    break;
                default:
                    Debug.LogError($"Can`t init spawned enemy, unknown type enemy :{enemy}");
                    return;
            }

            spawnedEnemy.InitInternal(movementController);
        }

        private void OnEnemyDead(EnemyDead enemyDead)
        {
            if (enemyDead.EnemyType is FlyingSaucer)
            {
                _spawnedFlyingSaucerCount--;
            }
        }

        public void Deinit()
        {
            EventManager.Unsubscribe<EnemyDead>(OnEnemyDead);
        }
    }
}
