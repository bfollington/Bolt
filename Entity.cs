using UnityEngine;
using System.Collections;

public class Entity {
	
	private GameObject gameObject;
	private World world;

	private Vector2 pos;
	private bool isAdded = false;

	public Entity(float x = 0, float y = 0) {
		gameObject = new GameObject("Test");
		gameObject.SetActive(false);

		pos = new Vector2(x, y);
	}

	public GameObject InnerObject() {
		return gameObject;
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
		
		Added();
	}

	public void Remove(float delay = 0) {
		Removed();
		
		gameObject.SetActive(false);
		world = null;
		this.isAdded = false;
	}

	public void Added() {
	
	}
	
	public void Removed() {
		
	}
	
	// Update is called once per frame
	public void Update() {
		pos.x = pos.x + 1;
		gameObject.transform.position = new Vector3(pos.x, pos.y, 0);
	}
}
