public class SkinRewardModel : IGetReward
{
    public PlayerSkinUIView Skin { get; private set; }

    //кнопка будет иметь в себе экземпляр этого класса
    public SkinRewardModel(PlayerSkinUIView skin)
    {
        Skin = skin;
    }

    public void RewardPlayer()
    {
        GameEvents.Current.UnlockSkinEvent(Skin);
    }
}