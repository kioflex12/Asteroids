using System.Threading.Tasks;
using Core.Gameplay.Player;
using UnityEngine;

namespace Core.Gameplay.Controllers
{
    public class PlayerWeaponController
    {
        private readonly InputController          _inputController;
        private readonly PlayerShip               _playerShip;
        private readonly ShipPlayerSettings       _shipPlayerSettings;
        private readonly PlayerMovementController _playerMovementController;
        private readonly CustomTimer              _timer;

        private bool _weaponIsReady;

        public PlayerWeaponController(InputController inputController, PlayerShip playerShip, ShipPlayerSettings shipPlayerSettings, PlayerMovementController playerMovementController)
        {
            _inputController          = inputController;
            _playerShip               = playerShip;
            _shipPlayerSettings       = shipPlayerSettings;
            _playerMovementController = playerMovementController;

            _timer = new CustomTimer(_shipPlayerSettings.BulletShootCooldown);

            _inputController.OnFIre += OnFire;

            _weaponIsReady = true;
        }

        private async void StartCooldown()
        {
            _weaponIsReady = false;

            while(!_weaponIsReady)
            {
                if (_timer.Tick())
                {
                    _weaponIsReady = true;
                    return;
                }

                await Task.Yield();
            }
        }
        private void OnFire()
        {
            if (_weaponIsReady)
            {
                var transform = _playerShip.transform;
                var bullet = Object.Instantiate(_shipPlayerSettings.BulletPrefab, transform.position,
                    transform.rotation);
                bullet.Init(_shipPlayerSettings.BulletSpeed + _playerMovementController.Speed);
                StartCooldown();
            }
        }

        public void Deinit()
        {
            _inputController.OnFIre -= OnFire;
        }
    }
}