using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] AttributeBar playerHealthBar;
    [SerializeField] TMP_Text playerHealthText;

    [Header("Coin")]
    [SerializeField] TMP_Text curCoinText;
    [SerializeField] GameObject enemyDropCoinPrefab;

    [Header("Level")]
    [SerializeField] TMP_Text curLevelText;

    [Header("Other UI")]
    [SerializeField] GameObject gameOverPage;
    [SerializeField] GameObject gameClearPage;

    PlayerController player;
    float playerMaxHp = 150f;
    Canvas canvas;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        canvas = GetComponentInParent<Canvas>();

        playerMaxHp = player.GetTankStat().health;
        playerHealthBar.Setup(playerMaxHp);
        player.SubscribeCurrentHealth().Subscribe(hp =>
        {
            playerHealthBar.Setup(playerMaxHp);
            playerHealthBar.SetValue(hp);

            playerHealthText.text = hp.ToString() + " / " + playerMaxHp.ToString();
        }).AddTo(this);

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
