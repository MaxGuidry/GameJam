﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public List<Text> TextList;
    public AudioClip MusicClip;
    public GameObject Buttons, PauseButtons, target;
    public Slider sensitivity;
    public Toggle invertMouse;
    public Text broadCastText;

    private AudioSource _backSound;

    // Use this for initialization
    void Start()
    {
        InputMap.LoadSettings();
        _backSound = GetComponent<AudioSource>();
        _backSound.volume = 1;
        sensitivity.value = InputMap.Sensitivity;
        if (InputMap.Sensitivity < 0)
            invertMouse.isOn = true;
        _backSound.clip = MusicClip;
        _backSound.Play();
        StartCoroutine(ColorChange());
        StartCoroutine(BroadCast());
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
        TextList[0].gameObject.SetActive(false);
        broadCastText.gameObject.SetActive(false);
    }

    public void Back()
    {
        Buttons.SetActive(true);
        PauseButtons.SetActive(false);
        TextList[0].gameObject.SetActive(true);
        broadCastText.gameObject.SetActive(true);
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
        yield return new WaitForSeconds(1);
        InputMap.SaveSettings();
        SceneManager.LoadScene("0.MainGame");
    }

    public IEnumerator Exit()
    {
        yield return new WaitForSeconds(2);
        Application.Quit();
    }

    public IEnumerator BroadCast()
    {
        var startPos = broadCastText.transform.position;
        var back = false;
        while (true)
        {
            broadCastText.transform.position = Vector3.Lerp(
                broadCastText.transform.position, 
                !back ? target.transform.position : 
                startPos, Time.deltaTime);
            if (Vector3.Distance(broadCastText.transform.position, 
                target.transform.position) <= 2)
                back = true;
            if (back && Vector3.Distance(broadCastText.transform.position, 
                startPos) <= 2)
                back = false;
            yield return null;
        }
    }
}
