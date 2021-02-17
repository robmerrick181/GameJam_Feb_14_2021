using UnityEngine;

public class Sword : MonoBehaviour
{
	private void OnTriggerEnter(Collider collided)
	{
		Character character = collided.gameObject.GetComponent<Character>();
		
		if(character && character != transform.root.GetComponent<Character>())
		{
			character.ApplyDamage(transform.root.GetComponent<Character>());
		}
	}
}
