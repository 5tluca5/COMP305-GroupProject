using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    [Header("Health")]
    [Header("Player 1")]
    [SerializeField] AttributeBar player1HealthBar;
    [SerializeField] TMP_Text player1HealthText;
    [Header("Player 2")]
    [SerializeField] AttributeBar player2HealthBar;
    [SerializeField] TMP_Text player2HealthText;

    [Header("Coin")]
    [SerializeField] TMP_Text curCoinText;
    [SerializeField] GameObject enemyDropCoinPrefab;

    [Header("Level")]
    [SerializeField] TMP_Text curLevelText;

    [Header("Other UI")]
    [SerializeField] GameObject gameOverPage;
    [SerializeField] GameObject gameClearPage;

    PlayerController player1;
    PlayerController player2;

    float playerMaxHp = 150f;
    Canvas canvas;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();

        var p1 = GameObject.FindGameObjectWithTag("Player");
        var p2 = GameObject.FindGameObjectWithTag("Player2");

        if(p1 != null)
            player1 = p1.GetComponent<PlayerController>();

        if(p2 != null)
            player2 = p2.GetComponent<PlayerController>();


        playerMaxHp = player1.GetTankStat().health;

        player1HealthBar.Setup(playerMaxHp);
        player1.SubscribeCurrentHealth().Subscribe(hp =>
        {
            player1HealthBar.Setup(playerMaxHp);
            player1HealthBar.SetValue(hp);

            player1HealthText.text = hp.ToString() + " / " + playerMaxHp.ToString();
        }).AddTo(this);

        if(player2 != null)
        {
            player2HealthBar.transform.parent.gameObject.SetActive(true);

            player2HealthBar.Setup(playerMaxHp);
            player2.SubscribeCurrentHealth().Subscribe(hp =>
            {
                player2HealthBar.Setup(playerMaxHp);
                player2HealthBar.SetValue(hp);

                player2HealthText.text = hp.ToString() + " / " + playerMaxHp.ToString();
            }).AddTo(this);
        }

        GameManager.Instance.IsGameOver.Subscribe(x =>
        {
            if (x)
                gameOverPage.SetActive(true);
        }).AddTo(this);

        GameManager.Instance.IsGameClear.Subscribe(x =>
        {
            if (x)
                gameClearPage.SetActive(true);
        }).AddTo(this);

        GameManager.Instance.PlayerCoins.Subscribe(x =>
        {
            curCoinText.text = x.ToString();
        }).AddTo(this);

        GameManager.Instance.CurrentGameLevel.Subscribe(x =>
        {
            curLevelText.text = x.ToString();
        }).AddTo(this);
    }

    public void OnClickBackMainScene()
    {
        GameManager.Instance.SetGameClear(false);
        GameManager.Instance.SetGameOver(false);
        GameManager.Instance.ResetGameLevel();
    }

    public void CreateEnemyDropCoinGO(Vector3 pos, int amount)
    {
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(pos);
        var dp = Instantiate(enemyDropCoinPrefab, transform.parent);
        dp.transform.position = viewportPosition;
    }
}
