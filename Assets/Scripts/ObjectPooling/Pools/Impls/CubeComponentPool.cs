using Enums;
using ObjectPooling.Core;
using ObjectPooling.Objects;

namespace ObjectPooling.Pools.Impls
{
    public class CubeComponentPool : Pool<ShapeComponentBehaviour>, ICubeComponentPool
    {
        public EShapeComponentType ShapeType => EShapeComponentType.Cube;
    }
}