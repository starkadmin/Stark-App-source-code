namespace Stark.Helper.Enum
{
    public class ScreenEnum 
    {
        //NOTE: Enum index must match the UIManager screen array index
        public enum MainScreenType
        {
            SignUp,
            Login,
            Welcome,
            GenderSelection,
            Home,
            Customization,
            Greetings,
            Leaderboard,
            MarketPlace
        }
        public enum CustomizationScreenType
        {
            Default,
            Head,
            UpperBody,
            LowerBody,
            Footwear,
        }
        
    }
}
