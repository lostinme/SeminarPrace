using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CsharpBoss : MonoBehaviour
{
    [Header("Boss fight properties")]
    public float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float actionCount;
    bool currentActionEnd = true;
    float randomAction;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject[] spawnPositions;
    [SerializeField] private float timeBetweenActions;

    [Header("UI")]
    [SerializeField] private float hodnotaDoUI;
    [SerializeField] private string actionName;
    [SerializeField] private string loading;
    [SerializeField] private Image bossHpUI;
    [SerializeField] private TextMeshProUGUI screenText;
    float halfSecond;

    bool isDead;

    [Header("References")]
    [SerializeField] GameObject player;
    [SerializeField] private Color playerSpriteAValue;

    float textureAValue;

    //Time properties
    float currentTime;
    float timeToCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerSpriteAValue = player.GetComponent<SpriteRenderer>().color;

        timeToCount = Time.time;

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            TimeCalculate();
        }

        bossHpUI.fillAmount = health / maxHealth;

        OnDead();
    }

    void TimeCalculate()
    {
        if (currentActionEnd)
        {
            currentTime += Time.time - timeToCount;
        } else
        {
            currentTime = 0;
            halfSecond = 0;
            loading = "";

            currentActionEnd = true;
        }

        if (currentTime >= timeBetweenActions && currentActionEnd)
        {
            PickAction();

            currentActionEnd = false;
        }

        UIHalfSecondCapture();

        timeToCount = Time.time;
    }

    void UIHalfSecondCapture()
    {
        if (currentTime >= halfSecond + 0.5f)
        {
            loading += "|";
            halfSecond = currentTime;
        }

        if (hodnotaDoUI == 0)
        {
            screenText.text = $"Loading: {loading}";
        } else
        {
            screenText.text = $"player.{actionName} = {hodnotaDoUI};    Loading {loading}";
        }
    }

    void PickAction()
    {
        ResetAfterAction();

        randomAction = Random.Range(0, actionCount);

        if (randomAction < 1)
        {
            TurnOffTexture();
        } else if (randomAction < 2)
        {
            SetPlayerHP();
        } else if (randomAction < 3)
        {
            SetPlayerSpeed();
        } else if (randomAction < 4)
        {
            SetPlayerJump();
        }

        ShootProjectiles();

        timeBetweenActions -= timeBetweenActions * 0.05f;
    }

    void TurnOffTexture()
    {
        playerSpriteAValue.a = Random.Range(0.1f, textureAValue);

        player.GetComponent<SpriteRenderer>().color = playerSpriteAValue;

        hodnotaDoUI = playerSpriteAValue.a;
        actionName = "setTexture.a";
    }

    void SetPlayerHP()
    {
        playerFight playerFight = player.GetComponent<playerFight>();

        float hp = Random.Range(30,playerFight.p_defaultPlayerHp);

        playerFight.p_playerHp = hp;

        hodnotaDoUI = hp;
        actionName = "healthPoints";
    }

    void SetPlayerSpeed()
    {
        player.GetComponent<playerMovement>().moveSpeed = Random.Range(3, 5);

        hodnotaDoUI = player.GetComponent<playerMovement>().moveSpeed;
        actionName = "speed";
    }

    void SetPlayerJump()
    {
        player.GetComponent<playerMovement>().jumpForce = Random.Range(14, 20);

        hodnotaDoUI = player.GetComponent<playerMovement>().jumpForce;
        actionName = "jump";
    }

    void ResetAfterAction()
    {
        player.GetComponent<playerMovement>().jumpForce = 20;
        player.GetComponent<playerMovement>().moveSpeed = 5;

        playerSpriteAValue.a = 1;

        player.GetComponent<SpriteRenderer>().color = playerSpriteAValue;
    }

    void ShootProjectiles()
    {
        float randomProjectiles = Random.Range(4, 12);

        for (int i = 0; i < randomProjectiles; i++)
        {
            int randomPos = Random.Range(0, spawnPositions.Length);
            float randomUpPos = Random.Range(-0.5f, 0.5f);

            if (randomPos == 0)
            {
                GameObject currentProjectile = Instantiate(projectile, spawnPositions[randomPos].transform.position, transform.rotation);
                currentProjectile.GetComponent<bossProjectile>().leftDirection = false;
                currentProjectile.GetComponent<bossProjectile>().upDirecition = randomUpPos;
            } else
            {
                GameObject currentProjectile = Instantiate(projectile, spawnPositions[randomPos].transform.position, transform.rotation);
                currentProjectile.GetComponent<bossProjectile>().upDirecition = randomUpPos;
                currentProjectile.GetComponent<bossProjectile>().leftDirection = true;
            }
        }
    }

    void OnDead()
    {
        if (health <= 0)
        {
            player.GetComponent<playerFight>().isWin = true;
            isDead = true;
        }
    }
}
