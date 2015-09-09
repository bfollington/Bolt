using UnityEngine;
using System.Collections.Generic;
using Bolt;

public class SceneManager : MonoBehaviour
{

	private static List<Timeline> timelines = new List<Timeline>();
	private static World world;


	public SceneManager () : base ()
	{
		
	}

	public static Camera Cam() {
		return Camera.main;
	}

	// Use this for initialization
	void Start ()
	{
		var world = ChangeWorld( new World() );
		world.Add( new Sylvi(0, 0) );

		var e = new Entity(32, 0);
		var p = e.AddComponent<Polybox>();
		p.type = "solid";

		p.AddVertex(new Vector2(0, 0));
		p.AddVertex(new Vector2(0, 128));
		p.AddVertex(new Vector2(64, 64));

		world.Add(e);

		e = new Entity(64, 0);
		p = e.AddComponent<Polybox>();
		p.type = "solid";

		p.AddVertex(new Vector2(0, 0));
		p.AddVertex(new Vector2(0, 128));
		p.AddVertex(new Vector2(64, 64));

		world.Add(e);

		StartTimeline (new TestTimeline ());
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
