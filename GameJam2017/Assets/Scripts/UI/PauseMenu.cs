using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject Pausemenu;

    private bool paused = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(InputMap.KeyBinds["Pause"]))
        {
            paused = !paused;
            Time.timeScale = (paused) ? 0 : 1;
            Camera.main.gameObject.GetComponent<CameraController>().enabled =
                !Camera.main.gameObject.GetComponent<CameraController>().enabled;
            Pausemenu.SetActive(paused);
        }
    }
}
