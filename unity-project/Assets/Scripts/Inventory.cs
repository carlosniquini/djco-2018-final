using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

  private FirstPersonController fpsController;
  private ArrayList items = new ArrayList();
  private bool isDisplayed = false;
  private Player player;
  private GameObject panel;

  // Use this for initialization
  void Start () {
    fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
    player = GameObject.Find("FPSController").GetComponent<Player>();
    panel = GameObject.Find("Panel");
  }

  // Update is called once per frame
  void Update () {

  }

  public void UpdateInventory() {
    foreach (Image image in panel.GetComponentsInChildren<Image>()) {
      Destroy(image);
    }
    foreach (Item item in items) {
      GameObject gameObject = new GameObject(item._name);
      gameObject.AddComponent<Image>().sprite = item.image;
      gameObject.transform.parent = panel.transform;
      //Debug.Log(item._name);
    }
  }

  public void ShowInventory() {
    this.GetComponent<Animator>().SetBool("isDisplayed", isDisplayed = !isDisplayed);
    fpsController.m_MouseLook.SetCursorLock(!isDisplayed);
  }

  public void AddItem(Item item) {
    items.Add(item);
    //UpdateInventory();
    GameObject gameObject = new GameObject(item._name);
    gameObject.AddComponent<Image>().sprite = item.image;
    gameObject.transform.parent = panel.transform;

  }
}
