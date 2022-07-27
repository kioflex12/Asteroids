using Core.Gameplay.Controllers;
using UnityEngine;

namespace Core.Gameplay.Player
{
    public sealed class PlayerShip : MonoBehaviour
    {
        private  InputController          _inputController;
        private  MovementController       _movementController;
        private  BoundsMovementController _boundsMovementController;

        public void Init(InputController inputController, MovementController movementController, BoundsMovementController boundsMovementController)
        {
            _inputController          = inputController;
            _movementController       = movementController;
            _boundsMovementController = boundsMovementController;
        }

        private void Update()
        {
            TryMove();
            TryRotate();
        }

        private void TryMove()
        {
            var inertialDirection = _movementController.GetInertialDirection(transform.rotation, _inputController.Direction);

            if (inertialDirection == Vector3.zero)
            {
                return;
            }

            transform.position = _boundsMovementController.TryMoveToOppositeSide(transform.position);
            transform.Translate(inertialDirection, Space.World);
        }

        private void TryRotate()
        {
            if (_inputController.Direction.x == 0)
            {
                return;
            }

            var rotationVector = _movementController.GetRotationVector();
            transform.Rotate(rotationVector, -_inputController.Direction.x);
        }

        public void Deinit()
        {
            _inputController = null;
            _movementController = null;
        }
    }
}
