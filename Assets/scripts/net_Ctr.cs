using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MyCompany.MyGame
{
    public class net_Ctr : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "ball")
            {
                if (gameObject.name == "net")
                {
                    GameManager.Inst.playerPrefab.GetComponent<PlayerManager>().team1++;
                }
                else
                {
                    GameManager.Inst.playerPrefab.GetComponent<PlayerManager>().team2++;
                }
                collision.gameObject.SetActive(false);
                collision.gameObject.transform.position = Vector2.zero;
                collision.gameObject.SetActive(true);
            }
        }
    }
}
