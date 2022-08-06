using Core.Gameplay.Controllers;
using UnityEngine;

namespace Core.Gameplay.Player
{
    public sealed class PlayerShip : MonoBehaviour
    {
        private  InputController    _inputController;
        private  PlayerMovementController _playerMovementController;

        public void Init(InputController inputController, PlayerMovementController playerMovementController)
        {
            _inputController          = inputController;
            _playerMovementController = playerMovementController;
        }

        private void Update()
        {
            TryMove();
            TryRotate();
        }

        private void TryMove()
        {
            _playerMovementController.TryMove(transform, _inputController.Direction);
        }

        private void TryRotate()
        {
           _playerMovementController.TryRotate(transform, _inputController.Direction);
        }

        public void Deinit()
        {
            _inputController = null;
            _playerMovementController = null;
        }
    }
}
