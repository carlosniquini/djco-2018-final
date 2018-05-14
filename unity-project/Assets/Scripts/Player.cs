using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Player : MonoBehaviour {

  private Inventory inventory;
  private Animator options;
  private GameObject carrying;
  private PostProcessingProfile ppp;
  private TaskDefault task_1;

  private void Awake() {
    //Options.SetOptios(GameObject.Find("Options").GetComponent<Animator>());
  }

  // Use this for initialization
  void Start () {
    inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    options = GameObject.Find("Options").GetComponent<Animator>();
    ppp = this.GetComponentInChildren<PostProcessingBehaviour>().profile;
    task_1 = GameObject.Find("Task1").GetComponent<TaskDefault>();
    //var aux = ppp.depthOfField.settings;
    //aux.focusDistance = 5f;
    //ppp.depthOfField.settings = aux;
  }

  // Update is called once per frame
  void Update () {
    if (Input.GetKeyDown("i")) {
      inventory.ShowInventory();
    }
    var aux = ppp.depthOfField.settings;
    aux.focusDistance = task_1.Dist();
    ppp.depthOfField.settings = aux;
  }

  public void AddItem(Item item) {
    inventory.AddItem(item);
  }

  public void ShowOptions(bool s) {
    options.SetBool("isDisplayed", s);
  }

  public GameObject Carrying {
    get { return carrying; }
    set { this.carrying = value; }
  }
}
