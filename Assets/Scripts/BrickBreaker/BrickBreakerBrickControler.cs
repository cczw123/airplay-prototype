using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BrickBreakerBrickControler : MonoBehaviour
{
    public int current_num = 1;
    private TextMeshPro text_child;

    void Start()
    {
        text_child = gameObject.GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {

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
            hockeyRigidbody2D.AddForce(force);
            AudioManager.Instance.PlayKickAudio(collision.transform.position);
            ChangeNum(-1);
            BrickBreakerGameManager.Inst.score++;
        }

    }

    public void ChangeNum(int val)
    {
        current_num+=val;
        if (current_num <= 0)
        {
            Destroy(gameObject);
        }
        text_child.text = current_num.ToString();
    }
}
