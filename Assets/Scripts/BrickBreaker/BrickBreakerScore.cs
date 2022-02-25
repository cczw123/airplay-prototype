using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickBreakerScore : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Text> scores = new List<Text>();

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var score in scores)
        {
            score.text = BrickBreakerGameManager.Inst.score.ToString("D2");
        }
    }
}
