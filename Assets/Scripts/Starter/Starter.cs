using Core.Gameplay;
using Core.Gameplay.Controllers;
using Core.Gameplay.Managers;
using Core.Gameplay.Player;
using UnityEngine;
using Utils;

namespace Starter
{
    public sealed class Starter : MonoBehaviour
    {
        [SerializeField] private PlayerShip             _playerShip;
        [SerializeField] private Camera                 _camera;
        [SerializeField] private ShipPlayerSettings     _shipPlayerSettings;
        [SerializeField] private EnemiesData            _enemiesData;
        [SerializeField] private EnemyManager           _enemyManager;

        public PlayerMovementController    PlayerMovementController    { get; private set; }
        public AsteroidsMovementController AsteroidsMovementController { get; private set; }
        public FlyingSaucerMovementController FlyingSaucerMovementController { get; private set; }
        public PlayerInputActions          PlayerInputActions          { get; private set; }
        public InputController             InputController             { get; private set; }
        public BoundsMovementController    BoundsMovementController    { get; private set; }
        public EnemySpawner                EnemySpawner                { get; private set; }
        public PlayerWeaponController      PlayerWeaponController      { get; private set; }

        private void Reset()
        {
            _playerShip = FindObjectOfType<PlayerShip>();
            _camera = FindObjectOfType<Camera>();
        }

        private void Awake()
        {
            PlayerInputActions             = new PlayerInputActions();
            InputController                = new InputController(PlayerInputActions);
            BoundsMovementController       = new BoundsMovementController(_camera);
            PlayerMovementController       = new PlayerMovementController(_shipPlayerSettings, BoundsMovementController, InputController);
            AsteroidsMovementController    = new AsteroidsMovementController(_enemiesData, BoundsMovementController);
            FlyingSaucerMovementController = new FlyingSaucerMovementController(_enemiesData, _playerShip, BoundsMovementController);
            EnemySpawner                   = new EnemySpawner(_camera, _enemiesData, AsteroidsMovementController, FlyingSaucerMovementController);
            PlayerWeaponController         = new PlayerWeaponController(InputController, _playerShip, _shipPlayerSettings, PlayerMovementController);

            PlayerInputActions.Enable();
            _playerShip.Init(PlayerMovementController);
            _enemyManager.Init(EnemySpawner);
        }


        private void OnDestroy()
        {
            PlayerWeaponController.Deinit();
            _playerShip.Deinit();
            InputController.Deinit();
            EnemySpawner.Deinit();
        }
    }
}
