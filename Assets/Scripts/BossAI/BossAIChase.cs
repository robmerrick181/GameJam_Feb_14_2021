using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIChase : StateMachineBehaviour
{
	private Character _player;
	private Character _boss;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
		_boss = animator.GetComponentInParent<Character>();
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_boss.MoveXZ(_player.transform.position - _boss.transform.position, false, null);
		_boss.transform.LookAt(_player.transform.position);

		if(Vector3.Distance(_player.transform.position, _boss.transform.position) < 10.0F)
		{
			animator.SetTrigger("Attack");
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}
}
