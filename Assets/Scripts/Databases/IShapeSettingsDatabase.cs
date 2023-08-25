using Databases.Impls;
using Models;
using Models.Shapes;

namespace Databases
{
    public interface IShapeSettingsDatabase
    {
        ShapeSettingsVo Settings { get; }
        CircleSettingsVo CircleSettings { get; }
        PyramidSettingsVo PyramidSettings { get; }
        RectangleSettingsVo RectangleSettings { get; }
        SquareSettingsVo SquareSettings { get; }
    }
}