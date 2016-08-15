using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float velocity; 
	public float bulletMaxInitialVelocity;
	public float maxTimeShooting; 
	public BoxCollider2D groundBC; 
	public GameObject bulletPrefab; 

	private BoxCollider2D bc; 
	private Rigidbody2D rb; 
	private Animator an; 
	private bool shooting;
	private bool facingRight = true;
	private float timeShooting;
	private Vector2 shootDirection;

	public GameObject shootingEffect;
	public Transform gunTransform; 
	public Transform bodyTransform; 
	public Transform bulletInitialTransform; 

	private bool targetting; 

	// Use this for initialization
	void Start () {
		bc = GetComponent<BoxCollider2D>();
		rb = GetComponent<Rigidbody2D>();
		//an = GetComponentInChildren<Animator>();
		an = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown(KeyCode.W) ){
			targetting = true;
			gunTransform.gameObject.SetActive(true);
		}
		if( targetting ){
			UpdateTargetting();
			UpdateShootDetection();
			if( shooting )
				UpdateShooting();
		}        
	}
    
	void UpdateShootDetection(){
		if( Input.GetMouseButtonDown(0)){
			shooting = true;
			shootingEffect.SetActive(true);
			timeShooting = 0f;
		}
	}
    
	void UpdateShooting(){
		timeShooting += Time.deltaTime;
		if(  Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space) ){
			shooting = false;
			shootingEffect.SetActive(false);
			Shoot();
		}
		if( timeShooting > maxTimeShooting ){
			shooting = false;
			shootingEffect.SetActive(false);
			Shoot ();
		}
	}
    
	void Shoot(){
		Vector3 mousePosScreen = Input.mousePosition;
		Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
		Vector2 playerToMouse = new Vector2( mousePosWorld.x - transform.position.x,
		                                    mousePosWorld.y - transform.position.y);
		
		playerToMouse.Normalize();

		shootDirection = playerToMouse;
		Debug.Log("Shoot!");
		GameObject bullet = Instantiate(bulletPrefab);
		bullet.transform.position = bulletInitialTransform.position;
		bullet.GetComponent<Rigidbody2D>().velocity = shootDirection*bulletMaxInitialVelocity*(timeShooting/maxTimeShooting);
	}
    
	void UpdateTargetting(){
		Vector3 mousePosScreen = Input.mousePosition;
		Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
		Vector2 playerToMouse = new Vector2( mousePosWorld.x - transform.position.x,
		                                    mousePosWorld.y - transform.position.y);

		playerToMouse.Normalize();

		float angle = Mathf.Asin(playerToMouse.y)*Mathf.Rad2Deg;
		if( playerToMouse.x < 0f )
			angle = 180-angle;

		if( playerToMouse.x > 0f && bodyTransform.localScale.x < 0f ){
			bodyTransform.localScale = new Vector3(-bodyTransform.localScale.x, bodyTransform.localScale.y, 0f);
		}
		else if( playerToMouse.x < 0f && bodyTransform.localScale.x > 0f ){
			bodyTransform.localScale = new Vector3(-bodyTransform.localScale.x, bodyTransform.localScale.y, 0f);
		}

		gunTransform.localEulerAngles = new Vector3(0f, 0f, angle);
	}

	void UpdateMove(){
		if( Input.GetKey(KeyCode.D) ){
			rb.velocity = Vector2.right*velocity;
			if( bodyTransform.localScale.x < 0f )
				bodyTransform.localScale = new Vector3( -bodyTransform.localScale.x, bodyTransform.localScale.y, 0f );
            
		}
		else if( Input.GetKey(KeyCode.A) ){
			rb.velocity = -Vector2.right*velocity;
			if( bodyTransform.localScale.x > 0f )
				bodyTransform.localScale = new Vector3( -bodyTransform.localScale.x, bodyTransform.localScale.y, 0f );            
		}
		else{
			rb.velocity = Vector2.zero;
		}
	}
    
	void OnCollisionStay2D( Collision2D other ){
		if( other.collider.tag == "Ground" ){
			UpdateMove();
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 tempScale = transform.localScale;
		tempScale.x *= -1;
		transform.localScale = tempScale;
	}
}
