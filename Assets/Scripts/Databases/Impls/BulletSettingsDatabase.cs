using Models;
using UnityEngine;

namespace Databases.Impls
{
    [CreateAssetMenu(menuName = "Databases/BulletSettingsDatabase", fileName = "BulletSettingsDatabase")]
    public class BulletSettingsDatabase : ScriptableObject, IBulletSettingsDatabase
    {
        [SerializeField] private BulletSettingsVo _bulletSettings;

        public BulletSettingsVo Settings => _bulletSettings;
    }
}