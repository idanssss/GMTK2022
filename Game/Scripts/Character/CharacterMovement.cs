using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed = 5.5f;

	[SerializeField]
	private float accelerationRate;
	
	[SerializeField]
	private float decelerationRate;
	
	private Vector2 _moveDirection;
	public Vector2 MoveDirection
    {
		get => _moveDirection;
		private set {
			value.Normalize();
			if (_moveDirection == value) return;

			_moveDirection = value;
			MoveSpeed = _moveDirection * moveSpeed;
		}
    }

	public Vector2 MoveSpeed { get; private set; }
	private Rigidbody2D rb;

	private void Awake() => rb = GetComponent<Rigidbody2D>();

	public void Move(float horizontal, float vertical) => Move(new Vector2(horizontal, vertical));
	public void Move(Vector2 moveDir) => MoveDirection = moveDir;

    private void FixedUpdate() => UpdateVelocity();
    private void UpdateVelocity()
    {
	    const float threshold = 0.001f;
	    Vector2 newVel = Vector2.zero;

	    if (Math.Abs(MoveSpeed.x) > threshold)
		    newVel.x = Mathf.MoveTowards(rb.velocity.x, MoveSpeed.x, accelerationRate);
	    else
		    newVel.x = Mathf.MoveTowards(rb.velocity.x, 0, decelerationRate);

	    if (Math.Abs(MoveSpeed.y) > threshold)
		    newVel.y = Mathf.MoveTowards(rb.velocity.y, MoveSpeed.y, accelerationRate);
	    else
		    newVel.y = Mathf.MoveTowards(rb.velocity.y, MoveSpeed.y, decelerationRate);

	    rb.velocity = newVel;
    }
}
