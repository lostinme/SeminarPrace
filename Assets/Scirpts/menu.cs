using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        menuShow();
    }

    void menuShow()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuUI.SetActive(!menuUI.activeSelf);

            if (menuUI.activeSelf)
            {
                Time.timeScale = 0;
            } else
            {
                Time.timeScale = 1;
            }
        }
    }

    public void Leave()
    {
        SceneManager.LoadScene(0);
    }
}
