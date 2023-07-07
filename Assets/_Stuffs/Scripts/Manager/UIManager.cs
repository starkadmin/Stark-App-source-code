using UnityEngine;
using System;
using Stark.Helper.Enum;
using Stark.Controller;
using Stark.Core;
using Stark.UI;

namespace Stark.Manager
{
    public class UIManager : MonoBehaviour
    {   
        public static UIManager instance;
        public ScreenEnum.MainScreenType currentScreen;

        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Main Screens")]
        [SerializeField]private GameObject[] m_main;
        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Customization Screens")]
        [SerializeField]private GameObject[] m_customization;
        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Prompts UI")]
        [SerializeField]private GameObject[] m_prompts;

        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Reference")]
        [SerializeField]private CameraController m_cameraController;
        [SerializeField]private PageTransition m_pageTransition;
        [SerializeField]private GlowEffectController m_glowEffect;
        public GameObject _platform;
        
        public static Action<int> GenderSelected;
        public static Action<ScreenEnum.MainScreenType> PageTransitionEnded;

        void Awake()
        {
            instance=this;
            Application.targetFrameRate = 60;
        }

    #region Button Methods
        public void SelectMaleGender(){
            OnGenderSelected(1);
        }
        public void SelectFemaleGender(){
            OnGenderSelected(0);
        }
        public void CustomizationPage(){
            SwitchMainScreens(ScreenEnum.MainScreenType.Customization);
        } 
        public void CustomizeMain(){
            m_glowEffect.EnableGlowEffect(true);
            SwitchCustomizationScreens(ScreenEnum.CustomizationScreenType.Default);
            m_cameraController.SetCameraPosition(CameraTargetType.Default);
        }
        public void CustomizeHead(){
            m_glowEffect.EnableGlowEffect(false);
            SwitchCustomizationScreens(ScreenEnum.CustomizationScreenType.Head);
            m_cameraController.SetCameraPosition(CameraTargetType.Head);
        }
        public void CustomizeUpperBody(){
            m_glowEffect.EnableGlowEffect(false);
            SwitchCustomizationScreens(ScreenEnum.CustomizationScreenType.UpperBody);
            m_cameraController.SetCameraPosition(CameraTargetType.UpperBody);
        }
        public void CustomizeLowerBody(){
             m_glowEffect.EnableGlowEffect(false);
            SwitchCustomizationScreens(ScreenEnum.CustomizationScreenType.LowerBody);
            m_cameraController.SetCameraPosition(CameraTargetType.LowerBody);
        }
        public void CustomizeFootwear(){
             m_glowEffect.EnableGlowEffect(false);
            SwitchCustomizationScreens(ScreenEnum.CustomizationScreenType.Footwear);
            m_cameraController.SetCameraPosition(CameraTargetType.Footwear);
        }
        public void ResetCameraPosition(){
            m_cameraController.SetCameraPosition(CameraTargetType.Default);
        }
        public void EnableDisablePlayer(bool isEnable){
            DataManager.Instance.ActiveAvatar.SetActive(isEnable);
            _platform.SetActive(isEnable);
        }
        
        public void SaveSuccessAnim(){
            DataManager.Instance.ActiveAvatar.GetComponent<Customization>().DoneAnimation();
        }

        public void LoadDirectHome()
        {
            SwitchMainScreens(ScreenEnum.MainScreenType.Home);
            _platform.SetActive(true);
        }

        #endregion


    #region Page Transitions
        public void GenderPageTransition(){
            DoPageTransition(ScreenEnum.MainScreenType.Customization);
        }
    #endregion


    #region Event Methods
        public void OnGenderSelected(int gender){
            GenderSelected?.Invoke(gender);
        }
    #endregion
        /// <summary>
        /// Switching customization UI
        /// Sorted by array index
        /// </summary>
        public void SwitchCustomizationScreens(ScreenEnum.CustomizationScreenType screenType){
            for (int i = 0; i < m_customization.Length; i++) { m_customization[i].gameObject.SetActive(i == ((int)screenType)); }
            AttireController.OnAttireSetupCount?.Invoke();
        }
        public void SwitchCustomizationScreens(int index){
            for (int i = 0; i < m_customization.Length; i++) { m_customization[i].gameObject.SetActive(i == ((int)index)); }
        }
        /// <summary>
        /// Switching main UI
        /// Sorted by array index
        /// </summary>
        public void SwitchMainScreens(ScreenEnum.MainScreenType screenType){
            currentScreen = screenType;
            for (int i = 0; i < m_main.Length; i++) { 
                m_main[i].gameObject.SetActive(i == ((int)screenType));
            }
        }

        public void SwitchMainScreens(int index)
        {
            currentScreen = (ScreenEnum.MainScreenType)index;
            for (int i = 0; i < m_main.Length; i++)
            {
                m_main[i].gameObject.SetActive(i == index);
            }
        }

        public void SwitchPrompts(int index){
            for (int i = 0; i < m_prompts.Length; i++) { m_prompts[i].gameObject.SetActive(i == index); }
        }

        public void DoPageTransition(ScreenEnum.MainScreenType screenType){
            StartCoroutine(m_pageTransition.TransitionRoutine(screenType));
        }
    }
}
