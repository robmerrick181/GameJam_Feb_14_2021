using System;
using UnityEngine;

public class CharacterStats: MonoBehaviour
{
    [SerializeField] private float _startingHealthPoints = 100.0F;
    [SerializeField] private float _startingStrength = 10.0F;
    [SerializeField] private float _startingWeaponDamage = 0.0f;

    private float _currentHealthPoints;
    private float _currentStrength;
    private float _currentWeaponDamage;
    private Action _deathCallback = null;

    public float StartingHealthPoints => _startingHealthPoints;
    public float StartingStrength => _startingStrength;
    public float CurrentHealthPoints => _currentHealthPoints;
    public float CurrentHealthPercent => _currentHealthPoints / _startingHealthPoints;
    public float CurrentStrength => _currentStrength;
    public float StartingWeaponDamage => _startingWeaponDamage;
    public float CurrentWeaponDamage => _currentWeaponDamage;
    public float MaxMovementSpeed;

    private void Start()
	{
		_currentHealthPoints = _startingHealthPoints;
        _currentStrength = _startingStrength;
        _currentWeaponDamage = _startingWeaponDamage;
    }

    public void SetDeathCallback(Action callback)
    {
        _deathCallback = callback;
    }

	public void ChangeStrength(float _strength)
    {
        _currentStrength += _strength;
    }

    public void ResetStrength()
    {
        _currentStrength = _startingStrength;
    }

    public void ChangeWeaponDamage(float _damage)
    {
        _currentWeaponDamage += _damage;
    }

    public void ResetWeaponDamage()
    {
        _currentWeaponDamage = _startingWeaponDamage;
    }

    public void ChangeHealth(float _health)
    {
        _currentHealthPoints += _health;

        if (_currentHealthPoints > _startingHealthPoints)
            _currentHealthPoints = _startingHealthPoints;

        if(_currentHealthPoints <= 0)
		{
            _deathCallback.Invoke();
		}
    }

    public void ResetHealth()
    {
        _currentHealthPoints = _startingHealthPoints;
    }
}
