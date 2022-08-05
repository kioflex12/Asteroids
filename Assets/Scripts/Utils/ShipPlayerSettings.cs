using UnityEngine;

namespace Core.Gameplay
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "SettingAsset/PlayerSettings", order = 1)]
    public sealed class ShipPlayerSettings : ScriptableObject
    {
        public float RotationSpeed;
        public float MoveSpeedLimit;
        public float InertionDamping;
        public float GasForce ;
        public float StopForce;
        public float SpeedLimit;
    }
}

