using System.Collections.Generic;
using Databases;
using Enums;
using ObjectPooling.Objects;
using ObjectPooling.Pools;
using UnityEngine;

namespace ShapeSpawnStrategies.Impls
{
    public class CircleSpawnStrategy : AShapeSpawnStrategy
    {
        private readonly IShapeSettingsDatabase _shapeSettingsDatabase;

        public override EShapeType ShapeType => EShapeType.Circle;

        public CircleSpawnStrategy
        (
            IRandomShapeComponentPool randomShapeComponentPool,
            IShapeSettingsDatabase shapeSettingsDatabase,
            IColorSettingsDatabase colorSettingsDatabase
        ) : base(randomShapeComponentPool, shapeSettingsDatabase, colorSettingsDatabase)
        {
            _shapeSettingsDatabase = shapeSettingsDatabase;
        }

        public override List<ShapeComponentBehaviour> Spawn(Transform parent, Vector3 spawnPoint)
        {
            var radius = _shapeSettingsDatabase.CircleSettings.Radius;

            CurrentShapeComponents.Clear();
            for (var x = -radius; x < radius; x++)
                for (var y = -radius; y < radius; y++)
                {
                    var halfSize = _shapeSettingsDatabase.Settings.StandardComponentSize;
                    var position = new Vector3(x + halfSize + spawnPoint.x, y + halfSize + spawnPoint.y, spawnPoint.z);

                    if (Vector3.Distance(spawnPoint, position) <= radius) 
                        SpawnAndAddComponent(parent, position);
                }

            return CurrentShapeComponents;
        }
    }
}