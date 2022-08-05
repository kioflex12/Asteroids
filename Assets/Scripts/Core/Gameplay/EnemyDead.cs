using Core.Gameplay.Enemies;

namespace Core.Gameplay
{
    public readonly struct EnemyDead
    {
        public readonly Enemy EnemyType;

        public EnemyDead(Enemy enemyType)
        {
            EnemyType = enemyType;
        }
    }
}