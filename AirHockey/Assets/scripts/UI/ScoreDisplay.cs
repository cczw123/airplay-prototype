using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    Text team1;
    Text team2;

    void Start()
    {
        team1 = transform.Find("team1").GetComponent<Text>();
        team2 = transform.Find("team2").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        team1.text = GameManager.Inst.playerPrefab.GetComponent<PlayerManager>().team1.ToString();
        team2.text = GameManager.Inst.playerPrefab.GetComponent<PlayerManager>().team2.ToString();
    }
}