using System;

namespace Stark.Model
{
    [Serializable]
    public class AvatarModel
    {
        public AvatarModel(int _gender)
        {
            id = DateTime.Now.Ticks.ToString();
            gender = _gender;
            dateModified = DateTime.UtcNow.ToString("h:mm:ss tt zz");
            attireData = new AttireModel.AttireData(new AttireModel.AttireClass());
        }
        
        public string id;
        public int gender; //Female = 0 : Male = 1
        public string dateModified;
        public AttireModel.AttireData attireData;
        
    }
 
}

