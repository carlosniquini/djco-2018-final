using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Photo : MonoBehaviour {

  public int _id;
  public string _name;
  public Sprite image_low;
  public Sprite image_high;
  private bool inGalery;

  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {

  }

  public void Load(Photo photo, AppPhotos appPhoto) {
    this._id = photo._id;
    this._name = photo._name;
    this.image_low = photo.image_low;
    this.image_high = photo.image_high;
    gameObject.AddComponent<Image>().sprite = photo.image_low;
    gameObject.AddComponent<Button>().onClick.AddListener(delegate {
      appPhoto.ShowImage(this.image_high);
    });
  }
}
