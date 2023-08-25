using Enums;
using ObjectPooling.Core;
using ObjectPooling.Objects;

namespace ObjectPooling.Pools.Impls
{
    public class CubeComponentPool : Pool<ShapeComponentBehaviour>, IShapeComponentPool, ICubeComponentPool
    {
        public EShapeComponentType ShapeType => EShapeComponentType.Cube;
    }
}