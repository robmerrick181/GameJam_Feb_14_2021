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

		if(_bossCharacter.IsDead)
		{
			 
		}
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}
}
