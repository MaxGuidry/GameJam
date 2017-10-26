using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void QuitGame()
    {
        StartCoroutine(Exit());
    }

    public void StartGame()
    {
        StartCoroutine(Load());
    }

    public IEnumerator Load()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("0.MainGame");
    }

    public IEnumerator Exit()
    {
        yield return new WaitForSeconds(2);
        Application.Quit();
    }
}
