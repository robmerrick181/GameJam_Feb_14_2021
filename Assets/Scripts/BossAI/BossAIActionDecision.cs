using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIActionDecision : StateMachineBehaviour
{
	private Character _bossCharacter;
	private Character _playerCharacter;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_bossCharacter = animator.GetComponentInParent<Character>();

		if(Random.value < 0.1F) //And the health is less than 50%
		{
			animator.SetTrigger("VulnerableLogic");
		}
		else if(Vector3.Distance(_bossCharacter.transform.position, _playerCharacter.transform.position) > 5.0F)
		{
			animator.SetTrigger("ChaseLogic");
		}
		else if(Random.value < 0.4F)
		{
			animator.SetTrigger("AttackLogic");
		}
		else
		{
			animator.SetTrigger("IdleLogic");
		}
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}
}
