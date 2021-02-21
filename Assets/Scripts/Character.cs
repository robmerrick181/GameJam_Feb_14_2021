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
	private GameObject[] _phantoms = new GameObject[5];
    private bool _isTakingDamage = false;
	private bool _isGloating = false;
	private bool _isBoss = false;
	private Vector3 _xzVelocity = Vector3.zero;
	private Quaternion _savedRotation = Quaternion.identity;

	public float damageCooldown = 0f;
	public bool IsSwingingSword { get; private set; } = false;
    public bool IsJumping { get; private set; } = false;
	public bool IsDead { get; private set; } = false;
	public Character Player { get; private set; }
	public CharacterStats CharacterStats => _characterStats;
	private float MaxMovementSpeed; 
	private int enemyLayer;
	private int hitCount = 0;

	private void Start()
	{
		_rigidBody = GetComponent<Rigidbody>();
		_characterStats = GetComponent<CharacterStats>();
		_animator = GetComponentInChildren<Animator>();
		_characterStats.SetDeathCallback(Die);
		_isBoss = transform.name == "Boss";
		enemyLayer = _isBoss ? 7 : 6;
		MaxMovementSpeed = _characterStats.MaxMovementSpeed;
		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
		GameObject phantomsContainer = GameObject.Find("Phantoms");

		for(int i = 0; i < 5; i++)
        {
			_phantoms[i] = phantomsContainer.transform.GetChild(i).gameObject;
			_phantoms[i].SetActive(false);
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
		else if (collision.gameObject.layer == enemyLayer && collision.collider.CompareTag("Sword"));
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

		if(translation.magnitude >= 0.01F)
		{
			_rigidBody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationZ;
			_rigidBody.MovePosition(_rigidBody.position + 110.0F * translation * Time.deltaTime);

			if(!targetSystemEngaged)
			{
				transform.LookAt(transform.position + translation);
			}
			else
			{
				transform.LookAt(targetCharacter.transform.position);
			}

			_savedRotation = transform.rotation;
		}
		else
		{
			_rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationZ;
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
        }
    }

	public void TakeDamage(Character characterAttackingMe)
	{
		if (!_isTakingDamage && characterAttackingMe.IsSwingingSword)
		{
			_characterAttackingMe = characterAttackingMe;
			_characterStats.ChangeHealth(-_characterAttackingMe._characterStats.CurrentStrength);
			_isTakingDamage = true;
			damageCooldown = 1.0f;


			if (characterAttackingMe.name == "Player")
			{
				hitCount++;
				if (hitCount < 5)
				{
					SpawnPhantom();
				}
			}
			if (gameObject.name == "player") 
			{
				Player.hitCount = 0;
				//DespawnPhantoms();
			}
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
		SkinnedMeshRenderer modelRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
		if (_isTakingDamage && damageCooldown <= 0)
		{
			_isTakingDamage = false;
		}
		if (damageCooldown > 0)
        {
			damageCooldown -= Time.deltaTime;
			modelRenderer.material.EnableKeyword("_EMISSION");
			modelRenderer.material.SetColor("_EmissionColor", Color.gray);
			Debug.Log("Cooldown");
			//Color color = modelRenderer.material.color;
			//color.a = 0.0f;
			//modelRenderer.material.SetColor("_Albedo", color);
		}
		if (damageCooldown < 0)
        {
			modelRenderer.material.EnableKeyword("_EMISSION");
			modelRenderer.material.SetColor("_EmissionColor", Color.black);
			//Color color = modelRenderer.material.color;
			//color.a = 1.0f;
			//modelRenderer.material.SetColor("_Albedo", color);

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
	private void SpawnPhantom()
    {
		_phantoms[hitCount].SetActive(true);
	}
	private void DespawnPhantoms()
    {
		for(int i = 0; i < 5; i++)
        {
			_phantoms[i].SetActive(false);
        }
    }
}
