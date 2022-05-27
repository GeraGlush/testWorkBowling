using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    public GameObject failPanel;
    public GameObject winPanel;

    public Text pinText;
    public Text levelText;

    private void Start()
    {
        StartCoroutine(LevelHide());
    }

    public void Win()
    {
        pinText.text = FindObjectOfType<PinManager>().pinDestroy + "/" + FindObjectOfType<PinManager>().allPinCount + " pins knocked down";
        winPanel.SetActive(true);
    }

    public void Fail()
    {
        failPanel.SetActive(true);
        FindObjectOfType<PlayerMovement>().canMove = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LevelHide()
    {
        levelText.gameObject.SetActive(true);
        levelText.text = "#" + (SceneManager.GetActiveScene().buildIndex + 1);
        yield return new WaitForSeconds(1f);
        levelText.gameObject.SetActive(false);
    }
}
