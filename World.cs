using System;
using System.Collections.Generic;

public class World {

	private List<Entity> entities;

	public World () {
		entities = new List<Entity>();
	}

	public void Begin() {
		foreach (var e in entities) {
			e.InnerObject().SetActive(e.IsAdded());
		}
	}

	public void End() {
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


