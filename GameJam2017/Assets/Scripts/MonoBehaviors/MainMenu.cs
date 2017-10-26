using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public List<Text> TextList;
    public AudioClip MusicClip;

    public GameObject Buttons, PauseButtons;
    private AudioSource _backSound;

    // Use this for initialization
    void Start()
    {
        _backSound = GetComponent<AudioSource>();
        _backSound.volume = 1;
        _backSound.clip = MusicClip;
        _backSound.Play();
        StartCoroutine(ColorChange());
    }

    void Update()
    {

    }

    public IEnumerator ColorChange()
    { 
        while (true)
        {
            foreach (var text in TextList)
            {
                text.GetComponent<Outline>().effectColor = Color.Lerp(
                    Color.red,
                    Color.cyan,
                    Mathf.PingPong(Time.time, 1));
            }
            yield return null;
        }
    }
    public void QuitGame()
    {
        StartCoroutine(Exit());
    }

    public void StartGame()
    {
        StartCoroutine(Load());
    }

    public void Options()
    {
        Buttons.SetActive(false);
        PauseButtons.SetActive(true);
    }

    public void Back()
    {
        Buttons.SetActive(true);
        PauseButtons.SetActive(false);
    }

    public void InvertMouse()
    {
        InputMap.Sensitivity *= -1;
    }

    public void MouseSensitivitySlider(float value)
    {
      InputMap.Sensitivity = value;
    }

    public void AudioSlider(float value)
    {
        _backSound.volume = value;
    }

    public IEnumerator Load()
    {
        yield return new WaitForSeconds(2);
        InputMap.SaveSettings();
        SceneManager.LoadScene("0.MainGame");
    }

    public IEnumerator Exit()
    {
        yield return new WaitForSeconds(2);
        Application.Quit();
    }
}
