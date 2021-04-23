public class SkinRewardModel : IGetReward
{
    //кнопка будет иметь в себе экземпляр этого класса с уже сохраненным id
    public SkinRewardModel(PlayerSkinUIView skin)
    {
        Skin = skin;
    }
    public PlayerSkinUIView Skin { get; private set; }

    public void RewardPlayer()
    {
        //здесь нужно открыть скин по id у игрока
        //делаем gameevent в который будет отправляться id skin и открывать его
        //
        GameEvents.Current.UnlockSkinEvent(Skin);
    }
}