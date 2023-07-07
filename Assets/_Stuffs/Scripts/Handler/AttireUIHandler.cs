using UnityEngine;
using Stark.Model;
using UnityEngine.UI;
using Stark.Controller;
using Stark.UI;
using Stark.Helper.Enum;

namespace Stark.Handler
{
    public class AttireUIHandler : MonoBehaviour
    {
        [SerializeField]private RuntimeEnum.AttireType _attireType;
        [SerializeField]private CustomizationItemSwitcher[] itemSwitcher;
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
              
            }    
        }

    }
}
