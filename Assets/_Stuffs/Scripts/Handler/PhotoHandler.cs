using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using UnityEngine.Networking;
using UnityEngine.Events;

namespace Stark.Handler
{
    public class PhotoHandler : MonoBehaviour {
    public Image m_photoPreview;
    public string Base64Img {get; private set;}
    private string _base64Img;
    private Texture2D _photoTexture;

    [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Events")]
    public UnityEvent OnImageLoaded = new UnityEvent();

       

        /// <summary>
        // Open the device camera to take a photo
        /// </summary>
        public void TakePhoto() {
    if (!Application.isMobilePlatform) {
        Debug.LogError("This feature is only available on mobile devices.");
        return;
    }

    // Request permission to use the camera
    NativeCamera.Permission permission = NativeCamera.TakePicture((path) => {
        if (path != null) {
            StartCoroutine(LoadPhoto(path));
        }
        }, 512);

        if (permission == NativeCamera.Permission.Denied) {
            Debug.LogError("Permission denied to access the camera.");
        }
    }

    /// <summary>
    // Open the gallery to select a photo
    /// </summary>
  
    public void ChoosePhoto() {
        if (!Application.isMobilePlatform) {
            Debug.LogError("This feature is only available on mobile devices.");
            return;
        }

        // Request permission to access the gallery
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) => {
            if (path != null) {
                StartCoroutine(LoadPhoto(path));
            }
        }, "Select a photo");

        if (permission == NativeGallery.Permission.Denied) {
            Debug.LogError("Permission denied to access the gallery.");
        }
    }

    
    /// <summary>
    // Load the selected photo into a Texture2D and display it in the preview
    /// </summary>
    private IEnumerator LoadPhoto(string path) {
        _photoTexture = new Texture2D(512, 512);
        byte[] photoData = File.ReadAllBytes(path);
        _photoTexture.LoadImage(photoData);

        m_photoPreview.sprite = Sprite.Create(_photoTexture, new Rect(0, 0, _photoTexture.width, _photoTexture.height), new Vector2(0.5f, 0.5f));
        m_photoPreview.gameObject.SetActive(true);
        OnImageLoaded.Invoke();
        ConvertImageFormat();
        yield return null;
    }

    /// <summary>
    // Upload the selected photo to the server
    /// </summary>
    public void UploadPhoto() {
        if (_photoTexture == null) {
            Debug.LogError("No photo selected.");
            return;
        }

        string url = "https://testurl.com/upload.php";
        byte[] photoData = _photoTexture.EncodeToJPG();
        WWWForm form = new WWWForm();
        form.AddBinaryData("photo", photoData);
        StartCoroutine(UploadPhotoCoroutine(url, form));
        
        
    }

    public void ConvertImageFormat(){
        if (_photoTexture == null) {
            Debug.LogError("No photo selected.");
            return;
        }
        byte[] photoData = _photoTexture.EncodeToJPG();
        Base64Img = Convert.ToBase64String(photoData);
    }

    private IEnumerator UploadPhotoCoroutine(string url, WWWForm form) {
        using (UnityWebRequest www = UnityWebRequest.Post(url, form)) {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
                Debug.LogError(www.error);
            } else {
                Debug.Log("Photo uploaded successfully.");
            }
        }
    }

    }
}