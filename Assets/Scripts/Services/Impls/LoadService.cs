using Databases;
using Jsons;
using Jsons.Values;
using UnityEngine;
using Zenject;

namespace Services.Impls
{
    public class LoadService : ILoadService, IInitializable
    {
        private readonly IColorSettingsDatabase _colorSettingsDatabase;

        public LoadService
        (
            IColorSettingsDatabase colorSettingsDatabase
        )
        {
            _colorSettingsDatabase = colorSettingsDatabase;
        }
        
        public void Initialize() => LoadColorsFromJson();

        void LoadColorsFromJson()
        {
            var jsonFile = Resources.Load<TextAsset>(Paths.Colors);
            var colorData = JsonUtility.FromJson<ColorValue>(jsonFile.text);

            foreach (var hexColor in colorData.colors)
                if (ColorUtility.TryParseHtmlString(hexColor, out var newColor))
                    _colorSettingsDatabase.Colors.Add(newColor);
        }
    }
}