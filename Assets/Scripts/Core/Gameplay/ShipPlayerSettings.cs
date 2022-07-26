using UnityEngine;

namespace Core.Gameplay
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "SettingAsset/PlayerSettings", order = 1)]
    public class ShipPlayerSettings : ScriptableObject
    {
        public float RotationSpeed;
        public float MoveSpeedLimit;
        public float InertionDamping;
        public float GasForce ;
        public float StopForce;
    }
}

