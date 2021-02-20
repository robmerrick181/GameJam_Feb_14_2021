using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*TODO:
 * Change the Keycode to use input instead so it's configurable 
 */

public class InventoryManager : MonoBehaviour
{
    private CharacterStats _player;

    private int _healthPotions = 0;
    private int _strengthPotions = 0;

    [SerializeField] private KeyCode UseHealthKey = KeyCode.Alpha1;
    [SerializeField] private KeyCode UsePotionKey = KeyCode.Alpha2;

    [SerializeField] private Text HealthText;
    [SerializeField] private Text StrengthText;

    private void Start()
    {
        if (!_player)
        {
            GameObject _playerObj = GameObject.FindGameObjectWithTag("Player");
            _player = _playerObj.GetComponent<CharacterStats>();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(UseHealthKey))
        {
            UseHealthPotion(10f);
            HealthText.text = _healthPotions.ToString();
        }
        if(Input.GetKeyDown(UsePotionKey))
        {
            UseStrengthPotion(10f);
            StrengthText.text = _strengthPotions.ToString();
        }
    }

    private void UseHealthPotion(float _addHealth)
    {
        if(_healthPotions > 0)
        {
            _healthPotions--;
            _player.ChangeHealth(_addHealth);
        }
    }

    private void UseStrengthPotion(float _addStrength)
    {
        if(_strengthPotions > 0)
        {
            _strengthPotions--;
            _player.ChangeStrength(_addStrength);
        }
    }

    public void AddHealthPotion()
    {
        _healthPotions++;
    }

    public void AddStrengthPotion()
    {
        _strengthPotions++;
    }
}
