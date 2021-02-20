using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*TODO:
 * Change the Keycode to use input instead so it's configurable 
 */

public class InventoryManager : MonoBehaviour
{
    static public CharacterStats Player;

    static private int _healthPotions = 0;
    static private int _strengthPotions = 0;

    [SerializeField] private KeyCode UseHealthKey = KeyCode.Alpha1;
    [SerializeField] private KeyCode UsePotionKey = KeyCode.Alpha2;

    [SerializeField] private Text HealthText;
    [SerializeField] private Text StrengthText;

    private void Start()
    {
        if (!Player)
        {
            GameObject _playerObj = GameObject.FindGameObjectWithTag("Player");
            Player = _playerObj.GetComponent<CharacterStats>();
        }
        HealthText.text = _healthPotions.ToString();
        StrengthText.text = _strengthPotions.ToString();
    }

    private void Update()
    {
        HealthText.text = _healthPotions.ToString();
        StrengthText.text = _strengthPotions.ToString();
        if(Input.GetKeyDown(UseHealthKey))
        {
            UseHealthPotion(10f);
        }
        if(Input.GetKeyDown(UsePotionKey))
        {
            UseStrengthPotion(10f);
        }
    }

    private void UseHealthPotion(float _addHealth)
    {
        if(_healthPotions > 0)
        {
            _healthPotions--;
            Player.ChangeHealth(_addHealth);
        }
    }

    private void UseStrengthPotion(float _addStrength)
    {
        if(_strengthPotions > 0)
        {
            _strengthPotions--;
            Player.ChangeStrength(_addStrength);
        }
    }

    static public void AddHealthPotion()
    {
        _healthPotions++;
    }

    static public void AddStrengthPotion()
    {
        _strengthPotions++;
    }
}
