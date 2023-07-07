using System.IO;
using Stark.Core;
using Stark.Model;
using Stark.Helper.Enum;
using Stark.SO;
using UnityEngine;

namespace Stark.Manager
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }
        
        [Space(10), UProperty(9, UPropertyAttribute.EPropertyStyle.Header, "Player Data")]
        public PlayerSO PlayerData;

        [Space(10), UProperty(9, UPropertyAttribute.EPropertyStyle.Header, "Avatar Setup")]
        [SerializeField] private GameObject m_femaleAvatar;
        [SerializeField] private GameObject m_maleAvatar;
        [SerializeField] private Transform m_avatarLocation;
        
        private int genderIndex = 0;
        private string localDataPath => $"data-{PlayerData.PlayerModel.fullName}.json";
        public GameObject ActiveAvatar { get; private set; }
        public AvatarModel AvatarData { get; set; } = new AvatarModel(1);
        private void Awake()
        {
            Instance = this;
        }

        public void SaveAvatarData()
        {
            var _json = JsonUtility.ToJson(AvatarData);
            // Debug.Log(Path.Combine(Application.persistentDataPath));
            File.WriteAllText(Path.Combine(Application.persistentDataPath,localDataPath), _json);
        }

        public void SavePlayerData(){
            ActiveAvatar.GetComponent<Customization>().SaveAvatarData();
        }

        public void DeletePlayerData()
        {
            /*var filePath = Path.Combine(Application.persistentDataPath, LocalDataPath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log($"File {LocalDataPath} deleted.");
            }
            else Debug.LogWarning($"File {LocalDataPath} does not exist.");*/ 
            PlayerPrefs.DeleteAll();
            AvatarData = new AvatarModel(1);
            Destroy(ActiveAvatar);
        }
        public void Initialize(UserModel.PlayerModel _player)
        {
            PlayerData.PlayerModel = _player;
            var _loadData = LoadPlayerData();

            if (!string.IsNullOrEmpty(_loadData))
            {
                //Load data
                AvatarData = JsonUtility.FromJson<AvatarModel>(_loadData);
                genderIndex = AvatarData.gender;
                SpawnPlayerAvatar(genderIndex);
                UIManager.instance.LoadDirectHome();
                return;
            }
            UIManager.instance.SwitchMainScreens(ScreenEnum.MainScreenType.Welcome);
        }

        public void SpawnPlayerAvatar(int gender)
        {
           
            var avatar = gender == 0 ? m_femaleAvatar : m_maleAvatar;
            if (ActiveAvatar != null)
            {
                if (AvatarData.gender != gender)
                {
                    Destroy(ActiveAvatar);
                    AvatarData = new AvatarModel(gender);
                    CustomizationAvatar(avatar).SetupOnLoad(AvatarData);
                    return;
                }
                ActiveAvatar.GetComponent<Customization>().SetupOnLoad(AvatarData);
                return;
            }
            CustomizationAvatar(avatar).SetupOnLoad(AvatarData);
            
        }
        private string LoadPlayerData()
        {
            return File.Exists(Path.Combine(Application.persistentDataPath,localDataPath)) 
                ? File.ReadAllText(Path.Combine(Application.persistentDataPath, localDataPath)) : string.Empty;
        }


        private Customization CustomizationAvatar(GameObject avatar)
        {
            ActiveAvatar = Instantiate(avatar, m_avatarLocation.position, Quaternion.identity) as GameObject;
            AvatarData.gender = genderIndex;
            return ActiveAvatar.GetComponent<Customization>();
        }

        private void OnGenderSelected(int gender){
            genderIndex = gender;
        }
        private void OnPageTransitionEnded(ScreenEnum.MainScreenType pageType)
        {
            if(pageType ==ScreenEnum.MainScreenType.Customization) UIManager.instance.SwitchMainScreens(ScreenEnum.MainScreenType.Customization);
            UIManager.instance._platform.SetActive(true);
            SpawnPlayerAvatar(genderIndex);    
        }

        private void OnEnable()
        {
            UIManager.GenderSelected += OnGenderSelected;
            UIManager.PageTransitionEnded += OnPageTransitionEnded;
        }


        private void OnDisable()
        {
            UIManager.GenderSelected -= OnGenderSelected;  
            UIManager.PageTransitionEnded -= OnPageTransitionEnded;
        }

    }

}
