using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    public PlayerController Player;
    public Slider PlayerHealthSlider, PlayerStaminaSlider;
    public Vector3 initial1, initial2, initial3, initial4;
    public Transform target, gameTarget, overTarget;

    public Image sword1, sword2;

    public Text gameText, overText;
    public AudioClip SoundEffect, MusicClip;

    private AudioSource _backSound;
    private bool restartGame;
    // Use this for initialization
    void Start()
    {
        //initial1 = sword1.transform.position;
        //initial2 = sword2.transform.position;
        //initial3 = gameText.transform.position;
        //initial4 = overText.transform.position;
        _backSound = GetComponent<AudioSource>();
        restartGame = false;
        PlayerHealthSlider.maxValue = Player.stats.GetStat("PlayerHealth").Value;
        PlayerStaminaSlider.maxValue = Player.stats.GetStat("PlayerStamina").Value;
        StartCoroutine(GameOver());
        StartCoroutine(RestartGame());
    }
    [ContextMenu("Test Death")]
    public void TestGameOver()
    {
        StopAllCoroutines();
        sword1.transform.position = initial1;
        sword2.transform.position = initial2;
        gameText.transform.position = initial3;
        overText.transform.position = initial4;
        StartCoroutine(GameOver());
    }
    // Update is called once per frame
    void Update()
    {
        if (!Player) return;
        PlayerHealthSlider.value = Player.stats.GetStat("PlayerHealth").Value;
        PlayerStaminaSlider.value = Player.stats.GetStat("PlayerStamina").Value;
        if (Input.GetKeyDown(KeyCode.L))
            Player.TakeDamage(100);
    }

    public IEnumerator RestartGame()
    {
        while (true)
        {
            if (restartGame)
            {
                _backSound.clip = SoundEffect;
                _backSound.Play();
                yield return new WaitForSeconds(2);
                SceneManager.LoadScene("3.MainMenu");
            }
            yield return null;
        }
    }
    public AnimationCurve slamEffect;

    public IEnumerator GameOver()
    {
        var startpos1 = sword1.transform.position;
        var startpos2 = sword2.transform.position;
        var startpos3 = gameText.transform.position;
        var startpos4 = overText.transform.position;
        var timer = 0f;
        var totalTime = 2f;
        while (true)
        {
            if (!Player)
            {
                var swordEffect = slamEffect.Evaluate(timer / totalTime);

                sword1.transform.position = Vector3.Lerp(
                    startpos1,
                    target.transform.position,
                    swordEffect);

                sword2.transform.position = Vector3.Lerp(
                    startpos2,
                    target.transform.position,
                    swordEffect);

                gameText.transform.position = Vector3.Lerp(
                    startpos3,
                    gameTarget.transform.position,
                    swordEffect);

                overText.transform.position = Vector3.Lerp(
                    startpos4,
                    overTarget.transform.position,
                    swordEffect);
                timer += Time.deltaTime;

                if (sword1.transform.position == target.transform.position)
                    restartGame = true;
            }
            yield return null;
        }
    }
}
