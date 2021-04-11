using UnityEngine;

public abstract class BasePlayerStateModel : IPlayerState
{
    private RaycastHit _hit;
    private float _footDistance = 1f;
    private Vector3 _raycastOffset = new Vector3(0, 1, 0);

    private float _failVelocity = -10f;


    public virtual void Execute(PlayerController controller, PlayerView player)
    {
        if (Physics.Raycast(player.transform.position + _raycastOffset, Vector3.down, out _hit, 5f))
        {
            player.Rigidbody.isKinematic = true;
            player.transform.position += new Vector3(0, (_footDistance - _hit.distance), 0);
        }
        else
        {
            player.Rigidbody.isKinematic = false;

            if (player.Rigidbody.velocity.y <= _failVelocity)
            {
                player.LevelFail();
                Time.timeScale = 0;
            }
        }
    }
}