using Core.Gameplay.Controllers;
using UnityEngine;

namespace Core.Gameplay.Enemies
{
    public class Asteroid : Enemy
    {
        private BaseMovementController _movementController;

        public override void InitInternal(BaseMovementController movementController)
        {
            _movementController = movementController;

            _movementController.TryRotate(transform);
        }

        private void Update()
        {
            TryMove();
        }

        private void TryMove()
        {
            _movementController.TryMove(transform);

        }
    }
}
