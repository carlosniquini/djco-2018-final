using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Player : MonoBehaviour {

  private Inventory inventory;
  private Animator options;
  private GameObject carrying;
  private PostProcessingProfile ppp;
  private AudioSource audioSourceSteps;
  private AudioSource audioSourceAmbiente;
  private bool underlake = false;
  private TaskDefault task_1;
  public AudioClip lake;
  public AudioClip ambiente;

  private void Awake() {
    //Options.SetOptios(GameObject.Find("Options").GetComponent<Animator>());
  }

  // Use this for initialization
  void Start () {
    inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    options = GameObject.Find("Options").GetComponent<Animator>();
    ppp = this.GetComponentInChildren<PostProcessingBehaviour>().profile;
    audioSourceSteps = this.GetComponent<AudioSource>();
    audioSourceAmbiente = this.GetComponentsInChildren<AudioSource>()[1];
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

  public bool UnderLake {
    get { return underlake; }
    set { this.underlake = value; }
  }

  private void OnTriggerExit(Collider other) {
    if (other.gameObject.tag == "lake" && gameObject.transform.position.y < other.gameObject.transform.position.y) {
      audioSourceAmbiente.clip = lake;
      audioSourceSteps.mute = true;
      audioSourceAmbiente.Play();
      UnderLake = true;
    }
    if (other.gameObject.tag == "lake" && gameObject.transform.position.y > other.gameObject.transform.position.y) {
      audioSourceAmbiente.clip = ambiente;
      audioSourceSteps.mute = false;
      audioSourceAmbiente.Play();
      UnderLake = false;
    }
  }
}
