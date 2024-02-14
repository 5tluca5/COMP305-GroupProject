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

    [Header("Level")]
    [SerializeField] TMP_Text curLevelText;

    [Header("Other UI")]
    [SerializeField] GameObject gameOverPage;
    [SerializeField] GameObject gameClearPage;

    PlayerController player;
    float playerMaxHp = 150f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

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

        GameManager.Instance.CurrentGameLevel.Subscribe(x =>
        {
            curLevelText.text = x.ToString();
        }).AddTo(this);
    }

}
