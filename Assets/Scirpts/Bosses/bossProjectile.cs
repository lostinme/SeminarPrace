using UnityEngine;

public class bossProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] public bool leftDirection;
    [SerializeField] public float upDirecition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leftDirection)
        {
            transform.Translate(-transform.right * speed * Time.deltaTime);
        } else
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }

        transform.Translate(transform.up * upDirecition * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "player")
        {
            collision.collider.GetComponent<playerFight>().p_playerHp -= 10;
            Destroy(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
}
