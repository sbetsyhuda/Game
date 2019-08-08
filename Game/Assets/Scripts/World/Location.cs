using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
	public abstract class Location : WorldObject
	{
		protected List<WorldObject> objects;

		public List<WorldObject> Objects { get => objects; }

		protected Texture blocksTexture;
		protected Texture2D backgroundTexture;
		protected Sprite backgroundSprite;
		protected Vector2Int backgroundSize;

		public Location(string name, Vector2Int size, Vector2Int position) : base(name, size, position)
		{
			this.objects = new List<WorldObject>();
		}

		public Texture2D GetBackgroundTexture2D()
		{
			return this.backgroundTexture;
		}
		public Sprite GetBackgroundSprite()
		{
			return this.backgroundSprite;
		}
	}
}