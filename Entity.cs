using UnityEngine;
using System.Collections.Generic;
using System;

public class Entity {
	
	private GameObject gameObject;
	private World world;

	private bool isAdded = false;

	private List<Entity> children;
	private Entity parent;

	/// <summary>
	/// The angle of the Entity in degrees, this is _relative_ to the parent's angle. 
	/// Underneath, this maps to the z rotation of the transform.
	/// </summary>
	/// <value>The angle.</value>
	public float angle {
		get { return gameObject.transform.localEulerAngles.z; }
		set { gameObject.transform.localEulerAngles = new Vector3(0, 0, value); }
	}

	/// <summary>
	/// The x position of the Entity, this is _relative_ to the parent's x. 
	/// Underneath, this maps to the x coordinate of the transform.
	/// </summary>
	/// <value>The x position.</value>
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

	/// <summary>
	/// The y position of the Entity, this is _relative_ to the parent's y. 
	/// Underneath, this maps to the y coordinate of the transform.
	/// </summary>
	/// <value>The y position.</value>
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

	/// <summary>
	/// The layer of the Entity, this is _relative_ to the parent's layer. 
	/// Underneath, this maps to the y coordinate of the transform.
	/// </summary>
	/// <value>The layer.</value>
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
		gameObject = new GameObject(this.GetType().Name);
		gameObject.SetActive(false);

		this.x = x;
		this.y = y;
		children = new List<Entity>();

		var access = AddComponent<EntityAccess>();
		access.entity = this;
	}

	/// <summary>
	/// Get acess to the GameObject that represents this Entity in game.
	/// </summary>
	/// <returns>The object.</returns>
	public GameObject InnerObject() {
		return gameObject;
	}

	/// <summary>
	/// Add one Entity as child of this one. This will add the child
	/// to the world and run its Added hook.
	/// Transformations will be relative to the parent from now on.
	/// </summary>
	/// <returns>The child that was added.</returns>
	/// <param name="e">The entity to add.</param>
	/// <typeparam name="T">The type of Entity to add, optional and used for method chaining.</typeparam>
	public T AddChild<T>(T e) where T : Entity {
		children.Add(e);
		e.parent = this;

		if (world != null) {
			e.Add(world);	
		}

		var x = e.x;
		var y = e.y;

		e.InnerObject().transform.parent = InnerObject().transform;

		// Preserve the initial coordinates, Unity will try to help and everything will be offset
		e.x = x;
		e.y = y;

		return e;
	}

	/// <summary>
	/// Gets a child of a particular type.
	/// </summary>
	/// <returns>The child.</returns>
	/// <typeparam name="T">Type of children to find.</typeparam>
	public T GetChild<T>() where T : Entity {
		return (T)children.Find( x => x is T );
	}

	/// <summary>
	/// Gets the children of a particular type from this Entity.
	/// Due to a lack of C# wizardry, I can't seem to get the return
	/// type to be List<T>.
	/// </summary>
	/// <returns>The children of type T.</returns>
	/// <typeparam name="T">Type of children to find.</typeparam>
	public List<Entity> GetChildren<T>() where T : Entity {
		return children.FindAll( x => x is T );
	}

	/// <summary>
	/// Finds a child using a LINQ query.
	/// </summary>
	/// <returns>The child.</returns>
	/// <param name="match">The query.</param>
	public Entity FindChild( Predicate<Entity> match ) {
		return children.Find(match);
	}

	/// <summary>
	/// Finds multiple children using a LINQ query.
	/// </summary>
	/// <returns>The children.</returns>
	/// <param name="match">The query.</param>
	public List<Entity> FindChildren( Predicate<Entity> match ) {
		return children.FindAll(match);
	}

	/// <summary>
	/// Remove a child Entity from this Entity.
	/// This will remove the child from the world and it will no longer update.
	///
	/// </summary>
	/// <returns>The child that was removed.</returns>
	/// <param name="e">The child to remove.</param>
	public Entity RemoveChild(Entity e) {
		children.Remove(e);
		e.parent = null;

		if (world != null) {
			e.Remove();
		}

		e.InnerObject().transform.parent = null;

		return e;
	}

	/// <summary>
	/// Remove a list of children from this Entity.
	/// </summary>
	/// <returns>The children.</returns>
	/// <param name="entities">Entities.</param>
	public List<Entity> RemoveChildren(List<Entity> entities) {
		foreach (var e in entities) {
			RemoveChild(e);
		}

		return entities;
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

	/// <summary>
	/// Called when added to a world, configures the Entity and GameObject
	/// to start updating.
	/// Can be invoked manually if neccessary.
	/// </summary>
	/// <param name="world">World.</param>
	public void Add(World world) {
		this.world = world;
		this.isAdded = true;
		gameObject.SetActive(this.world.IsActive() && this.isAdded);

		foreach (var child in children) {
			child.Add(world);
		}
		
		Added();
	}

	/// <summary>
	/// Called when removed from the world. Has the caveat that if a child remove's itself from the world,
	/// then its parent it re-added, the child will also be re-added.
	/// Can be invoked manually if neccessary.
	/// </summary>
	/// <param name="delay">Optional delay before actually removing the gameObject.</param>
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
