using System;
using System.Collections.Generic;

public class World {

	private List<Entity> entities;
	private bool isActive = false;

	public World () {
		entities = new List<Entity>();
	}

	public bool IsActive() {
		return isActive;
	}

	public void Begin() {
		isActive = true;

		foreach (var e in entities) {
			e.InnerObject().SetActive(e.IsAdded());
		}
	}

	public void End() {
		isActive = false;

		foreach (var e in entities) {
			e.InnerObject().SetActive(false);
		}
	}

	public Entity Add(Entity e) {
		e.Add(this);
		entities.Add(e);

		return e;
	}

	public Entity Remove(Entity e) {
		e.Remove();
		entities.Remove(e);

		return e;
	}

	public void Update() {
		foreach (var e in entities) {
			e.Update();
		}
	}

}


