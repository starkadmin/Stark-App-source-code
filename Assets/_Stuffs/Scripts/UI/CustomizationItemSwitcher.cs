using System.Diagnostics;
using TMPro;
using UnityEngine;
using Stark.Model;
using UnityEngine.UI;
using Stark.Controller;
using Stark.Helper.Enum;

namespace Stark.UI{
    public class CustomizationItemSwitcher : MonoBehaviour
    {
        [SerializeField]private RuntimeEnum.AttireType _attireType;

        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Reference")]
        // [SerializeField] private CustomizationItems m_customizationItems;
        [SerializeField] private TextMeshProUGUI m_indexTxt;
        [SerializeField] private CanvasGroup m_nextBtn;
        [SerializeField] private CanvasGroup m_previousBtn;

        [SerializeField] private int _totalItems=1; // TODO: ADD TOTAL Item

        private void OnEnable()
        {
            AttireController.OnAttireIndexCount += OnAttireIndexCount;
        }
        
        private void OnDisable()
        {
            AttireController.OnAttireIndexCount -= OnAttireIndexCount;
        }

        private void OnAttireIndexCount(AttireModel.AttireCountData attire)
        {
            switch (_attireType)
            {
                case RuntimeEnum.AttireType.HAIR:
                        UpdateUI(attire.hair);
                    break;
                case RuntimeEnum.AttireType.EYESIZE:
                        UpdateUI(attire.blendShapes.headBlendShapes.totalEyes);
                    break;
                case RuntimeEnum.AttireType.NOSESIZE:
                        UpdateUI(attire.blendShapes.headBlendShapes.totalNose);
                    break;
                case RuntimeEnum.AttireType.LIPSSIZE:
                        UpdateUI(attire.blendShapes.headBlendShapes.totalLips);
                    break;
                case RuntimeEnum.AttireType.BODYTYPE:
                        UpdateUI(attire.bodytype);
                    break;
                case RuntimeEnum.AttireType.CLOTHES:
                        UpdateUI(attire.clothes);
                    break;
                case RuntimeEnum.AttireType.PANTS:
                        UpdateUI(attire.pants);
                    break;
                case RuntimeEnum.AttireType.FOOTWEAR:
                        UpdateUI(attire.footware);
                    break;
                case RuntimeEnum.AttireType.SKINTONE:
                        UpdateUI(attire.skin);
                    break;
                
            }
        }
        private void UpdateUI(int index){
            var curIndex=index + 1;
            m_indexTxt.text =curIndex.ToString();
            if(curIndex >=_totalItems) m_nextBtn.interactable=false;
            else m_nextBtn.interactable=true;
            if(index <= 0) m_previousBtn.interactable=false;
            else m_previousBtn.interactable=true;
        }

    }
}
