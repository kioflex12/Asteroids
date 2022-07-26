using Core.Gameplay;
using Core.Gameplay.Controllers;
using Core.Gameplay.Player;
using UnityEngine;

namespace Starter
{
    public sealed class Starter : MonoBehaviour
    {
        [SerializeField] private PlayerShip             _playerShip;
        [SerializeField] private Camera                 _camera;
        [SerializeField] private ShipPlayerSettings     _shipPlayerSettings;

        public MovementController         MovementController        { get; private set; }
        public PlayerInputActions          PlayerInputActions       { get; private set; }
        public InputController             InputController          { get; private set; }
        public BoundsMovementController    BoundsMovementController { get; private set; }

        private void Reset()
        {
            _playerShip = FindObjectOfType<PlayerShip>();
            _camera = FindObjectOfType<Camera>();
        }

        private void Awake()
        {
            MovementController       = new MovementController(_shipPlayerSettings);
            PlayerInputActions       = new PlayerInputActions();
            InputController          = new InputController(PlayerInputActions);
            BoundsMovementController = new BoundsMovementController(_camera);

            PlayerInputActions.Enable();
            _playerShip.Init(InputController,MovementController,BoundsMovementController);
        }


        private void OnDestroy()
        {
            _playerShip.Deinit();
            InputController.Deinit();
        }
    }
}
