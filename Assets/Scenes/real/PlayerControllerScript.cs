using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class PlayerControllerScript: MonoBehaviour
{
	public GameObject player;
	public GameObject player2;
	public GameObject dingwei;
	// 1. Declare Variables
	Thread receiveThread; //1
	UdpClient client; //2
	public Camera cam;
	int port; //3
	coordinate myObject;
	public bool local = false;
	public float distance;
	Vector2 position;

    //public GameObject Player; //4
    //AudioSource jumpSound; //5
    //bool jump; //6

    // 2. Initialize variables
    private void Awake()
    {
		distance = Vector2.Distance(dingwei.transform.GetChild(0).position,dingwei.transform.GetChild(1).position);
		position = dingwei.transform.GetChild(1).position;

	}
    void Start () 
	{
		port = 5065; //1 
					 //jump = false; //2 
					 //jumpSound = gameObject.GetComponent<AudioSource>(); //3

		StartCoroutine(ReceiveData());//4
	}

	// 3. InitUDP
	//private void InitUDP()
	//{
	//	print ("UDP Initialized");

	//	receiveThread = new Thread (new ThreadStart(ReceiveData)); //1 
	//	receiveThread.IsBackground = true; //2
	//	receiveThread.Start (); //3

	//}

	// 4. Receive Data
	IEnumerator  ReceiveData()
	{
		client = new UdpClient (port); //1
		while (true) //2
		{

				IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port); //3
				byte[] data = client.Receive(ref anyIP); //4
				string text = Encoding.UTF8.GetString(data); //5
				if(text == "finish")
                {
					
					local = true;
                }
                else if(local)
                {
					coordinate myObject1 = JsonUtility.FromJson<coordinate>(text);
					print(">> " + myObject1.x0 + " " + myObject1.y0 + " " + myObject1.nums);
					myObject = myObject1.Convert(distance,position);
					print(">> " + myObject.x0 + " " + myObject.y0 + " " + myObject.nums);

			}


			//Vector2 position = new Vector2(myObject.x , myObject.y);
			//var newPlayer = Instantiate(player);
			//newPlayer.transform.position = position;


			yield return new WaitForEndOfFrame();
		}
	}
	// 6. Check for variable value, and make the Player Jump!
	void Update () 
	{
        if (myObject != null)
        {
			player.transform.position = new Vector2(myObject.x0, myObject.y0);
			//if (player.transform.position.x < -0.89f)
			//{
			//	player.transform.Translate(Vector2.left);
			//}
			player2.transform.position = new Vector2(myObject.x1, myObject.y1);
			//if (player2.transform.position.x < -0.89f)
			//{
			//	player2.transform.Translate(Vector2.left);
			//}
		}
        if (local)
        {
			dingwei.SetActive(false);

		}
		
	}
}

public class coordinate
{
	public int nums = 0;
	public float x0;
	public float y0;
	public float x1;
	public float y1;

    coordinate(int nums, float x0, float y0, float x1, float y1)
    {
		this.nums = nums;
		this.x0 = x0;
		this.y0 = y0;
		this.x1 = x1;
		this.y1 = y1;
    }
	

	public coordinate Convert(float distance, Vector2 point2)
    {
		coordinate newcord = new coordinate(2, x0 * distance + point2.x, y0 * distance + point2.y, x1 * distance + point2.x, y1 * distance + point2.y);
		return newcord;

	}
}
