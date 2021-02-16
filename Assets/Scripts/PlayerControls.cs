using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField] [Range(0.0001F, 0.05F)] private float _strafingSensitivity = 0.02F;

    private Character _character;
    private Rigidbody _rigidBody;

	private void Start()
	{
        _character = GetComponent<Character>();
		_rigidBody = GetComponent<Rigidbody>();
	}

	private void Update()
    {
        UpdateStrafing();
		UpdateJump();
        UpdateSword();
    }

	private void UpdateStrafing()
	{
        Vector3 translation = Vector3.zero;
        Vector3 cameraForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        Vector3 cameraBackward = -cameraForward;
        Vector3 cameraRight = Camera.main.transform.right;
        Vector3 cameraLeft = -cameraRight;

        if(Input.GetKey(KeyCode.W))
        {
            translation += cameraForward;
        }

        if(Input.GetKey(KeyCode.A))
        {
            translation += cameraLeft;
        }

        if(Input.GetKey(KeyCode.S))
        {
            translation += cameraBackward;
        }

        if(Input.GetKey(KeyCode.D))
        {
            translation += cameraRight;
        }

        _rigidBody.MovePosition(_rigidBody.position + translation.normalized * _strafingSensitivity);
    }

    private void UpdateJump()
	{
        if(Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(_rigidBody.velocity.y) < Mathf.Epsilon)
		{
            _rigidBody.AddForce(6.5F * Vector3.up, ForceMode.Impulse);
		}
	}

    private void UpdateSword()
	{
        if(Input.GetMouseButtonDown(0))
		{
            _character.SwingSword();
		}
	}
}
