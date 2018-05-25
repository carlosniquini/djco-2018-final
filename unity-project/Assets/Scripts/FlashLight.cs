using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour {
	public Light flashlight;  // this will be the flashlight that we will use.
	public bool activateFlashlight; // this will determine whether our flashlight is  on or off;
	public bool reduceIntensity;
	public float flashlightBattery; //this will be the battery of our flashlight;

	// Use this for initialization
	void Start () {
		//here we give reference to the flashlight;
		flashlight = GetComponent<Light>();
		flashlightBattery = 100;
	}

	// Update is called once per frame
	void Update () {
		if (flashlightBattery <= 0) {

			flashlightBattery = 0;
		}
		// here we do all the scripting needed to make our flashklight work
		flashlight.enabled = activateFlashlight;
		if (Input.GetKeyDown ("f")) {
			activateFlashlight = !activateFlashlight;
		}
		// this section controls the battery system of flashlight;
		if (activateFlashlight) {

			flashlightBattery -= Time.time * 0.001f; // here we reduce the flashlight's battery


		}
		if (flashlightBattery <= 80) {
			reduceIntensity = true;
		}

		else if (flashlightBattery >= 80) {
			reduceIntensity = false;
		}
		//**********************
		if (reduceIntensity) {
			flashlight.intensity = flashlightBattery / 10;

		}
	}
}