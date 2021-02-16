using UnityEngine;

/// <summary>
/// This script needs to be attached to all characters in the scene (bosses and players).
/// </summary>
[RequireComponent(typeof(CharacterStats))]
public class Character : MonoBehaviour
{
	private CharacterStats _characterStats;
	private Animator _animator;
	private Character _characterAttackingMe;
	private bool _isTakingDamage = false;

	public bool IsSwingingSword { get; private set; } = false;

	private void Start()
	{
		_characterStats = GetComponent<CharacterStats>();
		_animator = GetComponentInChildren<Animator>();
	}

	public void SwingSword()
	{
		_animator.SetTrigger("Swing1");
		IsSwingingSword = true;
	}

	public void ApplyDamage(Character characterAttackingMe)
	{
		_characterAttackingMe = characterAttackingMe;
		_characterStats.ChangeHealth(-_characterAttackingMe._characterStats.CurrentStrength);
		_isTakingDamage = true;
	}

	private void Update()
	{
		CorrectRotation();
		CheckIfNotSwingingSword();
		CheckIfNotTakingDamage();
	}

	/// <summary>
	/// This just keeps the character standing upright. They tend to fall over when they're holding swords. Steel's heavy, you know.
	/// </summary>
	private void CorrectRotation()
	{
		transform.rotation = Quaternion.Euler(0.0F, transform.rotation.eulerAngles.y, 0.0F);
	}

	private void CheckIfNotSwingingSword()
	{
		if(IsSwingingSword && _animator.GetCurrentAnimatorStateInfo(0).IsName("SwordIdle"))
		{
			IsSwingingSword = false;
		}
	}

	private void CheckIfNotTakingDamage()
	{
		if(_isTakingDamage && !_characterAttackingMe.IsSwingingSword)
		{
			_isTakingDamage = false;
		}
	}
}
