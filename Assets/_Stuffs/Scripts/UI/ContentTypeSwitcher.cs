
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Stark
{
    public class ContentTypeSwitcher : MonoBehaviour
    {
        [SerializeField] private Image m_eyeIcon;
        [SerializeField] private TMP_InputField m_InputField;

        
        public void SwitchContentype(){
            if(m_InputField.contentType.Equals(TMP_InputField.ContentType.Standard)){
                Debug.Log($"Tesst 1: "+m_InputField.contentType);
                m_InputField.contentType = TMP_InputField.ContentType.Password;
                m_eyeIcon.color = new Color(m_eyeIcon.color.r, m_eyeIcon.color.g, m_eyeIcon.color.b, 0.5f);
            }else{
                Debug.Log($"Tesst 2: "+m_InputField.contentType);
                m_InputField.contentType = TMP_InputField.ContentType.Standard;
                m_eyeIcon.color = new Color(m_eyeIcon.color.r, m_eyeIcon.color.g, m_eyeIcon.color.b, 1f);
            }
            m_InputField.ForceLabelUpdate();

        }

    }
}
