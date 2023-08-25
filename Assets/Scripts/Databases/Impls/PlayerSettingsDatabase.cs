using Models;
using UnityEngine;

namespace Databases.Impls
{
    [CreateAssetMenu(menuName = "Databases/PlayerSettingsDatabase", fileName = "PlayerSettingsDatabase")]
    public class PlayerSettingsDatabase : ScriptableObject, IPlayerSettingsDatabase
    {
        [SerializeField] private PlayerSettingsVo _playerSettings;

        public PlayerSettingsVo Settings => _playerSettings;
    }
}