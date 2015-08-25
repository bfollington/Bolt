﻿using UnityEngine;
using System.Collections;
using Bolt.Timeline;

public class SceneManager : MonoBehaviour
{
	
	public CutsceneTimeline timeline;
	private World world;

	// Use this for initialization
	void Start ()
	{
		world = new World();
		world.Add( new TestEntity(0, 0) );

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
