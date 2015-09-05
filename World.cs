using System;
using System.Collections.Generic;

public class World {

	private List<Entity> toRemoveAtEndOfUpdate;
	private List<Entity> toAddAtEndOfUpdate;
	private List<Entity> entities;
	private bool isActive = false;

	public World () {
		entities = new List<Entity>();
		toAddAtEndOfUpdate = new List<Entity>();
		toRemoveAtEndOfUpdate = new List<Entity>();
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

	/// <summary>
	/// Add the specified entity to the world. 
	/// Additions are queued until the end of the update cycle.
	/// </summary>
	/// <param name="e">Entity.</param>
	public Entity Add(Entity e) {
		toAddAtEndOfUpdate.Add(e);

		return e;
	}

	/// <summary>
	/// Remove the specified entity to the world. 
	/// Removals are queued until the end of the update cycle.
	/// </summary>
	/// <param name="e">Entity.</param>
	public Entity Remove(Entity e) {
		toRemoveAtEndOfUpdate.Add(e);

		return e;
	}

	private void HandleAddedEntities() {
		foreach (var e in toAddAtEndOfUpdate) {
			e.Add(this);
			entities.Add(e);
		}

		toAddAtEndOfUpdate.Clear();
	}

	private void HandleRemovedEntities() {
		foreach (var e in toRemoveAtEndOfUpdate) {
			e.Remove();
			entities.Remove(e);
		}

		toRemoveAtEndOfUpdate.Clear();
	}

	public virtual void Update() {

	}

	/// <summary>
	/// Updates the world and all entities within.
	/// Do NOT override this method, override Update() instead.
	/// </summary>
	public void InnerUpdate() {

		Update();

		foreach (var e in entities) {
			e.Update();
		}

		HandleAddedEntities();
		HandleRemovedEntities();
	}

}


