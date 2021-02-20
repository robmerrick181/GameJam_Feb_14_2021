using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Character _targetCharacter;

    private Character _character;
    private bool targetSystemEngaged = false;

    private void Start()
	{
        _character = GetComponent<Character>();
    }

	private void Update()
    {
		UpdateJump();
        UpdateSword();
        UpdateMovement();
        UpdateTarget();
    }

    private void OnGUI()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UpdateMovement()
	{
        Vector3 leftRightTranslation = Camera.main.transform.right * Input.GetAxis("Horizontal");
        Vector3 forwardBackwardTranslation = Camera.main.transform.forward * Input.GetAxis("Vertical");
        Vector3 finalTranslation = leftRightTranslation + forwardBackwardTranslation;
        _character.MoveXZ(finalTranslation, targetSystemEngaged, _targetCharacter);
    }
    
    private void UpdateJump()
	{
        if(Input.GetButtonDown("Jump"))
		{
            _character.Jump();
		}
	}

    private void UpdateSword()
	{
        if(Input.GetButtonDown("Fire1"))
		{
            _character.SwingSword();
		}
    }

    private void UpdateTarget()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            targetSystemEngaged = !targetSystemEngaged;
        }
    }
}
