using Services.Impls;
using ShapeSpawnStrategies.Impls;
using Signals;
using Zenject;

namespace Installers.Main
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSignals();
            BindServices();
            BindShapeStrategies();
        }

        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<SignalBulletHitShape>();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<BulletService>().AsSingle();
            Container.BindInterfacesTo<ShapeService>().AsSingle();
        }

        private void BindShapeStrategies()
        {
            Container.BindInterfacesTo<CircleSpawnStrategy>().AsSingle();
            Container.BindInterfacesTo<RectangleSpawnStrategy>().AsSingle();
            Container.BindInterfacesTo<PyramidSpawnStrategy>().AsSingle();
            Container.BindInterfacesTo<SquareSpawnStrategy>().AsSingle();
        }
    }
}