using UnityEngine;

/// <summary>
/// This script needs to be attached to all characters in the scene (bosses and players).
/// </summary>
[RequireComponent(typeof(CharacterStats), typeof(Rigidbody))]
public class Character : MonoBehaviour
{
	private Rigidbody _rigidBody;
	private CharacterStats _characterStats;
	private Animator _animator;
	private Character _characterAttackingMe;
    private bool _isTakingDamage = false;
	private bool _isGloating = false;
	private Vector3 _xzVelocity = Vector3.zero;
	private Quaternion _savedRotation = Quaternion.identity;

	public float damageCooldown = 0f;
	public bool IsSwingingSword { get; private set; } = false;
    public bool IsJumping { get; private set; } = false;
	public bool IsDead { get; private set; } = false;
	private float MaxMovementSpeed; 
	private int enemyLayer;

	private void Start()
	{
		_rigidBody = GetComponent<Rigidbody>();
		_characterStats = GetComponent<CharacterStats>();
		_animator = GetComponentInChildren<Animator>();
		_characterStats.SetDeathCallback(Die);
		enemyLayer = 6;
		MaxMovementSpeed = _characterStats.MaxMovementSpeed;

		if(transform.name == "Boss")
        {
			enemyLayer = 7;
		}
	}

	private void Update()
	{
		UpdateAnimation();
		StandUpright();
		SetIsSwingingSword();
		CheckIfNotTakingDamage();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(IsJumping && collision.gameObject.layer == 3)
		{
			for (int i = 0; i < collision.contactCount; i++)
			{
				if (collision.GetContact(i).normal.y >= 0.8)
				{
					_animator.SetTrigger("Land");
					IsJumping = false;
					return;
				}
			}
		}
		else if (collision.gameObject.layer == 3)
			return;
		else if (collision.gameObject.layer == enemyLayer && collision.collider.name == "Blade")
			TakeDamage(collision.gameObject.GetComponent<Character>());
	}

	public void MoveXZ(Vector3 translation, bool targetSystemEngaged, Character targetCharacter)
	{
		if(IsDead)
        {
			translation = Vector3.zero;
        }

		translation = new Vector3(translation.x, 0, translation.z);
		translation = Vector3.ClampMagnitude(translation, MaxMovementSpeed);
		_xzVelocity = translation;

		if(translation.magnitude >= Mathf.Epsilon)
		{
			_rigidBody.MovePosition(_rigidBody.position + 110.0F * translation * Time.deltaTime);

			if (!targetSystemEngaged)
			{
				transform.LookAt(transform.position + translation);
			}
			else
			{
				transform.LookAt(targetCharacter.transform.position);
			}

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

	public void TakeDamage(Character characterAttackingMe)
	{
		if(!_isTakingDamage && characterAttackingMe.IsSwingingSword)
        {
			_characterAttackingMe = characterAttackingMe;
			_characterStats.ChangeHealth(-_characterAttackingMe._characterStats.CurrentStrength);
			_isTakingDamage = true;
			damageCooldown = 0.25f;
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
		float velocityProjectedOntoForward = Vector3.Dot(_xzVelocity, transform.forward);
		float velocityProjectedOntoRight = Vector3.Dot(_xzVelocity, transform.right);
		_animator.SetFloat("SpeedPercentZ", velocityProjectedOntoForward / MaxMovementSpeed); 
		_animator.SetFloat("SpeedPercentX", velocityProjectedOntoRight / MaxMovementSpeed);
	}

	/// <summary>
	/// This just keeps the character standing upright. They tend to fall over when they're holding swords. Steel's heavy, you know.
	/// </summary>
	private void StandUpright()
	{
		transform.rotation = Quaternion.Euler(new Vector3(0, _savedRotation.eulerAngles.y, 0));
	}

	private void SetIsSwingingSword()
	{
		IsSwingingSword = _animator.GetBool("IsSwingingSword");
	}

	private void CheckIfNotTakingDamage()
	{
		if(_isTakingDamage && damageCooldown <= 0)
		{
			_isTakingDamage = false;
		}
		else
        {
			damageCooldown -= Time.deltaTime;
        }
	}

	private void Die()
    {
		_animator.SetTrigger("Die");
		IsDead = true;
		IsSwingingSword = false;
    }

	public void BlockAttack(bool blocking)
    {
		
		if (blocking)
        {
			_animator.SetTrigger("StartBlock");
        }
        else
        {
			_animator.SetTrigger("EndBlock");
		}
    }
}
