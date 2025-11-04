using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public string StartNarrative;
    public string CodexScreen;
    public string Credits;
    public string level1;
    public string StartMenu;
    public string CodexNxtPage;
    public void OnCodexButton()
    {
        SceneManager.LoadScene(CodexScreen);
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
    public void OnStartButton()
    {//Alien getting molested
        SceneManager.LoadScene(StartNarrative);
    }
    public void OnCreditsButton()
    {
        SceneManager.LoadScene(Credits);
    }
    public void OnContinueButton()
    {
        SceneManager.LoadScene("LabFloorGenerate");
    }
    public void OnStartMenuButton()
    {
        SceneManager.LoadScene(StartMenu);
    }
    public void OnCodex2Button()
    {
        SceneManager.LoadScene(CodexNxtPage);
    }
    public void OnBackButton()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void OnOptionsButton()
    {
        SceneManager.LoadScene("Options");
    }
}
