using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopBehaviour : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("0.MainGame");
    }
}
