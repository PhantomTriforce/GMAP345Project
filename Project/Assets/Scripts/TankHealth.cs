using UnityEngine;
using System.Collections;

public class TankHealth : MonoBehaviour {

    public float maxHealth = 100;
    public float currentHealth = 100;
    private float heathBarOriginalState;

    // Use this for initialization
    void Start()
    {
        heathBarOriginalState = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = gameObject.transform.localScale;
        scale.x = currentHealth / maxHealth * heathBarOriginalState;
        gameObject.transform.localScale = scale;
    }

}
