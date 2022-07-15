using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed = 5.5f;

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
    private void UpdateVelocity() => rb.velocity = MoveSpeed;
}
