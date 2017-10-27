using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopBehaviour : MonoBehaviour
{
    public PlayerController Player;
    public GameObject PlayerStart, EnemyStart;
    public GameObject inGameUI, Shop;
    public void Play()
    {
        StopAllCoroutines();
        inGameUI.SetActive(true);
        Shop.SetActive(false);
    }
}
