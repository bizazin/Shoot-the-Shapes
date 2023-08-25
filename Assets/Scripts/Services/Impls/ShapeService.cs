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
            var spawnPoint = CalculateSpawnPoint(player);
            SpawnShapeAtPoint(spawnPoint, player);
            return spawnPoint;
        }

        public void DestroyCurrentShape(ShapeComponentBehaviour shapeComponent, Vector3 hitPosition)
        {
            EnablePhysicsForShapeComponents();
            ApplyForceToShapeComponent(shapeComponent, hitPosition);
        }

        public void DespawnCurrentShape()
        {
            if (_currentShapeComponents == null)
                return;

            foreach (var shapeComponentBehaviour in _currentShapeComponents)
                ResetShapeComponent(shapeComponentBehaviour);
        }

        private Vector3 CalculateSpawnPoint(Transform player)
        {
            var directionFromPlayer = GenerateDirectionFromPlayer(player);
            var distance = Random.Range(_shapeSettingsDatabase.Settings.SpawnRadius.x,
                _shapeSettingsDatabase.Settings.SpawnRadius.y);

            return player.position + directionFromPlayer * distance;
        }

        private Vector3 GenerateDirectionFromPlayer(Transform player)
        {
            Vector3 directionFromPlayer;
            float angle;

            do
            {
                directionFromPlayer = GetRandomPointOnHemisphere() - player.position;
                angle = Vector3.Angle(player.forward, directionFromPlayer.normalized);
            } while (angle < _shapeSettingsDatabase.Settings.VerticalViewAngleDeg.x ||
                     angle > _shapeSettingsDatabase.Settings.VerticalViewAngleDeg.y);

            return directionFromPlayer.normalized;
        }

        private Vector3 GetRandomPointOnHemisphere()
        {
            Vector3 randomPoint;
            do randomPoint = Random.onUnitSphere;
            while (randomPoint.y < 0);

            return randomPoint;
        }

        private void SpawnShapeAtPoint(Vector3 spawnPoint, Transform player)
        {
            var randomShapeType = EnumExtensions.GetRandomValue<EShapeType>();

            if (_shapeSpawnStrategies.TryGetValue(randomShapeType, out var randomStrategy))
                _currentShapeComponents = randomStrategy.Spawn(player, spawnPoint);
        }

        private void EnablePhysicsForShapeComponents()
        {
            foreach (var shapeComponentBehaviour in _currentShapeComponents)
            {
                shapeComponentBehaviour.Rigidbody.isKinematic = false;
                shapeComponentBehaviour.Rigidbody.WakeUp();
            }
        }

        private void ApplyForceToShapeComponent(ShapeComponentBehaviour shapeComponent, Vector3 hitPosition)
        {
            var rigidbody = shapeComponent.Rigidbody;
            rigidbody.AddForceAtPosition(
                (rigidbody.position - hitPosition).normalized * _shapeSettingsDatabase.Settings.HitForce, hitPosition);
        }

        private void ResetShapeComponent(ShapeComponentBehaviour shapeComponentBehaviour)
        {
            shapeComponentBehaviour.transform.localEulerAngles = Vector3.zero;
            shapeComponentBehaviour.Rigidbody.isKinematic = true;
            _randomShapeComponentPool.Despawn(shapeComponentBehaviour);
        }
    }
}
