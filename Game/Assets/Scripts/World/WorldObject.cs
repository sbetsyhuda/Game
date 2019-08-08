using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
	public abstract class WorldObject
	{
		protected string name;
		protected Vector2Int size;
		protected Vector2Int position;

		public string Name { get => name; }
		public Vector2Int Size { get => size; }
		public Vector2Int Position { get => position; }

		public WorldObject(string name, Vector2Int size, Vector2Int position)
		{
			this.name = name;
			this.size = size;
			this.position = position;
		}

		public Sprite GetSprite()
		{
			return null;
		}
	}
}