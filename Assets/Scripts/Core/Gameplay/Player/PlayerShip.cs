using Core.Gameplay.Controllers;
using UnityEngine;

namespace Core.Gameplay.Player
{
    public sealed class PlayerShip : MonoBehaviour
    {
        private  BaseMovementController _movementController;

        public void Init(BaseMovementController movementController)
        {
            _movementController = movementController;
        }

        private void Update()
        {
            TryMove();
            TryRotate();
        }

        private void TryMove()
        {
            _movementController.TryMove(transform);
        }

        private void TryRotate()
        {
           _movementController.TryRotate(transform);
        }

        public void Deinit()
        {
            _movementController = null;
        }
    }
}
