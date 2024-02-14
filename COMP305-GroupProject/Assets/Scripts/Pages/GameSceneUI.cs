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

    PlayerController player;
    float playerMaxHp = 150f;
    // Start is called before the first frame update
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
    }

}
