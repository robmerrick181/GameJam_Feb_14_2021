using UnityEngine;

/// <summary>
/// This script needs to be attached to all characters in the scene (bosses and players).
/// </summary>
[RequireComponent(typeof(CharacterStats), typeof(Rigidbody))]
public class Character : MonoBehaviour
{
	[SerializeField] [Range(0.0001F, 0.05F)] private float _maxMovementSpeed = 0.025F;

	private Rigidbody _rigidBody;
	private CharacterStats _characterStats;
	private Animator _animator;
	private Character _characterAttackingMe;
    private bool _isTakingDamage = false;
	private bool _isGloating = false;
	private Vector3 _xzVelocity = Vector3.zero;
	private Quaternion _savedRotation = Quaternion.identity;

	public bool IsSwingingSword { get; private set; } = false;
    public bool IsJumping { get; private set; } = false;
	public bool IsDead { get; private set; } = false;
    public float MaxMovementSpeed => _maxMovementSpeed;

	private void Start()
	{
		_rigidBody = GetComponent<Rigidbody>();
		_characterStats = GetComponent<CharacterStats>();
		_animator = GetComponentInChildren<Animator>();
		_characterStats.SetDeathCallback(Die);
	}

	private void Update()
	{
		UpdateAnimation();
		StandUpright();
		CheckIfNotSwingingSword();
		CheckIfNotTakingDamage();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(IsJumping && collision.gameObject.layer == 3)
		{
			for(int i = 0; i < collision.contactCount; i++)
			{
				if(collision.GetContact(i).normal.y >= 0.8)
				{
					_animator.SetTrigger("Land");
					IsJumping = false;
					return;
				}
			}
		}
	}

	public void MoveXZ(Vector3 translation)
	{
		if(IsDead)
        {
			translation = Vector3.zero;
        }

		translation = new Vector3(translation.x, 0, translation.z);
		translation = Vector3.ClampMagnitude(translation, _maxMovementSpeed);
		_xzVelocity = translation;

		if(translation.magnitude >= Mathf.Epsilon)
		{
            transform.LookAt(transform.position + translation);
            _rigidBody.MovePosition(_rigidBody.position + translation);
			_savedRotation = transform.rotation;
		}
	}

	public void Jump()
	{
		if(!IsDead && !IsJumping)
		{
			_animator.SetTrigger("Jump");
			_rigidBody.AddForce(6.5F * Vector3.up, ForceMode.Impulse);
			IsJumping = true;
		}
	}

	public void SwingSword()
	{
		if(!IsDead)
        {
			_animator.SetTrigger("Sword");
			IsSwingingSword = true;
        }
    }

	public void ApplyDamage(Character characterAttackingMe)
	{
		if(!_isTakingDamage)
        {
			_characterAttackingMe = characterAttackingMe;
			_characterStats.ChangeHealth(-_characterAttackingMe._characterStats.CurrentStrength);
			_isTakingDamage = true;
			characterAttackingMe.IsSwingingSword = false;
        }
    }

	public void Gloat()
    {
		if(!IsDead && !_isGloating)
        {
			_isGloating = true;
			_animator.SetTrigger("Gloat");
        }
    }

	private void UpdateAnimation()
    {
		_animator.SetFloat("speedPercent", _xzVelocity.magnitude / _maxMovementSpeed);
    }

	/// <summary>
	/// This just keeps the character standing upright. They tend to fall over when they're holding swords. Steel's heavy, you know.
	/// </summary>
	private void StandUpright()
	{
		transform.rotation = Quaternion.Euler(new Vector3(0, _savedRotation.eulerAngles.y, 0));
	}

	private void CheckIfNotSwingingSword()
	{
		if(IsSwingingSword && _animator.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
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

	private void Die()
    {
		_animator.SetTrigger("Die");
		IsDead = true;
    }
}
