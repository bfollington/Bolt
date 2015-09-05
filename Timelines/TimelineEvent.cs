
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Bolt;

public class TimelineEvent
{

	private string eventToTrigger;
	private ActionGroup groupToRun;
	private Func<IEnumerator> methodToCall;
	private Action action;
	public Timeline timeline;

	public TimelineEvent (string eventToTrigger)
	{
		this.eventToTrigger = eventToTrigger;
	}

	public TimelineEvent(ActionGroup groupToRun)
	{
		this.groupToRun = groupToRun;
	}

	public TimelineEvent(Action action) {
		this.action = action;
	}

	public Coroutine StartCoroutine(IEnumerator routine)
	{
		return timeline.StartCoroutine( routine );
	}

	public void Trigger()
	{
		if (groupToRun != null) {
			timeline.Run( groupToRun );
		} else if (eventToTrigger != null) {
			timeline.TriggerEvent( eventToTrigger );
		} else if (action != null) {
			action();
		}

	}

}


