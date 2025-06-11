using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject fkey;

    private void Start()
    {
        fkey.SetActive(false);
    }

    private void Update()
    {
        if (fkey.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fkey.SetActive(true);

            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        fkey.SetActive(false);
    }
}
