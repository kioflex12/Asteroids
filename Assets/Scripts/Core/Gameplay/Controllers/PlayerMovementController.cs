﻿using UnityEngine;

namespace Core.Gameplay.Controllers
{
    public sealed class PlayerMovementController : BaseMovementController
    {
        private readonly ShipPlayerSettings       _playerSettings;
        private readonly BoundsMovementController _boundsMovementController;
        private readonly InputController          _inputController;

        private Vector3 _inertial;

        public float Speed { get; private set; }

        public PlayerMovementController(ShipPlayerSettings playerSettings, BoundsMovementController boundsMovementController, InputController inputController)
        {
            _playerSettings           = playerSettings;
            _boundsMovementController = boundsMovementController;
            _inputController          = inputController;
        }

        private Vector3 GetInertialDirection(Quaternion rotation, Vector2 direction)
        {
            Speed = Mathf.Clamp( Speed + (direction.y > 0 ?  _playerSettings.GasForce : _playerSettings.StopForce)  * Time.fixedDeltaTime,
                0 ,
                _playerSettings.SpeedLimit);

            _inertial += rotation * Vector3.up * Speed * Time.deltaTime;
            _inertial = Vector2.ClampMagnitude(_inertial, _playerSettings.MoveSpeedLimit);
            _inertial *= _playerSettings.InertionDamping;
            return _inertial;
        }

        public override void TryRotate(Transform transform)
        {
            if (_inputController.Direction.x == 0)
            {
                return;
            }

            transform.Rotate(Vector3.forward * _playerSettings.RotationSpeed * -_inputController.Direction.x);
        }

        public override void TryMove(Transform transform)
        {
            var inertialDirection = GetInertialDirection(transform.rotation, _inputController.Direction);

            if (inertialDirection == Vector3.zero)
            {
                return;
            }

            transform.position = _boundsMovementController.TryMoveToOppositeSide(transform.position);
            transform.Translate(inertialDirection, Space.World);
        }
    }
}
