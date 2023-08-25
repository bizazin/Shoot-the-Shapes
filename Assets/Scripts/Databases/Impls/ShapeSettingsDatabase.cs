using Models;
using Models.Shapes;
using UnityEngine;

namespace Databases.Impls
{
    [CreateAssetMenu(menuName = "Databases/ShapeSettingsDatabase", fileName = "ShapeSettingsDatabase")]
    public class ShapeSettingsDatabase : ScriptableObject, IShapeSettingsDatabase
    {
        [Header("Main")] 
        [SerializeField] private ShapeSettingsVo _shapeSettings;
        [Header("Shapes")]
        [SerializeField] private CircleSettingsVo _circleSettings;
        [SerializeField] private PyramidSettingsVo _pyramidSettings;
        [SerializeField] private RectangleSettingsVo _rectangleSettings;
        [SerializeField] private SquareSettingsVo _squareSettings;

        public ShapeSettingsVo Settings => _shapeSettings;
        public CircleSettingsVo CircleSettings => _circleSettings;
        public PyramidSettingsVo PyramidSettings => _pyramidSettings;
        public RectangleSettingsVo RectangleSettings => _rectangleSettings;
        public SquareSettingsVo SquareSettings => _squareSettings;
    }
}