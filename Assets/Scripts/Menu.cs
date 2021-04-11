using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void clicouStart() {
        SceneManager.LoadScene("game");
    }

    public void clicouOptions() {
        SceneManager.LoadScene("options");
    }

    public void clicouMainMenu() {
        SceneManager.LoadScene("menu");
    }

    public void clicouBackMainMenuOptions() {

        Dropdown brickDD = GameObject.Find("brickDD").GetComponent<Dropdown>();
        Dropdown skyDD = GameObject.Find("skyDD").GetComponent<Dropdown>();
        Dropdown musicDD = GameObject.Find("musicDD").GetComponent<Dropdown>();

        PlayerPrefs.SetInt("brick", brickDD.value);
        PlayerPrefs.SetInt("sky", skyDD.value);
        PlayerPrefs.SetInt("music", musicDD.value);


        SceneManager.LoadScene("menu");
    }

    public void clicouQuit() {
        Application.Quit();
    }
}
