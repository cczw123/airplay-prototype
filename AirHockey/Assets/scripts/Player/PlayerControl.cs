using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviourPun
{
    public float kickForce = 10f;

    public float speed;

    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            transform.localScale = Vector2.one;
        }

        if (Input.GetMouseButtonUp(0))
        {
            transform.localScale = new Vector2(0.7f, 0.7f);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;
        Vector2 velocity = rigidbody2D.velocity;
        velocity.x = horizontal;
        velocity.y = vertical;
        rigidbody2D.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hockey"))
        {
            Vector2 force = collision.gameObject.transform.position - transform.position;
            force *= kickForce;
            AudioManager.Instance.PlayKickAudio(collision.transform);
            Rigidbody2D hockeyRigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
            if (hockeyRigidbody2D == null)
            {
                return;
            }

            if (Input.GetMouseButton(0))
            {
                hockeyRigidbody2D.AddForce(2 * force);
            }

            hockeyRigidbody2D.AddForce(force);
        }
    }
}