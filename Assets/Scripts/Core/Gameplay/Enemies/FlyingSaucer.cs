using System;
using Core.Gameplay.Controllers;
using Utils;

namespace Core.Gameplay.Enemies
{
    public class FlyingSaucer : Enemy
    {
        private BaseMovementController _movementController;

        public override void InitInternal(BaseMovementController movementController)
        {
            _movementController = movementController;
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
