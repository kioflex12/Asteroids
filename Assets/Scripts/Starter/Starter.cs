using Core.Gameplay;
using Core.Gameplay.Controllers;
using Core.Gameplay.Managers;
using Core.Gameplay.Player;
using UnityEngine;

namespace Starter
{
    public sealed class Starter : MonoBehaviour
    {
        [SerializeField] private PlayerShip             _playerShip;
        [SerializeField] private Camera                 _camera;
        [SerializeField] private ShipPlayerSettings     _shipPlayerSettings;
        [SerializeField] private EnemiesData            _enemiesData;
        [SerializeField] private EnemyManager           _enemyManager;

        public PlayerMovementController          PlayerMovementController       { get; private set; }
        public PlayerInputActions          PlayerInputActions       { get; private set; }
        public InputController             InputController          { get; private set; }
        public BoundsMovementController    BoundsMovementController { get; private set; }
        public EnemySpawner                EnemySpawner             { get; private set; }

        private void Reset()
        {
            _playerShip = FindObjectOfType<PlayerShip>();
            _camera = FindObjectOfType<Camera>();
        }

        private void Awake()
        {
            PlayerInputActions       = new PlayerInputActions();
            BoundsMovementController = new BoundsMovementController(_camera);
            PlayerMovementController = new PlayerMovementController(_shipPlayerSettings, BoundsMovementController);
            InputController          = new InputController(PlayerInputActions);
            EnemySpawner             = new EnemySpawner(_camera, _enemiesData);

            PlayerInputActions.Enable();
            _playerShip.Init(InputController, PlayerMovementController);
            _enemyManager.Init(EnemySpawner);
        }


        private void OnDestroy()
        {
            _playerShip.Deinit();
            InputController.Deinit();
            EnemySpawner.Deinit();
        }
    }
}
