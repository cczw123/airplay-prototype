using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tryondestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        Debug.Log(1);
    }
}
