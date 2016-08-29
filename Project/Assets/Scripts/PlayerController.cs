using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public int playerNumber;
    public float velocity;
    public float bulletMaxInitialVelocity;
    public float maxTimeShooting;
    public BoxCollider2D groundBC;
    public GameObject bulletPrefab;
    public float forwardSpeed = 5f;
    public float reverseSpeed = 2.5f;

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
    private bool moving;
    private Animator anim;
    private GameObject gameController;
	private GameObject red1, blue1;

	public AudioClip shoot;
	private bool didShoot = false;

	private float distanceRed, distanceBlue;
    
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        an = GetComponent<Animator>();
        gameController = GameObject.Find("GameController");
		red1 = GameObject.Find("Red_1");
		blue1 = GameObject.Find("Blue_1");
    }
    
    void Update()
    {
        int turn = gameController.GetComponent<GameController>().turn;
        targetting = gameController.GetComponent<GameController>().targetting;
        moving = gameController.GetComponent<GameController>().moving;
		distanceRed = red1.GetComponent<TankMovementTracker>().totalDistance;
		distanceBlue = blue1.GetComponent<TankMovementTracker>().totalDistance;	

        if (turn == 1 && gameObject.tag == "RedPlayer")
        {
            UpdateAll();
        }
        else if (turn == 2 && gameObject.tag == "BluePlayer")
        {
            UpdateAll();
        }
    }

    void UpdateAll()
    {
        if (targetting)
        {
            gunTransform.gameObject.SetActive(true);
            UpdateTargetting();
            UpdateShootDetection();
            if (shooting)
                UpdateShooting();
        }
        else
        {
            gunTransform.gameObject.SetActive(false);
        }
        UpdateMove();
    }

    void UpdateShootDetection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shooting = true;
            shootingEffect.SetActive(true);
            timeShooting = 0f;
        }
    }

    void UpdateShooting()
    {
		if(!didShoot) {
			AudioSource.PlayClipAtPoint(shoot, transform.position);
			didShoot = true;
		}

        timeShooting += Time.deltaTime;
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
        {
            shooting = false;
            shootingEffect.SetActive(false);
            Shoot();
            gameController.GetComponent<GameController>().targetting = false;
        }
        if (timeShooting > maxTimeShooting)
        {
            shooting = false;
            shootingEffect.SetActive(false);
            Shoot();
            gameController.GetComponent<GameController>().targetting = false;
        }
    }

    void Shoot()
    {
        Vector3 mousePosScreen = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
        Vector2 playerToMouse = new Vector2(mousePosWorld.x - transform.position.x,
                                            mousePosWorld.y - transform.position.y);

        playerToMouse.Normalize();

        shootDirection = playerToMouse;
        Debug.Log("Shoot!");
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = bulletInitialTransform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletMaxInitialVelocity * (timeShooting / maxTimeShooting);
    }

    void UpdateTargetting()
    {
        Vector3 mousePosScreen = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
        Vector2 playerToMouse = new Vector2(mousePosWorld.x - transform.position.x,
                                            mousePosWorld.y - transform.position.y);

        playerToMouse.Normalize();

        float angle = Mathf.Asin(playerToMouse.y) * Mathf.Rad2Deg;
        if (playerToMouse.x < 0f)
            angle = 180 - angle;

        if (playerToMouse.x > 0f && bodyTransform.localScale.x < 0f)
        {
            bodyTransform.localScale = new Vector3(-bodyTransform.localScale.x, bodyTransform.localScale.y, 0f);
        }
        else if (playerToMouse.x < 0f && bodyTransform.localScale.x > 0f)
        {
            bodyTransform.localScale = new Vector3(-bodyTransform.localScale.x, bodyTransform.localScale.y, 0f);
        }

        gunTransform.localEulerAngles = new Vector3(0f, 0f, angle);
    }

    void UpdateMove()
    {
        float move = Input.GetAxis("Horizontal");
		if (facingRight && moving)
        {
			if(gameObject.tag == "RedPlayer" && distanceRed <= 20) {
				rb.velocity = new Vector2(move * forwardSpeed, rb.velocity.y);
			}
			else if(gameObject.tag == "BluePlayer" && distanceBlue <= 20) {
				rb.velocity = new Vector2(move * reverseSpeed, rb.velocity.y);
			}
        }
        else if (!facingRight && moving)
        {
			if(gameObject.tag == "RedPlayer" && distanceRed <= 20) {
				rb.velocity = new Vector2(move * reverseSpeed, rb.velocity.y);
			}
			else if(gameObject.tag == "BluePlayer" && distanceBlue <= 20) {
				rb.velocity = new Vector2(move * forwardSpeed, rb.velocity.y);
			}
        }

        anim.SetFloat("Speed", Mathf.Abs(move));

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 tempScale = transform.localScale;
        //tempScale.x *= -1;
        transform.localScale = tempScale;
    }
}
