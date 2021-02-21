using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum GameStatus { Playing, Win, Lose}

public class GameManager : MonoBehaviour
{
    private GameStatus _gameStatus = GameStatus.Playing;

    [SerializeField] private CharacterStats Player;
    [SerializeField] private CharacterStats Boss;


    [SerializeField] private Canvas _endGameUI;

    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _loseUI;
    [SerializeField] private GameObject _PausedUI;

    [SerializeField] private Text _playerHealth;
    [SerializeField] private Text _playerStrength;
    [SerializeField] private Text _playerWeaponDamage;

    [SerializeField] private Button _continue;

    private void Start()
    {
        StartBattle();
    }

    private void Update()
    {
        if (_gameStatus == GameStatus.Playing)
            CheckStatus();

        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    private void StartBattle()
    {
        _gameStatus = GameStatus.Playing;

        if (_winUI.gameObject.activeSelf)
            _winUI.gameObject.SetActive(false);
        if (_loseUI.gameObject.activeSelf)
            _loseUI.gameObject.SetActive(false);
        if (_endGameUI.gameObject.activeSelf)
            _endGameUI.gameObject.SetActive(false);

        Time.timeScale = 1f;

        Player.ResetHealth();
        Player.ResetStrength();
        Player.ResetWeaponDamage();

        Boss.ResetHealth();
        Boss.ResetStrength();
        Boss.ResetWeaponDamage();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void CheckStatus()
    {
        if (Player.CurrentHealthPoints < 0)
            EndGame(GameStatus.Lose);
        if (Boss.CurrentHealthPoints < 0)
            EndGame(GameStatus.Win);
    }

    private void EndGame(GameStatus _status)
    {
        _endGameUI.gameObject.SetActive(true);

        _gameStatus = _status;
        Time.timeScale = 0.0f;
        if (_status == GameStatus.Win)
            _winUI.gameObject.SetActive(true);
        if (_status == GameStatus.Lose)
            _loseUI.gameObject.SetActive(true);

        SetPlayerData();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void SetPlayerData()
    {
        _playerHealth.text = Player.CurrentHealthPoints.ToString();
        _playerStrength.text = Player.CurrentStrength.ToString();
        _playerWeaponDamage.text = Player.CurrentWeaponDamage.ToString();
    }

    public void ResetLevel()
    {
        _gameStatus = GameStatus.Playing;

        if (_winUI.gameObject.activeSelf)
            _winUI.gameObject.SetActive(false);
        if (_loseUI.gameObject.activeSelf)
            _loseUI.gameObject.SetActive(false);

        Time.timeScale = 1f;

        Player.ResetHealth();
        Player.ResetStrength();
        Player.ResetWeaponDamage();

        Boss.ResetHealth();
        Boss.ResetStrength();
        Boss.ResetWeaponDamage();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        if (_endGameUI.gameObject.activeSelf && (_winUI.activeSelf || _loseUI.activeSelf))
            return;

        if (_endGameUI.gameObject.activeSelf == false)
        {
            _endGameUI.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
            SetPlayerData();
            _winUI.gameObject.SetActive(false);
            _loseUI.gameObject.SetActive(false);
            _PausedUI.SetActive(true);
            _continue.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            _continue.gameObject.SetActive(false);
            _endGameUI.gameObject.SetActive(false);
            _PausedUI.SetActive(false);
            Time.timeScale = 1.0f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
