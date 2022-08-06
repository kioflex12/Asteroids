using UnityEngine;

namespace Core.Gameplay.Controllers
{
    public abstract class BaseMovementController
    {
        public abstract void TryMove(Transform transform, Vector2 direction);
        public abstract void TryRotate(Transform transform, Vector2 direction);
    }
}