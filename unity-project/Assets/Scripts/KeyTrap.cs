using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTrap : MonoBehaviour
{

	public KeyCode key;
	private bool active = false;
	private GameObject keyDown;


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (key) && active) {
			Destroy (keyDown);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		active = true;
		if (col.gameObject.tag == "FallingKey") {
			keyDown = col.gameObject;
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		active = false;
	}
}
