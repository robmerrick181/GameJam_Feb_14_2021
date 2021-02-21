using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIActionDecision : StateMachineBehaviour
{
	private BossAIHelper _helper;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_helper = new BossAIHelper(animator);

		if(_helper.BossCharacter.IsDead)
		{
			return;
		}

		_helper.BossCharacter.transform.LookAt(_helper.PlayerCharacter.transform.position);

		if(_helper.BossCharacter.CharacterStats.CurrentHealthPercent <= 0.5F && Random.value < 0.1F)
		{
			animator.SetTrigger("VulnerableLogic");
		}
		else if(Vector3.Distance(_helper.BossCharacter.transform.position, _helper.PlayerCharacter.transform.position) > 5.0F)
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
