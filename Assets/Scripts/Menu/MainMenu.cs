using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //canvas panels
   public GameObject menu;
   public GameObject about;
   public GameObject credits;


    public void StartGame() {
        Debug.Log("Game Starting!");
    }
    public void OpenAbout() {
        Debug.Log("Opening About Page!");
        ShowAbout();
    }
    public void OpenCredits() {
        Debug.Log("Opening Credits Page!");
        ShowCredits();
    }
    public void ShowMain() {
        about.SetActive(false);
        credits.SetActive(false);
        menu.SetActive(true);
    }
    public void ShowAbout() {
        credits.SetActive(false);
        menu.SetActive(false);
        about.SetActive(true);
    }
    public void ShowCredits() {
        menu.SetActive(false);
        about.SetActive(false);
        credits.SetActive(true);
    }
}
