using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	private Rigidbody2D rb; 
	public Transform bulletSpriteTransform; 
	private bool updateAngle = true; 
	public GameObject bulletSmoke;
	public CircleCollider2D destructionCircle;
	public static GroundController groundController;
    public int damage = 20;
    private GameObject gameController;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController");
    }
	
	// Update is called once per frame
	void Update () {
		if( updateAngle ){
			Vector2 dir = new Vector2(rb.velocity.x, rb.velocity.y);
            dir.Normalize();			
			float angle = Mathf.Asin (dir.y)*Mathf.Rad2Deg;
			if( dir.x < 0f ){
				angle = 180 - angle;
			}
			bulletSpriteTransform.localEulerAngles = new Vector3(0f, 0f, angle+45f);
		}
	}

    void OnCollisionEnter2D(Collision2D coll) {
        int turn = gameController.GetComponent<GameController>().turn;

        if (coll.collider.tag == "Ground") {
            updateAngle = false;
            bulletSmoke.SetActive(false);
            groundController.DestroyGround(destructionCircle);
            Destroy(gameObject);
        }
        if (turn == 1)
        {
            if (coll.collider.tag == "BluePlayer")
            {

                healthDetection(coll);
            }
        }
        if (turn == 2)
        {
            if (coll.collider.tag == "RedPlayer")
            {
                healthDetection(coll);
            }
        }
    }

    private void healthDetection(Collision2D coll)
    {
        updateAngle = false;
        bulletSmoke.SetActive(false);
        groundController.DestroyGround(destructionCircle);

        Transform healthBarTransform = coll.gameObject.transform.FindChild("HealthBar");
        TankHealth healthBar = healthBarTransform.gameObject.GetComponent<TankHealth>();
        healthBar.currentHealth -= Mathf.Max(damage, 7);

        if (healthBar.currentHealth <= 0)
        {
            Destroy(coll.gameObject);
        }

        Destroy(gameObject);
    }
}
