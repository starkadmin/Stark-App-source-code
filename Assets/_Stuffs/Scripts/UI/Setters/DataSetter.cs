using TMPro;
using Stark.Manager;
using UnityEngine;
using Stark.SO;

namespace Stark.UI
{
    public class DataSetter : MonoBehaviour
    {
        [SerializeField] private DataSetterType dataType;
        private TextMeshProUGUI m_text;
        private PlayerSO m_playerData;
        void Awake()
        {
            m_playerData = DataManager.Instance.PlayerData;
            m_text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            if (dataType == DataSetterType.name)
                m_text.text = m_playerData.PlayerModel.fullName;
            else if (dataType == DataSetterType.email)
                m_text.text = m_playerData.PlayerModel.email;
        }
    }
}


public enum DataSetterType
{
    name,
    email,
}
