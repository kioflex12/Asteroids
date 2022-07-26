using Core.Gameplay;
using Core.Gameplay.Controllers;
using Core.Gameplay.Player;
using UnityEngine;

namespace Starter
{
    public sealed class Starter : MonoBehaviour
    {
        [SerializeField] private PlayerShip _playerShip;

        public MovementController MovementController { get; private set; }
        public PlayerInputActions PlayerInputActions { get; private set; }
        public InputController    InputController    { get; private set; }

        private void Reset()
        {
            _playerShip = FindObjectOfType<PlayerShip>();
        }

        private void Awake()
        {
            var playerSettings = GetPlayerSettings();
            if (!playerSettings)
            {
                Debug.LogError("Can`t find PlayerSetting.asset");
                return;
            }

            MovementController = new MovementController(playerSettings);
            PlayerInputActions = new PlayerInputActions();
            InputController    = new InputController(PlayerInputActions);
            PlayerInputActions.Enable();
            _playerShip.Init(InputController,MovementController);
        }

        private ShipPlayerSettings GetPlayerSettings() => Resources.Load<ShipPlayerSettings>("PlayerSettings");

        private void OnDestroy()
        {
            _playerShip.Deinit();
            InputController.Deinit();
        }
    }
}
