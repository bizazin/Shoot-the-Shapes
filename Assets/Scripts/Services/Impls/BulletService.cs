using System;
using Databases;
using ObjectPooling.Objects;
using ObjectPooling.Pools;
using Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace Services.Impls
{
    public class BulletService : IBulletService
    {
        private readonly IBulletPool _bulletPool;
        private readonly IBulletSettingsDatabase _bulletSettingsDatabase;
        private readonly SignalBus _signalBus;
        private readonly IColorSettingsDatabase _colorSettingsDatabase;

        public BulletService
        (
            IBulletPool bulletPool,
            IBulletSettingsDatabase bulletSettingsDatabase,
            SignalBus signalBus,
            IColorSettingsDatabase colorSettingsDatabase
        )
        {
            _bulletPool = bulletPool;
            _bulletSettingsDatabase = bulletSettingsDatabase;
            _signalBus = signalBus;
            _colorSettingsDatabase = colorSettingsDatabase;
        }

        public BulletBehaviour SpawnBullet(Transform spawnTransform)
        {
            var bullet = _bulletPool.Spawn(spawnTransform);
            bullet.transform.position = spawnTransform.position;
            bullet.Renderer.material.color = _colorSettingsDatabase.GetRandomColor();
            AttachHitActionToBullet(bullet);
            
            return bullet;
        }
        
        private void AttachHitActionToBullet(BulletBehaviour bullet)
        {
            var despawnTimer = SetupBulletDespawnTimer(bullet);

            var hitAction = CreateHitAction(bullet, despawnTimer);

            bullet.CurrentHitAction = hitAction;
            bullet.OnShapeHit += hitAction;
        }

        private IDisposable SetupBulletDespawnTimer(BulletBehaviour bullet) =>
            Observable
                .Timer(TimeSpan.FromSeconds(_bulletSettingsDatabase.Settings.MaxBulletLifetimeS))
                .Subscribe(_ => DespawnBullet(bullet)).AddTo(bullet);

        private Action<ShapeComponentBehaviour, Vector3> CreateHitAction(BulletBehaviour bullet, IDisposable despawnTimer) =>
            (hitShapeComponent, hitPosition) =>
            {
                despawnTimer.Dispose();
                DespawnBullet(bullet);
                _signalBus.Fire(new SignalBulletHitShape(hitShapeComponent, hitPosition));
            };

        private void DespawnBullet(BulletBehaviour bullet)
        {
            if (bullet.CurrentHitAction != null)
                bullet.OnShapeHit -= bullet.CurrentHitAction;

            _bulletPool.Despawn(bullet);
        }
    }
}
