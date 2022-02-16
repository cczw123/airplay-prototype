using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControl : MonoBehaviourPun
{
    public float kickForce = 10f;

    public float speed;

    // Start is called before the first frame update

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
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = horizontal;
        velocity.y = vertical;
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
        Vector2 force = collision.gameObject.transform.position - transform.position;
        force *= kickForce;

        if (Input.GetMouseButton(0))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(2 * force);
        }

        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
    }
}