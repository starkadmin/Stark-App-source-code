using System;
using System.Collections;
using Stark.Helper;
using Stark.Helper.Enum;
using Stark.Model;
using TMPro;
using Stark.Handler;
using Stark.UI;
using UnityEngine;

namespace Stark.Manager
{
    public class AuthUIManager : MonoBehaviour
    {
        [Space(10), UProperty(12f, UPropertyAttribute.EPropertyStyle.Header, "Login")]
        [SerializeField] private TMP_InputField m_userlogin;
        [SerializeField] private TMP_InputField m_passwordLogin;
        
        [Space(10), UProperty(12f, UPropertyAttribute.EPropertyStyle.Header, "Signup")]
        [SerializeField] private TMP_InputField m_username;
        [SerializeField] private TMP_InputField m_firstName;
        [SerializeField] private TMP_InputField m_lastName;
        [SerializeField] private TMP_InputField m_middleName;
        [SerializeField] private TMP_InputField m_email;
        [SerializeField] private TMP_InputField m_passwordSignup;
        [SerializeField] private TMP_InputField m_confirmPassword;
        [SerializeField] private TMP_InputField m_phoneNumber;
        [SerializeField] private TMP_InputField m_dateOfBirth;
        [SerializeField] private PhotoHandler documentPhotoHandler;
        [SerializeField] private PhotoHandler profilePhotoHandler;
        [SerializeField] private DateHandler dateOfBirthHandler;

        [Space(10), UProperty(12f, UPropertyAttribute.EPropertyStyle.Header, "Warning")]
        [SerializeField] private TextMeshProUGUI m_warningTMP;
        
        public void Login()
        {
            if (LoginValidation()) return;
            var _login = new AuthenticationModel.LoginModel
            {
                code = m_userlogin.text,
                password = m_passwordLogin.text
            };
            
            AuthenticationController.OnLogin?.Invoke(_login);
        }
        public void Signup()
        {
            if(SignupValidations())return;
            var _data = new AuthenticationModel.SignUPModel
            {
                userCode = m_username.text,
                firstName = m_firstName.text,
                lastName = m_lastName.text,
                middleName = m_middleName.text,
                email = m_email.text,
                gcashNo = m_phoneNumber.text,
                mobileNo = m_phoneNumber.text,
                dateOfBirthEpoch = dateOfBirthHandler.DateOfBirth,
                password = m_passwordSignup.text
            };
            // var _login = new AuthenticationModel.LoginModel
            // {
            //     code = m_phoneNumber.text,
            //     password = m_passwordSignup.text
            // };
            var _imageData = new AuthenticationModel.ProfileImage
            {
                govermentID = documentPhotoHandler.Base64Img,
                selfie =  profilePhotoHandler.Base64Img
            };
            AuthenticationController.OnSignup?.Invoke(StarkHelper.GenerateJwtToken(_data, "admin1"), _imageData,
                () =>
                {
                    //AuthenticationController.OnLogin?.Invoke(_login)
                    ToastWarning.OnWarning("Signup Success");
                    UIManager.instance.SwitchMainScreens(ScreenEnum.MainScreenType.Login);
                });
        }

        private bool LoginValidation()
        {
            if (string.IsNullOrEmpty(m_userlogin.text))
            {
                return true;
            }
            if (string.IsNullOrEmpty(m_passwordLogin.text))
            {
                return true;
            }

            return false;
        }

        private bool SignupValidations()
        {
            if (string.IsNullOrEmpty(m_username.text))
            {
                StartCoroutine(WarningRoutine("Enter your username"));
                return true;
            }
            if (string.IsNullOrEmpty(m_firstName.text))
            {
                StartCoroutine(WarningRoutine("Enter your first name"));
                return true;
            }
            if (string.IsNullOrEmpty(m_lastName.text))
            {
                StartCoroutine(WarningRoutine("Enter your last name"));
                return true;
            }
            if (string.IsNullOrEmpty(m_middleName.text))
            {
                StartCoroutine(WarningRoutine("Enter your middle name"));
                return true;
            }
            if (string.IsNullOrEmpty(m_email.text))
            {
                StartCoroutine(WarningRoutine("Enter your email"));
                return true;
            }
            if (string.IsNullOrEmpty(m_passwordSignup.text))
            {
                StartCoroutine(WarningRoutine("Enter your password"));
                return true;
            }
            if (string.IsNullOrEmpty(m_phoneNumber.text))
            {
                StartCoroutine(WarningRoutine("Enter your phone number"));
                return true;
            }
            // if (string.IsNullOrEmpty(m_dateOfBirth.text))
            // {
            //     StartCoroutine(WarningRoutine("Enter your birthday"));
            //     return true;
            // }

            if (m_passwordSignup.text.Length < 6)
            {
                StartCoroutine(WarningRoutine("Password is too short"));
                return true;
            }

            if (m_passwordSignup.text != m_confirmPassword.text)
            {
                StartCoroutine(WarningRoutine("Password does not match"));
                return true;
            }

            if (!DateTime.TryParseExact(dateOfBirthHandler.DateOfBirth, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None,
                    out var date))
            {
                StartCoroutine(WarningRoutine("Invalid Date Format MM/DD/YYYY"));
                return true; 
            }

            return false;
        }


        private IEnumerator WarningRoutine(string _warning)
        {
            m_warningTMP.text = _warning;
            yield return new WaitForSeconds(2f);
            m_warningTMP.text = string.Empty;
        }

    }
}
