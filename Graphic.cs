using Bolt;

public class Graphic : Entity {

	protected Entity child;

	public float originX
	{
		get { return child.x; }
	    set { child.x = value; }
	}

	public float originY
	{
		get { return child.y; }
	    set { child.y = value; }
	}

    public Graphic() {
		child = AddChild( new Entity() );
    }
}
