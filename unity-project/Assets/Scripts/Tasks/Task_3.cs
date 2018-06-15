using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_3 : TaskDefault {

  private ArrayList itens_ = new ArrayList();

  void Update() {
    //status = TaskPuzze();
    if (!status && TaskPuzze() && Player.LookAt != null && Player.LookAt.name == this.name && this.GetComponentInChildren<Spot>().HasPlayer && previous.status) {
      Debug.Log("Agora eu me lembro!");
      Player.ImproveMemory();
      status = true;
      LastTask();
    }
  }

  private bool TaskPuzze() {
    foreach (int i in itens) {
      if (!itens_.Contains(i)) return false;
    }
    return true;
  }

  private void OnTriggerEnter(Collider other) {
    Debug.Log(other.gameObject.name);
    if (other.gameObject.GetComponent<Item>() && !itens_.Contains(other)) {
      itens_.Add(other.gameObject.GetComponent<Item>()._id);
    }
  }

  private void OnTriggerExit(Collider other) {
    Debug.Log(other.gameObject.name);
    if (other.gameObject.GetComponent<Item>() && itens_.Contains(other)) {
      itens_.Remove(other.gameObject.GetComponent<Item>()._id);
    }
  }

}
