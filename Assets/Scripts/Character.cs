using UnityEngine;

/// <summary>
/// This script needs to be attached to all characters in the scene (bosses and players).
/// </summary>
[RequireComponent(typeof(CharacterStats), typeof(Rigidbody))]
public class Character : MonoBehaviour
{
	[SerializeField] [Range(0.0001F, 0.05F)] private float _maxMovementSpeed = 0.02F;

	private Rigidbody _rigidBody;
	private CharacterStats _characterStats;
	private Animator _animator;
	private Character _characterAttackingMe;
	private bool _isJumping = false;
	private bool _isTakingDamage = false;

	public bool IsSwingingSword { get; private set; } = false;
	public bool IsJumping => _isJumping;
	public float MaxMovementSpeed => _maxMovementSpeed;

	private void Start()
	{
		_rigidBody = GetComponent<Rigidbody>();
		_characterStats = GetComponent<CharacterStats>();
		_animator = GetComponentInChildren<Animator>();
	}

	private void Update()
	{
		StandUpright();
		CheckIfNotSwingingSword();
		CheckIfNotTakingDamage();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(_isJumping && collision.gameObject.layer == 3)
		{
			for(int i = 0; i < collision.contactCount; i++)
			{
				if(collision.GetContact(i).normal.y >= 0.8)
				{
					_isJumping = false;
					return;
				}
			}
		}
	}

	public void Move(Vector3 translation)
	{
		translation = Vector3.ClampMagnitude(translation, _maxMovementSpeed);
		transform.LookAt(transform.position + translation);
		_rigidBody.MovePosition(_rigidBody.position + translation);
	}

	public void Jump()
	{
		if(!_isJumping)
		{
			_rigidBody.AddForce(6.5F * Vector3.up, ForceMode.Impulse);
			_isJumping = true;
		}
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

	/// <summary>
	/// This just keeps the character standing upright. They tend to fall over when they're holding swords. Steel's heavy, you know.
	/// </summary>
	private void StandUpright()
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
