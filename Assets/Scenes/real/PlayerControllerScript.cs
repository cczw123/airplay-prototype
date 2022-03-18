using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class PlayerControllerScript: MonoBehaviour
{
	public GameObject cube;
	// 1. Declare Variables
	Thread receiveThread; //1
	UdpClient client; //2
	public Camera cam;
	int port; //3

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
				coordinate myObject = JsonUtility.FromJson<coordinate>(text);
				print (">> " + myObject.x + " " + myObject.y);
				Vector2 position = new Vector2(myObject.x , myObject.y);
				cube.transform.position = position;
				

			} catch(Exception e)
			{
				print (e.ToString()); //7
			}
		}
	}


	// 6. Check for variable value, and make the Player Jump!
	void Update () 
	{
		
	}
}

public class coordinate
{
	public int x;
	public int y;
}
