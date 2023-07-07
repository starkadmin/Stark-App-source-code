using System;

namespace Stark.Model
{
    [Serializable]
    public class AuthenticationModel
    {
        [Serializable]
        public class LoginModel
        {
            public string code = string.Empty;
            public string password = string.Empty;
        }

        [Serializable]
        public class SignUPModel
        {
            public string userCode = string.Empty;
            public string lastName = string.Empty;
            public string firstName = string.Empty;
            public string middleName = string.Empty;
            public string email = string.Empty;
            public string mobileNo = string.Empty;
            public string gcashNo = string.Empty;
            public string dateOfBirthEpoch = string.Empty;
            public string password = string.Empty;
        }

        public class ProfileImage
        {
            public string govermentID = string.Empty;
            public string selfie = string.Empty;
        }
    }

}
