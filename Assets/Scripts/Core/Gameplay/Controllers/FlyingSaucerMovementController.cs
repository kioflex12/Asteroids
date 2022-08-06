using Core.Gameplay.Player;
using UnityEngine;
using Utils;

namespace Core.Gameplay.Controllers
{
    public class FlyingSaucerMovementController : BaseMovementController
    {
        private readonly PlayerShip               _playerShip;
        private readonly EnemiesData              _enemiesData;
        private readonly BoundsMovementController _boundsMovementController;


        public FlyingSaucerMovementController(EnemiesData enemiesData,PlayerShip playerShip, BoundsMovementController boundsMovementController)
        {
            _enemiesData              = enemiesData;
            _playerShip               = playerShip;
            _boundsMovementController = boundsMovementController;
        }

        public override void TryMove(Transform transform)
        {
            var position = _boundsMovementController.TryMoveToOppositeSide(transform.position);
            transform.position = Vector2.MoveTowards(position, _playerShip.transform.position, _enemiesData.FlyingSaucerMoveSpeed * Time.deltaTime);
        }

        public override void TryRotate(Transform transform)
        {
        }
    }
}