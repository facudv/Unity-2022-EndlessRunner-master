using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class Menu : MonoBehaviour
{

    public List<GameObject> menu;
    public List<GameObject> cheatConsole;
    public List<GameObject> defeatMenu;
    bool _closed;
    public List<GameObject> menuPrincipal;
    public List<GameObject> credits;
    public Add add;

    public void OpenMenuAndClose()
    {
        if (!_closed)
        {
            foreach (var item in menu)
            {
                item.SetActive(true);
            }
            FindObjectOfType<PlatformManager>().menu = true; // esta linea hace que cuando estes en menu, se dejen de crear tiles.
            Time.timeScale = 0f;
            _closed = !_closed;
        }
        else
        {
            foreach (var item in menu)
            {
                item.SetActive(false);
            }
            Time.timeScale = 1f;
            FindObjectOfType<PlatformManager>().menu = false;
            _closed = !_closed;
            foreach (var item in cheatConsole)
            {
                item.SetActive(false);
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenConsole()
    {
        foreach (var item in cheatConsole)
        {
            item.SetActive(true);
        }
        foreach (var item in menu)
        {
            item.SetActive(false);
        }
    }

    public void CloseConsole()
    {
        foreach (var item in menu)
        {
            item.SetActive(true);
        }
        foreach (var item in cheatConsole)
        {
            item.SetActive(false);
        }
    }

    public void DefeatMenu()
    {
        Time.timeScale = 0f;
        FindObjectOfType<PlatformManager>().menu = true;
        foreach (var item in defeatMenu)
        {
            item.SetActive(true);
        }
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void Add()
    {
        add.activeAdd = true;
        //add.SetActive(true);
    }

    public void Restart()
    {
        //OpenMenuAndClose();
        SceneManager.LoadScene("LevelOne");
        Time.timeScale = 1f;
    }

    bool _activeCredits = true;

    public void Creditos()
    {
        if (_activeCredits)
        {
            foreach (var item in menuPrincipal)
            {
                item.SetActive(false);
            }
            foreach (var item in credits)
            {
                item.SetActive(true);
            }
            _activeCredits = !_activeCredits;
        }
        else
        {
            foreach (var item in menuPrincipal)
            {
                item.SetActive(true);
            }
            foreach (var item in credits)
            {
                item.SetActive(false);
            }
            _activeCredits = !_activeCredits;
        }

    }

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelOne");
        Time.timeScale = 1f;
    }

    public void StartCoroutineDie()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1f);
        DefeatMenu();
    }
}
