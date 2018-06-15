using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour {

  private bool hasPlayer;

  private void OnTriggerStay(Collider other) {
    if (other.gameObject.name == "FPSController") hasPlayer = true;
  }

  private void OnTriggerExit(Collider other) {
    if (other.gameObject.name == "FPSController") hasPlayer = false;
  }

  public bool HasPlayer {
    get {
      return this.hasPlayer;
    }
  }

}
