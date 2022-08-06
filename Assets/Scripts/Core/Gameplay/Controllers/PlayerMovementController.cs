using UnityEngine;

namespace Core.Gameplay.Controllers
{
    public sealed class PlayerMovementController : BaseMovementController
    {
        private readonly ShipPlayerSettings _playerSettings;
        private readonly BoundsMovementController _boundsMovementController;

        private Vector3 _inertial;
        private float _speed;

        public PlayerMovementController(ShipPlayerSettings playerSettings, BoundsMovementController boundsMovementController)
        {
            _playerSettings           = playerSettings;
            _boundsMovementController = boundsMovementController;
        }

        private Vector3 GetInertialDirection(Quaternion rotation, Vector2 direction)
        {
            _speed = Mathf.Clamp( _speed + (direction.y > 0 ? direction.y : _playerSettings.StopForce) * _playerSettings.GasForce * Time.fixedDeltaTime,
                0 ,
                _playerSettings.SpeedLimit);

            _inertial += rotation * Vector3.up * _speed * Time.deltaTime;
            _inertial = Vector2.ClampMagnitude(_inertial, _playerSettings.MoveSpeedLimit);
            _inertial *= _playerSettings.InertionDamping;
            return _inertial;
        }

        public override void TryRotate(Transform transform, Vector2 direction)
        {
            if (direction.x == 0)
            {
                return;
            }

            transform.Rotate(Vector3.forward * _playerSettings.RotationSpeed * -direction.x);
        }

        public override void TryMove(Transform transform, Vector2 direction)
        {
            var inertialDirection = GetInertialDirection(transform.rotation, direction);

            if (inertialDirection == Vector3.zero)
            {
                return;
            }

            transform.position = _boundsMovementController.TryMoveToOppositeSide(transform.position);
            transform.Translate(inertialDirection, Space.World);
        }
    }
}
