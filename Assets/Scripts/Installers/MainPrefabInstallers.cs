using Controllers.Impls;
using Databases;
using Databases.Impls;
using Extensions;
using ObjectPooling.Objects;
using ObjectPooling.Pools;
using ObjectPooling.Pools.Impls;
using UnityEngine;
using Views;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Installers/MainPrefabInstallers", fileName = "MainPrefabInstallers")]
    public class MainPrefabInstallers : ScriptableObjectInstaller
    { 
        [Header("Prefabs")]
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private BulletBehaviour _bulletBehaviour;
        [SerializeField] private ShapeComponentBehaviour _cubeShapeComponentBehaviour;
        [SerializeField] private ShapeComponentBehaviour _ballShapeComponentBehaviour;


        [Header("Databases")]
        [SerializeField] private PlayerSettingsDatabase _playerSettingsDatabase;

        [SerializeField] private BulletSettingsDatabase _bulletSettingsDatabase;

        public override void InstallBindings()
        {
            BindViews();
            BindObjectPools();
            BindDatabases();
        }

        private void BindViews()
        {
            Container.BindView<PlayerController, PlayerView>(_playerView);
        }

        private void BindObjectPools()
        {
            BindPool<BulletBehaviour, BulletPool, IBulletPool>(_bulletBehaviour, 11);
            BindPool<ShapeComponentBehaviour, CubeComponentPool, ICubeComponentPool>(_cubeShapeComponentBehaviour, 100);
            BindPool<ShapeComponentBehaviour, BallComponentPool, IBallComponentPool>(_ballShapeComponentBehaviour, 100);
            Container.BindInterfacesTo<RandomShapeComponentPool>().AsSingle();
        }

        private void BindDatabases()
        {
            Container.Bind<IPlayerSettingsDatabase>().FromInstance(_playerSettingsDatabase).AsSingle();
            Container.Bind<IBulletSettingsDatabase>().FromInstance(_bulletSettingsDatabase).AsSingle();
        }

        private void BindPool<TItemContract, TPoolConcrete, TPoolContract>(TItemContract prefab, int size)
            where TItemContract : MonoBehaviour
            where TPoolConcrete : TPoolContract, IMemoryPool
            where TPoolContract : IMemoryPool
        {
            var poolContainerName = "[Pool] " + prefab;
            Container.BindMemoryPoolCustomInterface<TItemContract, TPoolConcrete, TPoolContract>()
                .WithInitialSize(size)
                .FromComponentInNewPrefab(prefab)
#if UNITY_EDITOR
                .UnderTransformGroup(poolContainerName)
#endif
                .AsCached()
                .OnInstantiated((_, item) => (item as MonoBehaviour)?.gameObject.SetActive(false));
        }

    }
}