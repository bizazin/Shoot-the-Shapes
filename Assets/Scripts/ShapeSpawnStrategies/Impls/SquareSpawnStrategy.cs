using System.Collections.Generic;
using Databases;
using Enums;
using ObjectPooling.Objects;
using ObjectPooling.Pools;
using UnityEngine;

namespace ShapeSpawnStrategies.Impls
{
    public class SquareSpawnStrategy : AShapeSpawnStrategy
    {
        private readonly IShapeSettingsDatabase _shapeSettingsDatabase;
        
        public override EShapeType ShapeType => EShapeType.Square;

        public SquareSpawnStrategy
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
            var gridSize = _shapeSettingsDatabase.SquareSettings.GridSize;
    
            CurrentShapeComponents.Clear();
            for (var x = -gridSize; x < gridSize; x++)
                for (var y = -gridSize; y < gridSize; y++)
                {
                    var halfSize = _shapeSettingsDatabase.Settings.StandardComponentSize;
                    var position = new Vector3(spawnPoint.x + x + halfSize, spawnPoint.y + y + halfSize, spawnPoint.z); 

                    SpawnAndAddComponent(parent, position);
                }

            return CurrentShapeComponents;
        }
    }
}