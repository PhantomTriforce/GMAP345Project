using UnityEngine;
using System.Collections;

public class TestMovementScript : MonoBehaviour {
	
	public float maxSpeed = 5f;
	public float jumpForce = 350f;

	private bool facingRight = true;
	private bool grounded = false;

	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	private Rigidbody2D rb;
	private Animator anim;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		float move = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

		anim.SetFloat("Speed", Mathf.Abs(move));

		if(move > 0 && !facingRight) {
			Flip();
		}
		else if(move < 0 && facingRight) {
			Flip();
		}

		//Player jump
		if(Input.GetKeyDown(KeyCode.Space) && grounded) {
			rb.AddForce(new Vector2(0, jumpForce)); //could have done Vector2.up * jumpForce inside AddForce
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 tempScale = transform.localScale;
		tempScale.x *= -1;
		transform.localScale = tempScale;
	}
}
