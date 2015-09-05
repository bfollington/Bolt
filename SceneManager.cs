using UnityEngine;
using System.Collections;
using Bolt;

public class SceneManager : MonoBehaviour
{

	private static List<Timeline> timelines = new List<Timeline>();

	// Use this for initialization
	void Start ()
	{
		world = new World();
		world.Begin();

		world.Add( new Sylvi(0, 0) );




		SetTimeline (new TestTimeline ());
	}

	public SceneManager () : base ()
	{

	}

	public void SetTimeline (CutsceneTimeline timeline)
	{
		if (this.timeline != null) {
			this.timeline.parent = null;
		}

		this.timeline = timeline;
		this.timeline.parent = this;
	}

	// Update is called once per frame
	void Update ()
	{
		world.Update();

		if (timeline != null) {
			timeline.Update ();
		}
	}
}
