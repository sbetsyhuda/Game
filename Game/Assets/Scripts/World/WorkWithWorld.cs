using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.World;

public class WorkWithWorld : MonoBehaviour
{
	public GameObject character;
	public GameObject mainCamera;

	public Texture blocksTexture;
	public Vector2Int backgroundSize;
	public int roomDepth;
	public Vector2Int backgroundPosition;

	public Texture2D backgroundTexture;
	public Sprite backgroundSprite;

	private ReactangleRoom reactangleRoom;
	// Start is called before the first frame update
	void Start()
    {
		this.reactangleRoom = new EarthenRectangleRoom(backgroundSize, roomDepth, backgroundPosition, this.blocksTexture);

		this.reactangleRoom.AddLocationToWorld(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
		this.reactangleRoom.UpdateForeGroundStatus(mainCamera);
    }
}
