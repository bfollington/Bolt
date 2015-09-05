using Bolt;
using UnityEngine;

public class SpriteGraphic : Graphic {

    public Sprite sprite {
    	get { return child.GetComponent<SpriteRenderer>().sprite; }
    	set { child.GetComponent<SpriteRenderer>().sprite = value; }
    }

	public float alpha {
		get { return child.GetComponent<SpriteRenderer>().color.a; }
		set { 
			child.GetComponent<SpriteRenderer>().color = new Color(
				child.GetComponent<SpriteRenderer>().color.r,
				child.GetComponent<SpriteRenderer>().color.g,
				child.GetComponent<SpriteRenderer>().color.b,
				value
			);
		}
	}

    public SpriteGraphic(Sprite sprite = null, float x = 0, float y = 0) : base() {
        this.x = x;
        this.y = y;

		Logger.Log(this.x, this.y);

        child.AddComponent<SpriteRenderer>();
        this.sprite = sprite;
    }

    public SpriteGraphic(string path, float x = 0, float y = 0) : this(Resources.Load<Sprite>(path), x, y) {

    }

    public void setOrigin(float x, float y) {
        child.x = -x;
        child.y = -y;
    }

    public void centerOrigin() {
    	child.x = -sprite.bounds.size.x / 2;
		child.y = -sprite.bounds.size.y / 2;
    }

    public override void Update ()
	{
		base.Update ();
	}
}
