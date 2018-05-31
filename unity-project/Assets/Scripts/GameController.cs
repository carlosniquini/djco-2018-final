using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class GameController : MonoBehaviour {

  private Player player;
  public AudioSource[] lakeSounds;


  // Use this for initialization
  void Start () {
    player = GameObject.Find("FPSController").GetComponent<Player>();
  }

  void SwitchLakeSounds() {
    foreach (AudioSource a in lakeSounds) {
      foreach (AudioSource b in lakeSounds) {
        if (a != b && Vector3.Distance(a.gameObject.transform.position, player.transform.position) < Vector3.Distance(b.gameObject.transform.position, player.transform.position)) {
          a.mute = false;
          b.mute = true;
        }
      }
    }
  }

  // Update is called once per frame
  void Update () {
    SwitchLakeSounds();
  }
}
