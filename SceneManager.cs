using UnityEngine;
using System.Collections.Generic;
using Bolt;

public class SceneManager : MonoBehaviour
{

	private static List<Timeline> timelines = new List<Timeline>();
	private static World world;


	public SceneManager () : base ()
	{
		Collide.ClearTypeCache();
	}

	public static Camera Cam() {
		return Camera.main;
	}

	public virtual void Begin() {
		
	}

	// Use this for initialization
	void Start ()
	{
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 0;

		Begin();
	}

	public static World ChangeWorld(World w) {
		if (world != null) {
			world.End();
		}

		world = w;
		world.Begin();
		return w;
	}



	public void StartTimeline (Timeline timeline)
	{
		Logger.Log("starting timeline");
		timelines.Add(timeline);
		timeline.parent = this;
	}

	public void EndTimeline(Timeline timeline) {
		Logger.Log("ending timeline");
		timeline.parent = null;
		timelines.Remove(timeline);
	}

	// Update is called once per frame
	void Update ()
	{
		world.InnerUpdate();

		foreach (var t in timelines) {
			t.Update();
		}
	}
}
