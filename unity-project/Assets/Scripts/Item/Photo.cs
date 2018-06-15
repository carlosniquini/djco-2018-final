using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Photo : MonoBehaviour {

  public int _id;
  public string _name;
  public Sprite image_low;
  public Sprite image_high;
  public AudioClip dialog;
  private bool inGalery;
  private GameController gameController;

  // Use this for initialization
  void Start () {
    gameController = GameObject.Find("GameController").GetComponent<GameController>();
  }

  // Update is called once per frame
  void Update () {

  }

  public void Load(Photo photo, AppPhotos appPhoto) {
    this._id = photo._id;
    this._name = photo._name;
    this.image_low = photo.image_low;
    this.image_high = photo.image_high;
    this.dialog = photo.dialog;
    gameObject.AddComponent<Image>().sprite = photo.image_low;
    gameObject.AddComponent<Button>().onClick.AddListener(delegate {
      if (this.dialog != null) StartCoroutine(Dialog());
      appPhoto.ShowImage(this.image_high);
    });
  }

  private IEnumerator Dialog() {
    yield return new WaitForSeconds(1);
    gameController.PlayDialogue(this.dialog);
  }
}
