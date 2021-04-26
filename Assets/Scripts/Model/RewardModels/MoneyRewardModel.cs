using UnityEngine;

public class MoneyRewardModel : IGetReward
{
    public MoneyRewardModel()
    {
    }

    public void RewardPlayer()
    {
        GameEvents.Current.OnRewardMoney();
    }
}
