using System;
using Core.Abstracts;
using Databases;
using DG.Tweening;
using ObjectPooling.Objects;
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
        private readonly IShapeSettingsDatabase _shapeSettingsDatabase;
        private readonly IBulletService _bulletService;
        private readonly SignalBus _signalBus;
        private readonly IShapeService _shapeService;
        private readonly CompositeDisposable _disposable = new();
        private IDisposable _spawnSubscription;

        private bool _isCurrentShapeActive;

        public PlayerController
        (
            IPlayerSettingsDatabase playerSettingsDatabase,
            IBulletSettingsDatabase bulletSettingsDatabase,
            IShapeSettingsDatabase shapeSettingsDatabase,
            IBulletService bulletService,
            SignalBus signalBus,
            IShapeService shapeService
        )
        {
            _playerSettingsDatabase = playerSettingsDatabase;
            _bulletSettingsDatabase = bulletSettingsDatabase;
            _shapeSettingsDatabase = shapeSettingsDatabase;
            _bulletService = bulletService;
            _signalBus = signalBus;
            _shapeService = shapeService;
        }

        public void Initialize()
        {
            _signalBus.GetStream<SignalBulletHitShape>().Subscribe(s => OnShapeHit(s.HitShapeComponent, s.HitPosition))
                .AddTo(_disposable);
            SpawnNextShape();
            StartSpawningBullets();
        }

        public void Dispose() => _disposable?.Dispose();

        private void OnShapeHit(ShapeComponentBehaviour shapeComponent, Vector3 hitPosition)
        {
            if (!_isCurrentShapeActive)
                return;
            _spawnSubscription?.Dispose();
            _spawnSubscription = null;
            _shapeService.DestroyCurrentShape(shapeComponent, hitPosition);
            Observable.Timer(TimeSpan.FromSeconds(_shapeSettingsDatabase.Settings.SpawnIntervalS))
                .Subscribe(_ =>
                {
                    SpawnNextShape();
                    StartSpawningBullets();
                }).AddTo(_disposable);
            _isCurrentShapeActive = false;
        }

        private void SpawnNextShape()
        {
            _isCurrentShapeActive = true;
            _shapeService.DespawnCurrentShape();
            var shapeSpawnPoint = _shapeService.SpawnShape(View.transform);
            View.Player.transform.DOLookAt(shapeSpawnPoint, _playerSettingsDatabase.Settings.LookAtAnimationDurationS);
        }

        private void StartSpawningBullets()
        {
            _spawnSubscription?.Dispose();

            _spawnSubscription = Observable
                .Interval(TimeSpan.FromSeconds(_playerSettingsDatabase.Settings.ShootingIntervalS))
                .Subscribe(_ => LaunchBullet()).AddTo(_disposable);
        }

        public void TurnInDirection(Vector2 moveVector)
        {
            var horizontalRotation = moveVector.x * _playerSettingsDatabase.Settings.RotationSpeed * Time.deltaTime;
            var verticalRotation = -moveVector.y * _playerSettingsDatabase.Settings.RotationSpeed * Time.deltaTime;

            View.Player.Rotate(Vector3.up, horizontalRotation, Space.World);
            View.Player.Rotate(Vector3.right, verticalRotation, Space.Self);
        }

        private void LaunchBullet()
        {
            var bullet = _bulletService.SpawnBullet(View.BulletSpawnTransform);
            bullet.Launch(_bulletSettingsDatabase.Settings.BulletVelocity, View.BulletSpawnTransform.forward);
        }
    }
}