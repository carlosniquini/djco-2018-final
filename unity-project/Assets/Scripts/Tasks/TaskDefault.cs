using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDefault : Task {

  private Player player;
  private ScreenTips screenTips;
  private AppPhotos appPhotos;
  private float dist;
  private bool played = false;
  private GameController gameController;
  private Options options;
  private bool hasPlayer;

  // Use this for initialization
  void Start () {
    player = GameObject.Find("FPSController").GetComponent<Player>();
    screenTips = GameObject.Find("ScreenTips").GetComponent<ScreenTips>();
    appPhotos = GameObject.Find("AppPhotos").GetComponent<AppPhotos>();
    gameController = GameObject.Find("GameController").GetComponent<GameController>();
    options = GameObject.Find("Options").GetComponent<Options>();
  }

  // Update is called once per frame
  void Update () {
    dist = Vector3.Distance(gameObject.transform.position, player.GetComponent<Transform>().position);
    IsNear();
    if (action) TaskAction();
    if (dialog) TaskDialog();
    //float aux = (dist - 2) / (400 - 2);
    //Debug.Log(3 - aux * 2.9);
  }

  private void TaskAction() {
    if (!status && hasPlayer && Input.GetKeyDown("e")) {
      if (!status && HasAllItens()) {
        //Debug.Log("Sucesso querida!");
        screenTips.ShowTip("New photo added to Gallery");
        status = true;
        player.ImproveMemory();
        foreach (Photo p in photosToAdd) {
          appPhotos.AddPhotoInList(p);
        }
      } else {
        //gameController.PlayDialogue(wrong_dialogue);
        screenTips.ShowTip("Find all the items");
        //Debug.Log("Huum... eu deveria procurar todos os itens.");
      }
    }
  }

  private void TaskDialog() {
    if (hasPlayer && Input.GetKeyDown("e")) {
      if (!played) {
        StartCoroutine(player.VisualStingerFocus());
        //player.ImproveMemory();
      }
      gameController.PlayDialogue(intro_dialogue);
      played = true;
    }
  }

  private bool HasAllItens() {
    foreach (int i in itens) {
      if (!player.HasItem(i)) return false;
    }
    return true;
  }

  public float Dist() {
    float aux = (dist - 2) / (400 - 2);
    return (4 - aux * 3.9f);
  }
  /*
  private void OnTriggerStay(Collider other) {
  if (other.gameObject.name == "FPSController" && !played && hasPlayer) {
    if (Input.GetKeyDown("e")) {
      gameController.PlayDialogue(intro_dialogue);
      StartCoroutine(player.VisualStingerFocus());
      player.ImproveMemory();
      played = true;
    }
  }
  }*/

  private void IsNear() {
    float dist = Vector3.Distance(gameObject.transform.position, player.GetComponent<Transform>().position);
    if (dist <= 3f || (player.LookAt != null && player.LookAt.name == this.name)) {
      hasPlayer = true;
      options.SetTarget(this);
      options.ShowOptions();
    } else {
      hasPlayer = false;
      options.HideOptions(this.gameObject);
    }
  }

  public Player Player {
    get {
      return this.player;
    }
  }

  public void LastTask() {
    StartCoroutine(LastTaskCorountine());
  }

  private IEnumerator LastTaskCorountine() {
    yield return new WaitForSeconds(1);
    gameController.Completed();
  }
}
