
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
	public float xSpread;
	public float zSpread;
	public float yOffset;
	public Transform centerPoint;

	public float rotSpeed;
	//public bool rotateClockwise;

	float timer = 1f;

	// Update is called once per frame
	void Update()
	{
		timer += Time.deltaTime * rotSpeed;
		//Rotate();

		//float x = -Mathf.Cos(timer) * xSpread;
		//float z = Mathf.Sin(timer) * zSpread;
		//Vector3 pos = new Vector3(x, yOffset, z);
		//transform.position = pos + centerPoint.position;
		//}
		//else
		//{
		float x = Mathf.Cos(timer) * xSpread;
		float z = Mathf.Sin(timer) * zSpread;
		Vector3 pos = new Vector3(x, yOffset, z);
		transform.position = pos + centerPoint.position;
		//}
	}
}
