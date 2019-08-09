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

		protected GameObject background;
		protected Vector2Int backgroundSize;
		protected Vector3 backgroundOffset;
		protected Texture2D backgroundTexture;
		protected Sprite backgroundSprite;

		protected GameObject mainLayer;

		protected GameObject frontLayer;
		protected Vector2Int frontLayerSize;
		protected Texture2D frontLayerTexture;
		protected Sprite frontLayerSprite;

		public Location(string name, Vector2Int size, Vector2Int position) : base(name, size, position)
		{
			this.locationGameObject = new GameObject(name);
			this.objects = new List<WorldObject>();
			this.backgroundOffset = Vector3.forward;
		}

		protected abstract void CreateBackground();

		protected abstract void CreateFrontLayer();

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


		protected void AddItemToLocation(ref GameObject item, string name, Sprite sprite, Vector3 offset)
		{
			item = new GameObject(name);
			SpriteRenderer spriteRenderer = item.AddComponent<SpriteRenderer>();
			spriteRenderer.sprite = sprite;

			item.transform.parent = this.locationGameObject.transform;
			item.transform.position = offset;
		}

		protected void AddBackgroundToLocation()
		{
			AddItemToLocation(ref this.background, "Background", this.backgroundSprite, Vector3.forward);
		}

		protected void AddFrontLayerToLocation()
		{
			AddItemToLocation(ref this.frontLayer, "Front Layer", this.frontLayerSprite, Vector3.back);
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

			this.background.transform.position = new Vector3(xOffset, yOffset, 0f) + this.backgroundOffset;
		}

		protected abstract void CreateMainLayer();
	}
}