using Utils;

namespace Core.Gameplay.Enemies
{
    public class FlyingSaucer : Enemy
    {
        public override void InitInternal()
        {
        }

        private void OnDestroy()
        {
            EventManager.Fire(new EnemyDead(this));
        }
    }
}
