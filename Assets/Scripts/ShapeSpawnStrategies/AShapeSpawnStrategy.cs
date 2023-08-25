using System;
using System.Collections.Generic;
using Databases;
using Enums;
using ObjectPooling.Objects;
using ObjectPooling.Pools;
using UnityEngine;

namespace ShapeSpawnStrategies
{
    public abstract class AShapeSpawnStrategy : IShapeSpawnStrategy
    {
        private readonly IRandomShapeComponentPool _randomShapeComponentPool;
        private readonly IShapeSettingsDatabase _shapeSettingsDatabase;
        private readonly IColorSettingsDatabase _colorSettingsDatabase;

        public abstract EShapeType ShapeType { get; }
        protected readonly List<ShapeComponentBehaviour> CurrentShapeComponents = new();

        protected AShapeSpawnStrategy
        (
            IRandomShapeComponentPool randomShapeComponentPool,
            IShapeSettingsDatabase shapeSettingsDatabase,
            IColorSettingsDatabase colorSettingsDatabase
        )
        {
            _randomShapeComponentPool = randomShapeComponentPool;
            _shapeSettingsDatabase = shapeSettingsDatabase;
            _colorSettingsDatabase = colorSettingsDatabase;
        }

        public abstract List<ShapeComponentBehaviour> Spawn(Transform parent, Vector3 spawnPoint);

        protected void SpawnAndAddComponent(Transform parent, Vector3 position)
        {
            var shapeComponentBehaviour = _randomShapeComponentPool.Spawn(parent);
            if (shapeComponentBehaviour.transform.localScale != new Vector3(
                    _shapeSettingsDatabase.Settings.StandardComponentSize,
                    _shapeSettingsDatabase.Settings.StandardComponentSize,
                    _shapeSettingsDatabase.Settings.StandardComponentSize))
                throw new Exception(
                    $"[{nameof(AShapeSpawnStrategy)}] Shape component size is not equal to {_shapeSettingsDatabase.Settings.StandardComponentSize}");

            shapeComponentBehaviour.Renderer.material.color = _colorSettingsDatabase.GetRandomColor();
            CurrentShapeComponents.Add(shapeComponentBehaviour);
            shapeComponentBehaviour.transform.position = position;
        }
    }
}