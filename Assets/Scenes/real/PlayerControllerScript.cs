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
	// 1. Declare Variables
	Thread receiveThread; //1
	UdpClient client; //2
	public Camera cam;
	int port; //3
	coordinate myObject;

	//public GameObject Player; //4
	//AudioSource jumpSound; //5
	//bool jump; //6

	// 2. Initialize variables
	void Start () 
	{
		port = 5065; //1 
		//jump = false; //2 
		//jumpSound = gameObject.GetComponent<AudioSource>(); //3

		InitUDP(); //4
	}

	// 3. InitUDP
	private void InitUDP()
	{
		print ("UDP Initialized");

		receiveThread = new Thread (new ThreadStart(ReceiveData)); //1 
		receiveThread.IsBackground = true; //2
		receiveThread.Start (); //3

	}

	// 4. Receive Data
	private void ReceiveData()
	{
		client = new UdpClient (port); //1
		while (true) //2
		{
			try
			{
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port); //3
				byte[] data = client.Receive(ref anyIP); //4
				string text = Encoding.UTF8.GetString(data); //5
				myObject = JsonUtility.FromJson<coordinate>(text);
				myObject.Convert();
				print (">> " + myObject.x0 + " " + myObject.y0 + " " + myObject.nums);
				
				//Vector2 position = new Vector2(myObject.x , myObject.y);
				//var newPlayer = Instantiate(player);
				//newPlayer.transform.position = position;
				

			} catch(Exception e)
			{
				print (e.ToString()); //7
			}
		}
	}


	// 6. Check for variable value, and make the Player Jump!
	void Update () 
	{
        if (myObject != null)
        {
			player.transform.position = new Vector2(myObject.x0, myObject.y0);
			if (player.transform.position.x < -0.89f)
			{
				player.transform.Translate(Vector2.left);
			}
			player2.transform.position = new Vector2(myObject.x1, myObject.y1);
			if (player2.transform.position.x < -0.89f)
			{
				player2.transform.Translate(Vector2.left);
			}
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
	public float x2;
	public float y2;

	public void Convert()
    {

		x0 = 18.0f / 960 * x0 - 9;
		x1 = 18.0f / 960 * x1 - 9;
		x2 = 18.0f / 960 * x2 - 9;
		y0 = 10.0f / 540 * y0 - 5;
		y1 = 10.0f / 540 * y1 - 5;
		y2 = 10.0f / 540 * y2 - 5;
	}
}
