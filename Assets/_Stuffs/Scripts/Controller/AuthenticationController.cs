using System;
using System.Collections;
using JetBrains.Annotations;
using Stark.Manager;
using Stark.Model;
using Stark.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace Stark
{
    public class AuthenticationController : MonoBehaviour
    {
        public delegate void LoginEvent(AuthenticationModel.LoginModel login); 
        public delegate void SignupEvent(string jwt, AuthenticationModel.ProfileImage _img, [CanBeNull] Action callback);
        
        public static LoginEvent OnLogin;
        public static SignupEvent OnSignup;

        [SerializeField] private string authURL;
        [SerializeField] private string authSignupURL;
        

        private void OnEnable()
        {
            OnLogin += Login;
            OnSignup += Signup;
        }

        private void OnDisable()
        {
            OnLogin -= Login;
            OnSignup -= Signup;
        }

        private void Start()
        {
            var _data = PlayerPrefs.GetString("LOGIN-DATA");
            if (string.IsNullOrEmpty(_data)) return;
            var _json = JsonUtility.FromJson<AuthenticationModel.LoginModel>(_data);
            Login(_json);
        }

        private void Login(AuthenticationModel.LoginModel login)
        {
            var _json = JsonUtility.ToJson(login);
            StartCoroutine(LoginRoutine(_json));
        }
        
        public void Logout()
        {
            DataManager.Instance.DeletePlayerData();
            UIManager.instance.SwitchMainScreens(Helper.Enum.ScreenEnum.MainScreenType.Login);
        }

        private void Signup(string jwt, AuthenticationModel.ProfileImage _img, [CanBeNull] Action callback)
        {
            StartCoroutine(SignupRoutine(jwt , _img, callback));
        }

        private IEnumerator LoginRoutine(string _json)
        {
            using (var www = UnityWebRequest.Put($"{authURL}",_json))
            {
                www.method = UnityWebRequest.kHttpVerbPOST;
                www.SetRequestHeader("Content-Type", "application/json");
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    ToastWarning.OnWarning(www.downloadHandler.text);
                    yield break;
                }
                var _player = JsonUtility.FromJson<UserModel.PlayerModel>(www.downloadHandler.text);
                DataManager.Instance.Initialize(_player);

                Debug.Log(www.downloadHandler.text);
                PlayerPrefs.SetString("LOGIN-DATA",_json);

                ToastWarning.OnWarning("Login Success");
                Debug.Log("<color=green>Login Success</color>");
            }
            yield return null;
        }
        
        
        private IEnumerator SignupRoutine(string jwt, AuthenticationModel.ProfileImage _img,  [CanBeNull] Action  callback)
        {
            using (var www = UnityWebRequest.Put($"{authSignupURL}", JsonUtility.ToJson(_img)))
            {
                www.method = UnityWebRequest.kHttpVerbPOST;
                www.SetRequestHeader("payload", jwt);
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    ToastWarning.OnWarning(www.downloadHandler.text);
                    yield break;
                }
                Debug.Log("<color=green>Signup Success</color>");
                callback?.Invoke();

            }
            yield return null;
        }

        
        

    }
}
