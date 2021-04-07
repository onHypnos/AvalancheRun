using UnityEngine;

public class PlayerMovingStateModel : BasePlayerStateModel
{
    private Vector3 _movingVector = Vector3.zero;
    private Vector2 _movingVector2D;
    private float _magnitude;

    private RaycastHit _hit;
    private float _footDistance = 1f;
    private Vector3 _raycastOffset = new Vector3(0, 1, 0);

    public override void Execute(PlayerController controller, PlayerView player)
    {
        base.Execute(controller, player);
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

        //Перенести определение высоты в execute BaseEnemyStateModel с обработкой прыжка и падения
        if (Physics.Raycast(player.transform.position + _raycastOffset, Vector3.down, out _hit, 5f))
        {
            player.transform.position += new Vector3(0, (_footDistance - _hit.distance), 0);
        }

        player.Animator.SetFloat("VectorSpeedMagnitude", _magnitude * 0.01f);
    }
}