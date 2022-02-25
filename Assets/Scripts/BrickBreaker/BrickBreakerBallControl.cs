using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreakerBallControl : MonoBehaviour
{
    public float initial_force_val = 1;
    public float speed_increase_factor = 0.1f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 randomVector = Random.insideUnitCircle;
        randomVector.Normalize();
        rb.AddForce(randomVector * initial_force_val);
    }

    // Update is called once per frame
    public void increaseSpeedBy(float val)
    {
        rb.velocity += rb.velocity.normalized * val; 
    }
}
