using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
	public class EarthenRectangleRoom : ReactangleRoom
	{
		public EarthenRectangleRoom(Vector2Int size, Vector2Int position, Texture blocksTexture) : base("Earthen Rectangle Room", size, position, blocksTexture, new Vector2Int(196, 196), new Vector2Int[] { new Vector2Int(0, 3), new Vector2Int(0, 4), new Vector2Int(1, 3), new Vector2Int(1, 4) }, new float[] { 0.1f, 0.15f, 0.6f, 0.15f })
		{

		}
	}
}