using UnityEngine;

namespace Core.Gameplay.Controllers
{
    public sealed class BoundsMovementController
    {
        private readonly Vector2 _lowerLeftCameraBound;
        private readonly Vector2 _upperRightCameraBound;

        public BoundsMovementController(Camera camera)
        {
            _lowerLeftCameraBound = camera.ViewportToWorldPoint(Vector2.zero);
            _upperRightCameraBound = camera.ViewportToWorldPoint(Vector2.one);
        }

        public Vector3 TryMoveToOppositeSide(Vector3 position)
        {
            position.x = position.x < _lowerLeftCameraBound.x ? _upperRightCameraBound.x :
                position.x > _upperRightCameraBound.x ? _lowerLeftCameraBound.x : position.x;

            position.y = position.y < _lowerLeftCameraBound.y ? _upperRightCameraBound.y :
                position.y > _upperRightCameraBound.y ? _lowerLeftCameraBound.y : position.y;

            return position;
        }
    }
}
