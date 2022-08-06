using UnityEngine;

namespace Core.Gameplay.Controllers
{
    public abstract class BaseMovementController
    {
        public abstract void TryMove(Transform transform);
        public abstract void TryRotate(Transform transform);
    }
}