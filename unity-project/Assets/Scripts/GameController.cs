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

  }

  // Update is called once per frame
  void Update () {
    //SwitchLakeSounds();
  }
}
