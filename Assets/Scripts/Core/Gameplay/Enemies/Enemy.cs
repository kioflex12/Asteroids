using Core.Gameplay.Controllers;
using UnityEngine;
using Utils;

namespace Core.Gameplay.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public abstract void InitInternal(BaseMovementController movementController);

        public  void Dead()
        {
            EventManager.Fire(new EnemyDead(this));
            Destroy(gameObject);

        }
    }
}
