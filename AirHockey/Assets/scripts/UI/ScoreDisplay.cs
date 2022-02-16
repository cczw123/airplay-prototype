using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public Text leftTeam;
    public Text rightTeam;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        leftTeam.text = GameManager.Inst.scores[0].ToString();
        rightTeam.text = GameManager.Inst.scores[1].ToString();
    }
}