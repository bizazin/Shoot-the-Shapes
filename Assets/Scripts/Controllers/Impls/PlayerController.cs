using System;
using Core.Abstracts;
using Databases;
using Services;
using Signals;
using UniRx;
using UnityEngine;
using Views;
using Zenject;

namespace Controllers.Impls
{
    public class PlayerController : Controller<PlayerView>, IPlayerController, IInitializable, IDisposable
    {
        private readonly IPlayerSettingsDatabase _playerSettingsDatabase;
        private readonly IBulletSettingsDatabase _bulletSettingsDatabase;
        private readonly IBulletService _bulletService;
        private readonly SignalBus _signalBus;
        private readonly CompositeDisposable _disposable = new();
        private IDisposable _spawnSubscription;

        public PlayerController
        (
            IPlayerSettingsDatabase playerSettingsDatabase,
            IBulletSettingsDatabase bulletSettingsDatabase,
            IBulletService bulletService,
            SignalBus signalBus
        )
        {
            _playerSettingsDatabase = playerSettingsDatabase;
            _bulletSettingsDatabase = bulletSettingsDatabase;
            _bulletService = bulletService;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.GetStream<SignalBulletHitShape>().Subscribe(_ => StopSpawningBullets()).AddTo(_disposable);
            StartSpawningBullets();
        }

        public void Dispose() => _disposable?.Dispose();

        private void StartSpawningBullets()
        {
            _spawnSubscription?.Dispose();

            _spawnSubscription = Observable
                .Interval(TimeSpan.FromSeconds(_playerSettingsDatabase.Settings.ShootingIntervalS))
                .Subscribe(_ => LaunchBullet()).AddTo(_disposable);
        }

        private void StopSpawningBullets()
        {
            _spawnSubscription?.Dispose();
            _spawnSubscription = null;
        }

        public void TurnInDirection(Vector2 moveVector)
        {
            var horizontalRotation = moveVector.x * _playerSettingsDatabase.Settings.RotationSpeed * Time.deltaTime;
            var verticalRotation = -moveVector.y * _playerSettingsDatabase.Settings.RotationSpeed * Time.deltaTime;

            View.RiflePivotTransform.Rotate(Vector3.up, horizontalRotation, Space.World);
            View.RiflePivotTransform.Rotate(Vector3.right, verticalRotation, Space.Self);
        }

        private void LaunchBullet()
        {
            var bullet = _bulletService.SpawnBullet(View.BulletSpawnTransform);
            bullet.Launch(_bulletSettingsDatabase.Settings.BulletVelocity, View.BulletSpawnTransform.forward);
        }
    }
}