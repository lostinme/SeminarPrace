using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerFight : MonoBehaviour
{
    [Header("Fight basics")]
    public float p_defaultPlayerHp;
    public float p_playerHp;
    [SerializeField] private GameObject projectileToShoot;
    [SerializeField] private Image playerUIHP;
    
    [Header("Input")]
    [SerializeField] private KeyCode FightButton;

    [Header("End properties")]
    public bool isWin;
    bool isLoose;
    [SerializeField] private GameObject endTable;
    [SerializeField] private TextMeshProUGUI endText;

    [SerializeField] private playerMovement player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        p_playerHp = p_defaultPlayerHp;
        endTable.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(FightButton) && !isLoose)
        {
            ShootProjectile();
        }

        playerUIHP.fillAmount = p_playerHp / p_defaultPlayerHp;

        End();

        if (p_playerHp <= 0)
        {
            isLoose = true;
        }
    }

    void ShootProjectile()
    {
        GameObject currentProjectile = Instantiate(projectileToShoot, gameObject.transform.position, gameObject.transform.rotation);

        currentProjectile.GetComponent<playerProjectile>().direction = player.direction;
    }

    void End()
    {
        if (isWin)
        {
            endText.text = "You WON!!! Press 'G' to go to ending sceen!";

            endTable.SetActive(true);
        } else if (isLoose)
        {
            endText.text = "You LOST!!!! Press 'G' to go to the Hall";

            player.jumpForce = 0;
            player.moveSpeed = 0;

            endTable.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (isWin)
            {
                SceneManager.LoadScene(3);
            } else if (isLoose)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
