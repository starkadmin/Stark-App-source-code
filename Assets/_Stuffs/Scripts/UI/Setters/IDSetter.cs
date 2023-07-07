using TMPro;
using Stark.Manager;
using UnityEngine;
using Stark.SO;

namespace Stark.UI
{
    public class IDSetter : MonoBehaviour
    {
        private TextMeshProUGUI m_text;
        private PlayerSO m_playerData;
        void Awake()
        {
            m_playerData = DataManager.Instance.PlayerData;
            m_text = GetComponent<TextMeshProUGUI>();
        }
        private void OnEnable()
        {
            m_text.text = "ID: "+ m_playerData.PlayerModel.agentId;
        }
    }
}
