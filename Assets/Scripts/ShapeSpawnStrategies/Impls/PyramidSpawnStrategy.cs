using System.Collections.Generic;
using Databases;
using Enums;
using ObjectPooling.Objects;
using ObjectPooling.Pools;
using UnityEngine;

namespace ShapeSpawnStrategies.Impls
{
    public class PyramidSpawnStrategy : AShapeSpawnStrategy
    {
        private readonly IShapeSettingsDatabase _shapeSettingsDatabase;

        public override EShapeType ShapeType => EShapeType.Pyramid;

        public PyramidSpawnStrategy
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
            var baseSize = _shapeSettingsDatabase.PyramidSettings.BaseSize;
            
            CurrentShapeComponents.Clear();
            for (var y = 0; y < baseSize; y++)
            {
                var currentBaseSize = baseSize - y;
                var halfBaseSize = currentBaseSize / 2;

                for (var x = -halfBaseSize; x < halfBaseSize; x++)
                    for (var z = -halfBaseSize; z < halfBaseSize; z++)
                    {
                        var centerPos = spawnPoint;
                        var position = new Vector3(x + centerPos.x, y + centerPos.y, z + centerPos.z);

                        SpawnAndAddComponent(parent, position);
                    }

            }
            return CurrentShapeComponents;
        }
    }
}