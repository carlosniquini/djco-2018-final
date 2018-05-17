using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
  public Image FullBatteryImage;
  public Image RedBatteryImage;
  //public Image batterySaving; //If we make a battery saving mode
  private float timeLeft;
  public float MissingPercentage = 15;
  public Text percentageText;
  public float TotalTimeOfGameInMinutes = 250; //max time of the game in MINUTOS --> 15000 sec = 250 min = 4.16 horas
  public float RedBatteryStartsInMinutes = 20;
  public GameObject gameOverText;
  private Text clock;
  private int hh, mm;

  // Use this for initialization
  void Start () {

    gameOverText.SetActive(false);
    float percent = TotalTimeOfGameInMinutes * MissingPercentage / 100; //percentage that lack of battery to the cell
    timeLeft = (TotalTimeOfGameInMinutes - percent) * 60;
    percentageText.text = (100 - MissingPercentage).ToString() + "%";

  }

  // Update is called once per frame
  void Update () {
    //Debug.Log(TotalTimeOfGameInMinutes * 60 - timeLeft);
    if (timeLeft > RedBatteryStartsInMinutes * 60) {

      RedBatteryImage.enabled = false;
      timeLeft -= Time.deltaTime;
      FullBatteryImage.fillAmount = timeLeft / ((TotalTimeOfGameInMinutes) * 60 );
      updatePercentage();

    } else if (timeLeft > 0) { // Low battery

      RedBatteryImage.enabled = true;
      FullBatteryImage.enabled = false;
      timeLeft -= Time.deltaTime;
      RedBatteryImage.fillAmount = timeLeft / (TotalTimeOfGameInMinutes * 60 );
      updatePercentage();

    } else {
      //TIME OUT GAME OVER!!!
      GameOver();
    }
  }

  void GameOver() {

    gameOverText.SetActive(true);
    Time.timeScale = 0;

  }

  void updatePercentage() {

    percentageText.text = (Math.Round(this.timeLeft / ((this.TotalTimeOfGameInMinutes) * 60 ) * 100)).ToString() + "%";

  }
}
