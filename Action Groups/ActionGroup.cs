
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bolt;

namespace Bolt {
	public class ActionGroup
	{
		public Timeline timeline;

		public ActionGroup ()
		{

		}

		protected Coroutine StartCoroutine( IEnumerator routine )
		{
			return timeline.StartCoroutine( routine );
		}

		public virtual IEnumerator Routine()
		{
			yield return null;
		}
	}
}
