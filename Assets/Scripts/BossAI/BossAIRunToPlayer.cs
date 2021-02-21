using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIRunToPlayer : StateMachineBehaviour
{
	//private BossAIHelper _helper;

	//public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//	_helper = new BossAIHelper(animator);
	//}

	//public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//	_helper.BossCharacter.MoveXZ(_helper.PlayerCharacter.transform.position - _helper.BossCharacter.transform.position, false, null);

	//	if(Vector3.Distance(_helper.PlayerCharacter.transform.position, _helper.BossCharacter.transform.position) < 5.0F)
	//	{
	//		animator.SetTrigger("ChaseLogic");
	//	}
	//}

	//public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{

	//}
}
