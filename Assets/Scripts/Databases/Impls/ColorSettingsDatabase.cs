using System.Collections.Generic;
using UnityEngine;

namespace Databases.Impls
{
    public class ColorSettingsDatabase : IColorSettingsDatabase
    {
        public List<Color> Colors { get; } = new();
        
        public Color GetRandomColor()
        {
            if (Colors.Count == 0) 
                return Color.white;
            return Colors[Random.Range(0, Colors.Count)];
        }
    }
}