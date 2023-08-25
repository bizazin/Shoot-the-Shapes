using Controllers.Impls;
using Extensions;
using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Installers.Main
{
    [CreateAssetMenu(menuName = "Installers/MainUiInstaller", fileName = "MainUiInstaller")]
    public class MainUiInstaller : ScriptableObjectInstaller
    {
        [Header("Canvas")] 
        [SerializeField] private Canvas _canvas;
        
        [Header("Views")] 
        [SerializeField] private Joystick _joystick;
        
        public override void InstallBindings()
        {
            Container.Bind<CanvasScaler>().FromComponentInNewPrefab(_canvas).AsSingle();
            var parent = Container.Resolve<CanvasScaler>().transform;
            Container.BindView<JoystickController, Joystick>(_joystick, parent);
        }
    }
}