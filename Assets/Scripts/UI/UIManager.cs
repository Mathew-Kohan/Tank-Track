using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is NULL!");
            }

            return _instance;
        }
    }

    public Text coinCountText;
    public Text bulletCountText;

    [SerializeField]
    private GameObject _gameOverMenuUI;

    [SerializeField]
    private GameObject _winMenuUI;

    [SerializeField]
    private GameObject _cmvCam;

    private void Awake()
    {
        _instance = this;
    }

    public void BulletCount(int bulletCount)
    {
        bulletCountText.text = "" + bulletCount;
    }

    public void CoinCount(int coinCount)
    {
        coinCountText.text = "" + coinCount;
    }

    public void GameOverMenu()
    {
        StartCoroutine(WaitForMenu(_gameOverMenuUI, 0.5f));
    }

    public void WinMenu()
    {
        StartCoroutine(WaitForMenu(_winMenuUI, 1f));
    }

    private IEnumerator WaitForMenu(GameObject menu, float waitTime)
    {
        _cmvCam.SetActive(false);

        yield return new WaitForSeconds(waitTime);

        menu.SetActive(true);
        Time.timeScale = 0f;
    }

}
