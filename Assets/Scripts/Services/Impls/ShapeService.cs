using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using ShapeSpawnStrategies;
using Signals;
using UniRx;
using Zenject;

namespace Services.Impls
{
    public class ShapeService : IShapeService, IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly Dictionary<EShapeType, IShapeSpawnStrategy> _shapeSpawnStrategies;
        private readonly CompositeDisposable _disposable = new();

        public ShapeService
        (
            List<IShapeSpawnStrategy> shapeSpawnStrategies,
            SignalBus signalBus
        )
        {
            _signalBus = signalBus;
            _shapeSpawnStrategies = shapeSpawnStrategies.ToDictionary(strategy => strategy.ShapeType);
        }

        public void Initialize()
        {
            _signalBus.GetStream<SignalBulletHitShape>().Subscribe(_ => DestroyCurrentShape()).AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();

        private void DestroyCurrentShape()
        {
        }
    }
}