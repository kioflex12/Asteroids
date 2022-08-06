using Core.Gameplay.Controllers;
using UnityEngine;

namespace Core.Gameplay.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public abstract void InitInternal(BaseMovementController movementController);
    }
}
