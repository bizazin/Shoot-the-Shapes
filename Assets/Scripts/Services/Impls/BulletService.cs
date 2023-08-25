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

        public BulletService
        (
            IBulletPool bulletPool,
            IBulletSettingsDatabase bulletSettingsDatabase,
            SignalBus signalBus
        )
        {
            _bulletPool = bulletPool;
            _bulletSettingsDatabase = bulletSettingsDatabase;
            _signalBus = signalBus;
        }

        public BulletBehaviour SpawnBullet(Transform spawnTransform)
        {
            var bullet = _bulletPool.Spawn(spawnTransform);
            bullet.transform.position = spawnTransform.position;

            var despawnTimer = Observable
                .Timer(TimeSpan.FromSeconds(_bulletSettingsDatabase.Settings.MaxBulletLifetimeS))
                .Subscribe(_ => DespawnBullet(bullet)).AddTo(bullet);
            var hitAction = CreateHitAction(bullet, despawnTimer);

            bullet.CurrentHitAction = hitAction;
            bullet.OnShapeHit += hitAction;

            return bullet;
        }

        private Action CreateHitAction(BulletBehaviour bullet, IDisposable despawnTimer) =>
            () =>
            {
                despawnTimer.Dispose();
                DespawnBullet(bullet);
                _signalBus.Fire(new SignalBulletHitShape());
            };

        private void DespawnBullet(BulletBehaviour bullet)
        {
            if (bullet.CurrentHitAction != null)
                bullet.OnShapeHit -= bullet.CurrentHitAction;
            _bulletPool.Despawn(bullet);
        }
    }
}