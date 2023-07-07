using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;


public class APITest : MonoBehaviour
{
    [SerializeField] private TMP_InputField _usernameField;
    [SerializeField] private TMP_InputField _passwordField;

    public void Login()
    {
        StartCoroutine(LoginRoutine());

        IEnumerator LoginRoutine()
        {
            WWWForm form = new WWWForm();
            form.AddField("code", _usernameField.text);
            form.AddField("password", _passwordField.text);
            form.AddField("myField", "myData");

            using (UnityWebRequest www = UnityWebRequest.Post("https://earnmore.ph/v1/sean/login", form))
            {

                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                }
            }
        }
    }

   
}
