using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using Stark.Manager;
using UnityEngine;


namespace Stark
{
    public class TutorialBtnEnabler : MonoBehaviour
    {
        [SerializeField]private Button[] m_mainButton;
        [SerializeField]private GameObject m_tutorialButtons;
        [SerializeField]private GameObject m_normalButtons;
        [SerializeField]private GameObject m_genderButton;
        
        void OnEnable()
        {
            ButtonEnabler();
        }
        public string LocalDataPath => $"data-{ DataManager.Instance.PlayerData.PlayerModel.fullName}.json";
        public bool IsTutorialMode(){
            var filePath = Path.Combine(Application.persistentDataPath, LocalDataPath);
            if (File.Exists(filePath)) return false;
            else return true;
        }
        
        public void ButtonEnabler(){
            if(IsTutorialMode()){
                BodyPartsBtnEnabler(false);
                m_genderButton?.SetActive(false);
                m_tutorialButtons.SetActive(true);
                m_normalButtons.SetActive(false);
            }else{
                BodyPartsBtnEnabler(true);
                m_genderButton?.SetActive(true);
                m_tutorialButtons.SetActive(false);
                m_normalButtons.SetActive(true);
            }
        }
        public void BodyPartsBtnEnabler(bool isEnable){
            for (int i = 0; i < m_mainButton.Length; i++)
            {
                m_mainButton[i].interactable = isEnable;
            }
        }

     
    }
}
