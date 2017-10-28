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
    public GameObject Playerp, Enemyp;
    public StatSciptable stats;
    public Vector3 initial1, initial2, initial3, initial4;
    public Text gameText, overText;
    public Image sword1, sword2;
    private int level = 1;

    public void Start()
    {
        stats = Instantiate(stats);
        initial1 = sword1.transform.position;
        initial2 = sword2.transform.position;
        initial3 = gameText.transform.position;
        initial4 = overText.transform.position;
    }
    public void Play()
    {
        level++;

        Destroy(Player.gameObject);
        GameObject g = Instantiate(Playerp, PlayerStart.transform.position, Quaternion.identity);
        PlayerController pc = g.GetComponent<PlayerController>();
        
        for (int i = 0; i < level; i++)
        {
            GameObject e = Instantiate(Enemyp, EnemyStart.transform.position + new Vector3(i, 0, 0), Quaternion.identity);
            e.GetComponent<MonsterBehavior>().target = g.transform;
            e.GetComponent<MonsterBehavior>().stats = new StatSciptable();
            StatSciptable origin = e.GetComponent<MonsterBehavior>().stats;
            foreach (var statDictionaryKey in origin.StatDictionary.Keys)
            {
                e.GetComponent<MonsterBehavior>().stats.StatDictionary.Add(statDictionaryKey,origin.StatDictionary[statDictionaryKey]);
                e.GetComponent<MonsterBehavior>().stats.GetStat("EnemyHealth").Value = 100;
            }
        }
        GameObject.FindObjectOfType<EnemyUI>().Enemy = FindObjectOfType<MonsterBehavior>();
        GameObject.FindObjectOfType<PlayerUI>().Player = FindObjectOfType<PlayerController>();
        
        

        pc.stats.GetStat("PlayerHealth").Value = 100;
        Camera.main.gameObject.GetComponent<CameraController>().player = g;
        inGameUI.SetActive(true);
        Shop.SetActive(false);
    }

    public void Buy()
    {
        GameObject.FindObjectOfType<EnemyUI>().StopAllCoroutines();
        GameObject.FindObjectOfType<PlayerUI>().StopAllCoroutines();
        stats.GetStat("PlayerDamage").Value += 5;
        sword1.transform.position = initial1;
        sword2.transform.position = initial2;
        gameText.transform.position = initial3;
        overText.transform.position = initial4;
        Play();
    }

    public void Menu()
    {
        SceneManager.LoadScene("3.MainMenu");
    }
}
