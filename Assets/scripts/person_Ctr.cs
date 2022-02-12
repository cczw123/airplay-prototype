using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class person_Ctr : MonoBehaviourPun
{
    public float people_force;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        movement();
        if (Input.GetMouseButton(0))
        {
            transform.localScale = Vector2.one;
        }
        if (Input.GetMouseButtonUp(0))
        {
            transform.localScale = new Vector2(0.7f,0.7f);
        }
    }

    void movement()
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
        force *= people_force;
        
        if (Input.GetMouseButton(0))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(2*force);
        }
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
    }
}
