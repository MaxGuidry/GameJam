using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public MonsterBehavior Enemy;
    public Slider EnemyHealthSlider;
    public Transform target, gameTarget, overTarget;

    public Image sword1, sword2;

    public Text youText, winText, EnemyHitPoints;
    public AudioClip MusicClip;
    public GameObject inGameUI,Shop;
    private AudioSource _backSound;
    private bool restartGame, playOnce;
    // Use this for initialization
    void Start()
    {
        _backSound = GetComponent<AudioSource>();
        playOnce = false;
        restartGame = false;
        EnemyHealthSlider.maxValue = Enemy.stats.GetStat("EnemyHealth").Value;
        EnemyHitPoints.text = Enemy.stats.GetStat("EnemyHealth").Value.ToString();
        StartCoroutine(GameOver());
        StartCoroutine(RestartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enemy) return;
        EnemyHealthSlider.value = Enemy.stats.GetStat("EnemyHealth").Value;
        EnemyHitPoints.text = Enemy.stats.GetStat("EnemyHealth").Value.ToString();
        if (Input.GetKeyDown(KeyCode.Space))
            Enemy.TakeDamage(100);
    }
    public IEnumerator RestartGame()
    {
        while (true)
        {
            if (restartGame)
            {
                if (!playOnce)
                {
                    _backSound.clip = MusicClip;
                    _backSound.Play();
                    playOnce = true;
                }
                yield return new WaitForSeconds(2);
                inGameUI.SetActive(false);
                Shop.SetActive(true);
            }
            yield return null;
        }
    }
    public AnimationCurve slamEffect;

    public IEnumerator GameOver()
    {
        var startpos1 = sword1.transform.position;
        var startpos2 = sword2.transform.position;
        var startpos3 = youText.transform.position;
        var startpos4 = winText.transform.position;
        var timer = 0f;
        var totalTime = 2f;
        while (true)
        {
            if (!Enemy)
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

                youText.transform.position = Vector3.Lerp(
                    startpos3,
                    gameTarget.transform.position,
                    swordEffect);

                winText.transform.position = Vector3.Lerp(
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
