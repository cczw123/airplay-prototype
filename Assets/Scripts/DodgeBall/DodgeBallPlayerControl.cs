using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class DodgeBallPlayerControl : MonoBehaviour
{
    public float kickForce = 10f;

    [Tooltip("0 for not activated, 1 for activated via button/click")]
    public float[] kickFactorPreset = { 1.0f, 2.0f };

    [Tooltip("0 for not activated, 1 for activated via button/click")]
    public Vector3[] localScalePreset = { 0.7f * Vector3.one, Vector3.one };

    public float speed;

    private Rigidbody2D rb;



    public int team_ID = 0;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (PhotonNetwork.IsConnected && !photonView.IsMine)
        //{
        //    return;
        //}
        Move();
    }

    private void FixedUpdate()
    {

        //if (view.IsMine)
        //{
        //    
        //}
    }

    void Move()
    { 
        if (team_ID == 0)
        {

            float horizontal = Input.GetAxis("Horizontal") * speed;
            float vertical = Input.GetAxis("Vertical") * speed;
            Vector2 velocity = rb.velocity;
            velocity.x = horizontal;
            velocity.y = vertical;
            rb.velocity = velocity;
            Debug.Log("0: " + velocity);
        }
        else
        {
            float horizontal = 0;
            float vertical = 0;
            if (Input.GetKey(KeyCode.J))
            {
                horizontal = -1;
            }
            if (Input.GetKey(KeyCode.L))
            {
                horizontal = 1;
            }
            if (Input.GetKey(KeyCode.K))
            {
                vertical = -1;
            }
            if (Input.GetKey(KeyCode.I))
            {
                vertical = 1;
            }
            Vector2 velocity = rb.velocity;
            velocity.x = horizontal * speed;
            velocity.y = vertical * speed;
            rb.velocity = velocity;
            Debug.Log("1: " + velocity);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hockey"))
        {
            Rigidbody2D hockeyRigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
            if (hockeyRigidbody2D == null)
            {
                return;
            }

            Vector2 direction = collision.gameObject.transform.position - transform.position;
            direction.Normalize();
            hockeyRigidbody2D.velocity = Vector2.zero;
            hockeyRigidbody2D.AddForce(collision.gameObject.GetComponent<DodgeBallBallControl>().initial_force_val * direction);
            DodgeBallGameManager.Inst.scores[1 - (int)team_ID] += 1;
            AudioManager.Instance.PlayKickAudio(collision.transform.position);
        }
    }
}
