using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.World;

public class WorkWithWorld : MonoBehaviour
{
	public Texture blocksTexture;
	public Vector2Int backgroundSize;
	public Vector2Int backgroundPosition;

	public Texture2D backgroundTexture;
	public Sprite backgroundSprite;

	private ReactangleRoom reactangleRoom;
	// Start is called before the first frame update
	void Start()
    {
		this.reactangleRoom = new EarthenRectangleRoom(backgroundSize, backgroundPosition, this.blocksTexture);

		this.backgroundTexture = this.reactangleRoom.GetBackgroundTexture2D();
		this.backgroundSprite = this.reactangleRoom.GetBackgroundSprite();

		this.reactangleRoom.AddLocationToWorld(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
		this.reactangleRoom.UpdateBackgroundPosition(gameObject);
    }
}
