using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject menu;
    public GameObject credits;

    private void Start()
    {

        menu.SetActive(true);
        credits.SetActive(false);  
    }
    public void OnCreditsButton()
    {
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.UIClick, transform.position);
        menu.SetActive(false);
        credits.SetActive(true);
    }
    public void OnBackCreditsButton()
    {
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.UIClick, transform.position);
        menu.SetActive(true);
        credits.SetActive(false);
    }
    public void OnExitButton()
    {
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.UIClick, transform.position);
        Application.Quit();
    }
    public void OnPlayButton()
    {
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.UIClick, transform.position);
        SceneManager.LoadScene(1);
    }
}
