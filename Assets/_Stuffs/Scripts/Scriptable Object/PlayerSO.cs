using Stark.Model;
using UnityEngine;

namespace Stark.SO
{
    [CreateAssetMenu(fileName = "Player", menuName = "Stark/Player")]
    public class PlayerSO : ScriptableObject
    {
        [field: SerializeField] public UserModel.PlayerModel PlayerModel { get; set; }
    }
}
