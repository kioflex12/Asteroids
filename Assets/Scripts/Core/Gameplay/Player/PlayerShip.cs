using Core.Gameplay.Controllers;
using UnityEngine;

namespace Core.Gameplay.Player
{
    public class PlayerShip : MonoBehaviour
    {
        private  InputController    _inputController;
        private  MovementController _movementController;

        public void Init(InputController inputController, MovementController movementController)
        {
            _inputController = inputController;
            _movementController = movementController;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _movementController.Move(transform,_inputController.Direction);
        }

        public void Deinit()
        {
            _inputController = null;
            _movementController = null;
        }
    }
}
