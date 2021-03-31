public class PlayerIdleStateModel : BasePlayerStateModel
{
    public override void Execute(PlayerController controller, PlayerView player)
    {
        player.Animator.SetFloat("VectorSpeedMagnitude", 0);
    }    
}