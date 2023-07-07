using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine;

namespace Stark.UI
{
    public class DropdownContent : MonoBehaviour
    {   
        [TextArea]
        public string Content;
        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Reference")]
        [SerializeField]private Sprite[] m_icons;
        [SerializeField]private Image m_dropdownBtn;
        [SerializeField]private GameObject m_contentContainer;
        [SerializeField]private TextMeshProUGUI m_contentText;

        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Events")]
        public UnityEvent OnEnable;
        public UnityEvent OnDisable;

        public void ShowHideContent(){
            if(m_contentContainer.gameObject.activeSelf){
                OnDisable.Invoke();
                m_dropdownBtn.sprite =m_icons[0];
                m_contentContainer.SetActive(false);
                if(m_contentText == null) return;
                if(Content != null) m_contentText.text = "";
                
            }
            else{
                OnEnable.Invoke();
                m_dropdownBtn.sprite =m_icons[1];
                m_contentContainer.SetActive(true);
                if(m_contentText == null) return;
                if(Content != null) m_contentText.text = Content;
            }
        }

        

    }
}
