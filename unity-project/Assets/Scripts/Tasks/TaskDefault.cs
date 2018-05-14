using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDefault : Task {

  private Player player;
  private float dist;

  // Use this for initialization
  void Start () {
    player = GameObject.Find("FPSController").GetComponent<Player>();
  }

  // Update is called once per frame
  void Update () {
    dist = Vector3.Distance(gameObject.transform.position, player.GetComponent<Transform>().position);
    //float aux = (dist - 2) / (400 - 2);
    //Debug.Log(3 - aux * 2.9);
  }

  public float Dist() {
    float aux = (dist - 2) / (400 - 2);
    return (4 - aux * 3.9f);
  }
}
