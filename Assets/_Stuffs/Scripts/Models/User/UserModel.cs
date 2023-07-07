using System;

namespace Stark.Model
{
    public class UserModel
    {
           
        [Serializable]
        public class PlayerModel
        {
            public string code = string.Empty;
            public string key = string.Empty;
            public int agentId = 0;
            public string remarks = string.Empty;
            public string status = string.Empty;
            public string avatarStatus = string.Empty;
            public string fullName = string.Empty;
            public string agentCode = string.Empty;
            public string balance = string.Empty;
            public string withdrawal = string.Empty;
            public string groupCode = string.Empty;
            public string userType = string.Empty;
            public string gender = string.Empty;
            public string email = string.Empty;
            public string userCode = string.Empty;
        }
    }
}
