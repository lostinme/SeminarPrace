using UnityEngine;
using UnityEngine.UI;

public class playerProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] public bool direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (direction)
        {
            transform.Translate(transform.right * projectileSpeed * Time.deltaTime);
        } else
        {
            transform.Translate(-transform.right * projectileSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.transform.parent.GetComponent<CsharpBoss>().health -= 10;
            Destroy(gameObject);
        }  else if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        
    }
}
