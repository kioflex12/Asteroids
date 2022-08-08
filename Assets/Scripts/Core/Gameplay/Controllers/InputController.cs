using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Core.Gameplay.Controllers
{
    public sealed class InputController
    {
        private readonly PlayerInputActions _playerInputActions;

        public UnityAction OnFIre;

        public Vector2 Direction { get; private set; }

        public InputController(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
            Init();
        }

        private void Init()
        {
            _playerInputActions.Player.Move.performed += Move;
            _playerInputActions.Player.Move.canceled  += Stop;
            _playerInputActions.Player.Fire.performed += Fire;
        }

        private void Fire(InputAction.CallbackContext context)
        {
            OnFIre?.Invoke();
        }

        //TODO: change to event
        private void Move(InputAction.CallbackContext context)
        {
            Direction = context.ReadValue<Vector2>();
        }

        private void Stop(InputAction.CallbackContext context)
        {
            Direction = Vector2.zero;
        }

        public void Deinit()
        {
            _playerInputActions.Player.Move.performed -= Move;
            _playerInputActions.Player.Move.canceled  -= Stop;
            _playerInputActions.Player.Fire.performed -= Fire;
        }
    }
}
