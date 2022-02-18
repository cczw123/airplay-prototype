using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Text> leftTeam = new List<Text>();
    public List<Text> rightTeam = new List<Text>();

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var left in leftTeam)
        {
            left.text = GameManager.Inst.scores[0].ToString("D2");
        }

        foreach (var right in rightTeam)
        {
            right.text = GameManager.Inst.scores[1].ToString("D2");
        }
    }
}