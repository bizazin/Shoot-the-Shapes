using System.Collections.Generic;
using UnityEngine;

namespace Databases
{
    public interface IColorSettingsDatabase
    {
        List<Color> Colors { get; }
        Color GetRandomColor();
    }
}