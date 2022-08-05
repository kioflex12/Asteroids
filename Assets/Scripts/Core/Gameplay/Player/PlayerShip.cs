using Core.Gameplay.Controllers;
using UnityEngine;

namespace Core.Gameplay.Player
{
    public sealed class PlayerShip : MonoBehaviour
    {
        private  InputController    _inputController;
        private  MovementController _movementController;

        public void Init(InputController inputController, MovementController movementController)
        {
            _inputController    = inputController;
            _movementController = movementController;
        }

        private void Update()
        {
            TryMove();
            TryRotate();
        }

        private void TryMove()
        {
            _movementController.TryMove(transform, _inputController.Direction);
        }

        private void TryRotate()
        {
            if (_inputController.Direction.x == 0)
            {
                return;
            }

            var rotationVector = _movementController.GetRotationVector(_inputController.Direction.x);
            transform.Rotate(rotationVector);
        }

        public void Deinit()
        {
            _inputController = null;
            _movementController = null;
        }
    }
}
