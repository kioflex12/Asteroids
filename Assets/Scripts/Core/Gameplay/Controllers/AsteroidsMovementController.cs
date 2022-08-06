using UnityEngine;
using Utils;

namespace Core.Gameplay.Controllers
{
    public class AsteroidsMovementController : BaseMovementController
    {
        private readonly EnemiesData _enemiesData;
        private readonly BoundsMovementController _boundsMovementController;


        public AsteroidsMovementController(EnemiesData enemiesData, BoundsMovementController boundsMovementController)
        {
            _enemiesData = enemiesData;
            _boundsMovementController = boundsMovementController;
        }


        private Vector3 GetMoveDirection(Quaternion rotation)
        {
            return rotation * Vector3.up * _enemiesData.AsteroidsMoveSpeed * Time.deltaTime;
        }

        public override void TryMove(Transform transform)
        {
            transform.position = _boundsMovementController.TryMoveToOppositeSide(transform.position);
            transform.Translate(GetMoveDirection(transform.rotation));
        }

        public override void TryRotate(Transform transform)
        {
            transform.up = Vector3.zero - transform.position;
        }
    }
}