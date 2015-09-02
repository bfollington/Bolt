using UnityEngine;
using System.Collections.Generic;

public class Entity {
	
	private GameObject gameObject;
	private World world;

	private bool isAdded = false;

	private List<Entity> children;
	private Entity parent;

	public float angle {
		get { return gameObject.transform.localEulerAngles.z; }
		set { gameObject.transform.localEulerAngles = new Vector3(0, 0, value); }
	}

	public float x
	{
		get { return gameObject.transform.localPosition.x; }
	    set {
			gameObject.transform.localPosition = new Vector3(
				value,
				gameObject.transform.localPosition.y,
				gameObject.transform.localPosition.z
			);
	    }
	}

	public float y
	{
		get { return gameObject.transform.localPosition.y; }
	    set {
			gameObject.transform.localPosition = new Vector3(
				gameObject.transform.localPosition.x,
				value,
				gameObject.transform.localPosition.z
			);
	    }
	}

	public float layer
	{
	    get { return gameObject.transform.localPosition.z; }
		set {
			gameObject.transform.localPosition = new Vector3(
				gameObject.transform.localPosition.x,
				gameObject.transform.localPosition.y,
				value
			);
		}
	}

	public Entity(float x = 0, float y = 0) {
		gameObject = new GameObject("Test");
		gameObject.SetActive(false);

		this.x = x;
		this.y = y;
		children = new List<Entity>();
	}

	public GameObject InnerObject() {
		return gameObject;
	}

	public Entity AddChild(Entity e) {
		children.Add(e);
		e.parent = this;

		if (world != null) {
			e.Add(world);	
		}

		e.InnerObject().transform.parent = InnerObject().transform;

		return e;
	}

	public T AddChildTyped<T>(T e) where T : Entity {
		AddChild(e);
		return e;
	}

	public Entity RemoveChild(Entity e) {
		children.Remove(e);
		e.parent = null;

		if (world != null) {
			e.Remove();
		}

		e.InnerObject().transform.parent = null;

		return e;
	}

	public bool IsAdded() {
		return isAdded;
	}

	public void SetName(string name) {
		gameObject.name = name;
	}
	
	public T GetComponent<T>() {
		return gameObject.GetComponent<T>();
	}
	
	public T AddComponent<T>() where T : Component {
		return gameObject.AddComponent<T>() as T;
	}

	public void Add(World world) {
		this.world = world;
		this.isAdded = true;
		gameObject.SetActive(this.world.IsActive() && this.isAdded);

		foreach (var child in children) {
			child.Add(world);
		}
		
		Added();
	}

	public void Remove(float delay = 0) {
		Removed();

		foreach (var child in children) {
			child.Remove(delay);
		}
		
		gameObject.SetActive(false);
		world = null;
		this.isAdded = false;
	}

	public void Added() {
	
	}
	
	public void Removed() {
		
	}
	
	// Update is called once per frame
	public virtual void Update() {

		foreach (var child in children) {
			child.Update();
		}
	}
}
