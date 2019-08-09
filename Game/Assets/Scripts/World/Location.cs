using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
	public abstract class Location : WorldObject
	{
		protected List<WorldObject> objects;
		public List<WorldObject> Objects { get => objects; }

		protected GameObject locationGameObject;

		protected GameObject backgroundGameObject;
		protected Vector3 backgroundOffset;
		protected Texture2D backgroundTexture;
		protected Sprite backgroundSprite;
		protected Vector2Int backgroundSize;

		protected GameObject mainLayer;

		public Location(string name, Vector2Int size, Vector2Int position) : base(name, size, position)
		{
			this.locationGameObject = new GameObject(name);
			this.objects = new List<WorldObject>();
			this.backgroundOffset = Vector3.forward;
		}

		protected abstract void CreateBackground();

		public Texture2D GetBackgroundTexture2D()
		{
			return this.backgroundTexture;
		}
		public Sprite GetBackgroundSprite()
		{
			return this.backgroundSprite;
		}

		public void AddLocationToWorld(GameObject world)
		{
			this.locationGameObject.transform.parent = world.transform;
		}

		protected void AddBackgroundToLocation()
		{
			this.backgroundGameObject = new GameObject("Background GameObject");
			SpriteRenderer spriteRenderer = this.backgroundGameObject.AddComponent<SpriteRenderer>();
			spriteRenderer.sprite = this.backgroundSprite;

			this.backgroundGameObject.transform.position += new Vector3(0f, 0f, 1f);

			this.backgroundGameObject.transform.parent = this.locationGameObject.transform;
		}

		protected void AddMainLayerToLocation()
		{
			this.mainLayer.transform.parent = this.locationGameObject.transform;
		}

		protected float GetRelativeCharacterPosition(float position, float size)
		{
			return position / size + 0.5f;
		}

		protected float GetCharacterPositionOffset(float position, float size, float backgroundSize)
		{
			return GetRelativeCharacterPosition(position, size) * (size - backgroundSize) - (size - backgroundSize) / 2f;
		}

		public void UpdateBackgroundPosition(GameObject world, GameObject character)
		{
			float xOffset = GetCharacterPositionOffset(character.transform.position.x, this.size.x / 100f, this.backgroundSize.x / 100f);
			float yOffset = GetCharacterPositionOffset(character.transform.position.y, this.size.y / 100f, this.backgroundSize.y / 100f);

			this.backgroundGameObject.transform.position = new Vector3(xOffset, yOffset, 0f) + backgroundOffset;
		}

		protected abstract void CreateMainLayer();
	}
}