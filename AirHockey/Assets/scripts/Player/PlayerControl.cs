using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviourPun
{
    public float kickForce = 10f;

    [Tooltip("0 for not activated, 1 for activated via button/click")]
    public float[] kickFactorPreset = {1.0f, 2.0f};

    [Tooltip("0 for not activated, 1 for activated via button/click")]
    public Vector3[] localScalePreset = {0.7f * Vector3.one, Vector3.one};

    public float speed;

    private Rigidbody2D rigidbody2D;
    private float kickFactor;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        kickFactor = kickFactorPreset[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            return;
        }

        StrongKick();
    }

    private void StrongKick()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            transform.localScale = localScalePreset[1];
            kickFactor = kickFactorPreset[1];
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            transform.localScale = localScalePreset[0];
            kickFactor = kickFactorPreset[0];
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
            Rigidbody2D hockeyRigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
            if (hockeyRigidbody2D == null)
            {
                return;
            }

            Vector2 force = collision.gameObject.transform.position - transform.position;
            force *= kickForce * kickFactor;
            hockeyRigidbody2D.AddForce(force);
            AudioManager.Instance.PlayKickAudio(collision.transform);
        }
    }
}