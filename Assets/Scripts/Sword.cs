using UnityEngine;

public class Sword : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		Character character = other.gameObject.GetComponent<Character>();
		
		if(character && character != transform.parent.GetComponent<Character>())
		{
			character.ApplyDamage(transform.parent.GetComponent<Character>());
		}
	}
}
