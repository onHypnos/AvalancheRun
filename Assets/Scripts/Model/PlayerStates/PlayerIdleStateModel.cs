using UnityEngine;

public class PlayerIdleStateModel : BasePlayerStateModel
{
    public override void Execute(PlayerController controller, PlayerView player)
    {
        player.Animator.SetFloat("VectorSpeedMagnitude", 0);
        player.Rigidbody.velocity = Vector3.zero;
    }    
}