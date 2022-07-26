using Core.Gameplay.Controllers;
using UnityEngine;

namespace Core.Gameplay.Player
{
    public class PlayerShip : MonoBehaviour
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
            Move();
        }

        private void Move()
        {
            transform.position = _boundsMovementController.TryMoveToOppositeSide(transform.position);
            _movementController.Move(transform, _inputController.Direction);
        }

        public void Deinit()
        {
            _inputController = null;
            _movementController = null;
        }
    }
}
