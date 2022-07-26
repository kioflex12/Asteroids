using UnityEngine;

namespace Core.Gameplay.Controllers
{
    public sealed class MovementController
    {
        private readonly ShipPlayerSettings _playerSettings;

        private Vector3 _inertion;
        private float _gas;

        public MovementController(ShipPlayerSettings playerSettings)
        {
            _playerSettings = playerSettings;
        }

        public void Move(Transform transform, Vector2 direction)
        {
            transform.Rotate(Vector3.forward * _playerSettings.RotationSpeed, direction.x * -1f);

            if (direction.y >= 0)
            {
                _gas = Mathf.Clamp( _gas + (direction.y > 0 ? direction.y : _playerSettings.StopForce) * _playerSettings.GasForce * Time.fixedDeltaTime, 0 , 1.5f);

                _inertion += transform.up * _gas * Time.deltaTime;
                _inertion = Vector2.ClampMagnitude(_inertion, _playerSettings.MoveSpeedLimit);
                _inertion *= _playerSettings.InertionDamping;

                transform.Translate(_inertion, Space.World);

            }
        }
    }
}
