using System.Collections.Generic;
using System.Linq;
using Databases;
using Enums;
using Extensions;
using ObjectPooling.Objects;
using ObjectPooling.Pools;
using ShapeSpawnStrategies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services.Impls
{
    public class ShapeService : IShapeService
    {
        private readonly IShapeSettingsDatabase _shapeSettingsDatabase;
        private readonly IRandomShapeComponentPool _randomShapeComponentPool;
        private readonly Dictionary<EShapeType, IShapeSpawnStrategy> _shapeSpawnStrategies;
        private List<ShapeComponentBehaviour> _currentShapeComponents = new();

        public ShapeService
        (
            List<IShapeSpawnStrategy> shapeSpawnStrategies,
            IShapeSettingsDatabase shapeSettingsDatabase,
            IRandomShapeComponentPool randomShapeComponentPool
        )
        {
            _shapeSettingsDatabase = shapeSettingsDatabase;
            _randomShapeComponentPool = randomShapeComponentPool;
            _shapeSpawnStrategies = shapeSpawnStrategies.ToDictionary(strategy => strategy.ShapeType);
        }

        public Vector3 SpawnShape(Transform player)
        {
            Vector3 directionFromPlayer;
            float angle;

            do
            {
                Vector3 randomPointOnHemisphere;
                do
                {
                    randomPointOnHemisphere = Random.onUnitSphere;
                } while (randomPointOnHemisphere.y < 0);

                directionFromPlayer = (randomPointOnHemisphere - player.position).normalized;

                angle = Vector3.Angle(player.forward, directionFromPlayer);
            } while (angle < _shapeSettingsDatabase.Settings.VerticalViewAngleDeg.x ||
                     angle > _shapeSettingsDatabase.Settings.VerticalViewAngleDeg.y);

            var distance = Random.Range(_shapeSettingsDatabase.Settings.SpawnRadius.x,
                _shapeSettingsDatabase.Settings.SpawnRadius.y);

            var spawnPoint = player.position + directionFromPlayer * distance;

            var randomShapeType = EnumExtensions.GetRandomValue<EShapeType>();

            if (_shapeSpawnStrategies.TryGetValue(randomShapeType, out var randomStrategy))
                _currentShapeComponents = randomStrategy.Spawn(player, spawnPoint);

            return spawnPoint;
        }

        public void DestroyCurrentShape(ShapeComponentBehaviour shapeComponent, Vector3 hitPosition)
        {
            foreach (var shapeComponentBehaviour in _currentShapeComponents)
            {
                shapeComponentBehaviour.Rigidbody.isKinematic = false;
                shapeComponentBehaviour.Rigidbody.WakeUp();
            }

            var rigidbody = shapeComponent.Rigidbody;
            rigidbody.AddForceAtPosition(
                (rigidbody.position - hitPosition).normalized * _shapeSettingsDatabase.Settings.HitForce, hitPosition);
        }

        public void DespawnCurrentShape()
        {
            if (_currentShapeComponents == null)
                return;
            foreach (var shapeComponentBehaviour in _currentShapeComponents)
            {
                shapeComponentBehaviour.transform.localEulerAngles = Vector3.zero;
                shapeComponentBehaviour.Rigidbody.isKinematic = true;
                _randomShapeComponentPool.Despawn(shapeComponentBehaviour);
            }
        }
    }
}