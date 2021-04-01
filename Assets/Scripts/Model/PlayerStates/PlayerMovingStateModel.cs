using UnityEngine;

public class PlayerMovingStateModel : BasePlayerStateModel
{
    private Vector3 _movingVector = Vector3.zero;
    private Vector2 _movingVector2D;
    private float _magnitude;
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

        //player.Transform.Translate(Vector3.forward * _magnitude * 0.01f * player.MovementSpeed * Time.deltaTime);
        player.Rigidbody.AddForce(new Vector3(0, 10, 0));
        player.GetComponent<CharacterController>().SimpleMove(player.transform.forward * _magnitude * 0.01f * player.MovementSpeed);
        
        
        player.Animator.SetFloat("VectorSpeedMagnitude", _magnitude * 0.01f);
    }


}