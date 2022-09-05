using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosPos = RosMessageTypes.ApInterfaces.PosMsg;

public class posmessage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objects1;
    public GameObject objects2;
    public GameObject objects3;
    public GameObject objects4;

    private void Awake()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }

    void Start()
    {
        //objects1.SetActive(true); //test for DrawCircle
        //objects1.transform.position = new Vector3(3f, 4f, 0f);
        //objects2.transform.position = new Vector3(0f, 4f, 0f);
        //DrawCircle(objects1, 200, 1.5f, 0.2f);
        //DrawCircle(objects2, 200, 0.7f, 0.2f);
        ROSConnection.GetOrCreateInstance().Subscribe<RosPos>("pos_raw", posChange);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void posChange(RosPos rosPos)
    {
        objects1.SetActive(false);
        objects2.SetActive(false);
        objects3.SetActive(false);
        objects4.SetActive(false);
        int player_num = rosPos.total;
        float[] x = new float[4] { 0.0f, 0.0f, 0.0f, 0.0f };
        float[] y = new float[4] { 0.0f, 0.0f, 0.0f, 0.0f };
        float[] r = new float[4] { 0.0f, 0.0f, 0.0f, 0.0f };
        for (int i = 0; i < player_num; i++)
        {
            x[i] = (float)rosPos.x[i];
            y[i] = (float)rosPos.y[i];
            r[i] = (float)rosPos.size[i]/40f;

        }

        if (player_num == 1)
        {
            objects1.SetActive(true);
            var pos1 = Camera.main.ScreenToWorldPoint(new Vector3(x[0], y[0], Camera.main.nearClipPlane));
            //pos1 = Normlization(pos1);
            pos1.z = 0;
            objects1.transform.position = pos1;
            DrawCircle(objects1, 200, r[0], 0.2f);
        }
        if (player_num == 2)
        {
            objects1.SetActive(true);
            var pos1 = Camera.main.ScreenToWorldPoint(new Vector3(x[0], y[0], Camera.main.nearClipPlane));
            pos1.z = 0f;
            objects1.transform.position = pos1;
            objects2.SetActive(true);
            var pos2 = Camera.main.ScreenToWorldPoint(new Vector3(x[1], y[1], Camera.main.nearClipPlane));
            pos2.z = 0f;
            objects2.transform.position = pos2;
            DrawCircle(objects1, 200, r[0], 0.2f);
            DrawCircle(objects2, 200, r[1], 0.2f);
        }

        if (player_num == 3)
        {
            objects1.SetActive(true);
            var pos1 = Camera.main.ScreenToWorldPoint(new Vector3(x[0], y[0], Camera.main.nearClipPlane));
            pos1.z = 0;
            objects1.transform.position = pos1;
            objects2.SetActive(true);
            var pos2 = Camera.main.ScreenToWorldPoint(new Vector3(x[1], y[1], Camera.main.nearClipPlane));
            pos2.z = 0;
            objects2.transform.position = pos2;
            objects3.SetActive(true);
            var pos3 = Camera.main.ScreenToWorldPoint(new Vector3(x[2], y[2], Camera.main.nearClipPlane));
            pos3.z = 0;
            objects3.transform.position = pos3;
            DrawCircle(objects1, 200, r[0], 0.2f);
            DrawCircle(objects2, 200, r[1], 0.2f);
            DrawCircle(objects3, 200, r[2], 0.2f);
        }

        if (player_num == 4)
        {
            objects1.SetActive(true);
            var pos1 = Camera.main.ScreenToWorldPoint(new Vector3(x[0], y[0], Camera.main.nearClipPlane));
            pos1.z = 0;
            objects1.transform.position = pos1;
            objects2.SetActive(true);
            var pos2 = Camera.main.ScreenToWorldPoint(new Vector3(x[1], y[1], Camera.main.nearClipPlane));
            pos2.z = 0;
            objects2.transform.position = pos2;
            objects3.SetActive(true);
            var pos3 = Camera.main.ScreenToWorldPoint(new Vector3(x[2], y[2], Camera.main.nearClipPlane));
            pos3.z = 0;
            objects3.transform.position = pos3;
            objects4.SetActive(true);
            var pos4 = Camera.main.ScreenToWorldPoint(new Vector3(x[3], y[3], Camera.main.nearClipPlane));
            pos4.z = 0;
            objects4.transform.position = pos4;
            DrawCircle(objects1, 200, r[0], 0.2f);
            DrawCircle(objects2, 200, r[1], 0.2f);
            DrawCircle(objects3, 200, r[2], 0.2f);
            DrawCircle(objects4, 200, r[3], 0.2f);

        }



        //var x = (float)rosPos.x[0];
        //var y = (float)rosPos.y[0];
        //var pos = Camera.main.ScreenToWorldPoint(new Vector3(x, y,Camera.main.nearClipPlane));//convert to normal coordinate
        // pos.z = 0;
        Debug.Log(rosPos.total + " and " + rosPos.x[0] +  " and " + rosPos.y[0] + "and" + rosPos.size );
        // Debug.Log(rosPos.total);
        // Debug.Log(rosPos.size[0] + "and" + rosPos.size[1] +"and"+ rosPos.size[2]);
    }

    void DrawCircle(GameObject object_temp, int steps, float radius, float width)
    {
        object_temp.GetComponent<LineRenderer>().positionCount = (steps + 1);
        object_temp.GetComponent<LineRenderer>().startWidth = width;

        for (int i = 0; i < steps + 1; i++)
        {
            float x = radius * Mathf.Cos((360f / steps * i) * Mathf.Deg2Rad) + object_temp.transform.position.x; 
            float y = radius * Mathf.Sin((360f / steps * i) * Mathf.Deg2Rad) + object_temp.transform.position.y;
            object_temp.GetComponent<LineRenderer>().SetPosition(i, new Vector3(x, y, object_temp.transform.position.z));
        }

    }

    Vector3 Normlization(Vector3 worldpoint)
    {
        Vector3 normlized_point;
        normlized_point.x = worldpoint.x / worldpoint.z;
        normlized_point.y = worldpoint.y / worldpoint.z;
        normlized_point.z = 0;
        return normlized_point;
    }
}
