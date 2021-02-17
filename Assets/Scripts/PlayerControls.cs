using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField] [Range(0.0001F, 0.05F)] private float _strafingSensitivity = 0.025F;

    private Character _character;

	private void Start()
	{
        _character = GetComponent<Character>();
	}

	private void Update()
    {
		UpdateJump();
        UpdateSword();
        UpdateMovement();
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
        finalTranslation *= _strafingSensitivity;
        _character.MoveXZ(finalTranslation);
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
}
