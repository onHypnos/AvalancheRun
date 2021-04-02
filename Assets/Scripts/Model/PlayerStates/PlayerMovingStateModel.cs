using UnityEngine;

public class PlayerMovingStateModel : BasePlayerStateModel
{
    private Vector3 _movingVector = Vector3.zero;
    private Vector2 _movingVector2D;
    private float _magnitude;

    private RaycastHit _hit;
    private float _footDistance = 1f;
    private Vector3 _targetVelocity;
    public override void Execute(PlayerController controller, PlayerView player)
    {
        _movingVector2D = controller.PositionDelta - controller.PositionBegan;
        _magnitude = _movingVector2D.magnitude;
        if (_magnitude > 100)
        {
            _magnitude = 100.0f;
        }
        _movingVector.x = _movingVector2D.x;
        _movingVector.z = _movingVector2D.y;
        _movingVector.y = 0;

        player.Rotation = Quaternion.LookRotation(_movingVector, Vector3.up);

        player.Transform.Translate(Vector3.forward * _magnitude * 0.01f * player.MovementSpeed * Time.deltaTime);


        player.Rigidbody.useGravity = false;
        //player.Rigidbody.AddForce(player.transform.forward * 0.1f * player.MovementSpeed, ForceMode.VelocityChange);
        //player.Rigidbody.velocity = Vector3.ClampMagnitude(player.Rigidbody.velocity, player.MovementSpeed);
        if (Physics.Raycast(player.transform.position + new Vector3(0, 1, 0), Vector3.down, out _hit, 5f))
        {
            player.transform.position += new Vector3(0, (_footDistance - _hit.distance), 0);
        }
        else
        {
            player.Rigidbody.useGravity = true;
        }

        player.Animator.SetFloat("VectorSpeedMagnitude", _magnitude * 0.01f);
    }


}