using System;
using System.Collections;
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

	[SerializeField] private float spinSpeedOnDeath = 10f;
	[SerializeField] private float shrinkSpeedOnDeath = 10f;
	
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

	private LayerMask deathLayer;
	public event System.Action OnDeath;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		deathLayer = LayerMask.GetMask("Death");
	}

	public void Move(float horizontal, float vertical) => Move(new Vector2(horizontal, vertical));
	public void Move(Vector2 moveDir) => MoveDirection = moveDir;

	public bool canMove = true;
    private void FixedUpdate()
    {
	    if (canMove) UpdateVelocity();
    }

    private void Update()
    {
	    var hits = Physics2D.CircleCastAll(transform.position, 0.05f,
		    Vector2.zero, 0f, deathLayer);

	    if (hits.Length <= 0) return;
	    
	    // Player death animation
	    OnDeath?.Invoke();
	    rb.velocity = Vector2.zero;
	    StartCoroutine(DeathAnim());
    }

    private IEnumerator DeathAnim()
    {
	    while (transform.localScale.x > 0.01f)
	    {
		    transform.localScale -= Vector3.one * shrinkSpeedOnDeath * Time.deltaTime;
		    transform.Rotate(Vector3.forward * spinSpeedOnDeath * Time.deltaTime);
		    
		    yield return null;
	    }
    }

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
