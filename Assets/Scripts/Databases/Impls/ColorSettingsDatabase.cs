using System.Collections.Generic;
using UnityEngine;

namespace Databases.Impls
{
    public class ColorSettingsDatabase : IColorSettingsDatabase
    {
        public List<Color> Colors { get; } = new();
        
        public Color GetRandomColor()
        {
            if (Colors.Count == 0) return Color.white; // Default to white if no colors available
            return Colors[Random.Range(0, Colors.Count)];
        }
    }
}