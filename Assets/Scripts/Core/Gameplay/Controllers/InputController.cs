using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Gameplay.Controllers
{
    public sealed class InputController
    {
        private readonly PlayerInputActions PlayerInputActions;

        public Vector2 Direction { get; private set; }

        public InputController(PlayerInputActions playerInputActions)
        {
            PlayerInputActions = playerInputActions;
            Init();

        }

        private void Init()
        {
            PlayerInputActions.Player.Move.performed += Move;
            PlayerInputActions.Player.Move.canceled  += Stop;
            PlayerInputActions.Player.Fire.performed += Fire;
        }

        private void Fire(InputAction.CallbackContext context)
        {
            Debug.Log("Fire!");
        }

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
            PlayerInputActions.Player.Move.performed -= Move;
            PlayerInputActions.Player.Move.canceled  -= Stop;
            PlayerInputActions.Player.Fire.performed -= Fire;
        }
    }
}
