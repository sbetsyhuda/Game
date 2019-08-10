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
		protected Sprite backgroundSprite;

		protected GameObject mainLayer;

		protected GameObject frontLayer;
		protected Vector2Int frontLayerSize;
		protected Vector2Int frontLayerInteriorSize;
		protected Vector3 frontLayerOffset;
		protected Sprite frontLayerSprite;

		protected int locationDepth;

		public Location(string name, Vector2Int size, int locationDepth, Vector2Int position) : base(name, size, position)
		{
			this.locationDepth = locationDepth;
			this.locationGameObject = new GameObject(name);
			this.objects = new List<WorldObject>();
			this.backgroundOffset = Vector3.forward * (this.locationDepth / 200f);
			this.frontLayerOffset = Vector3.back * (this.locationDepth / 200f);
		}

		protected abstract void CreateBackground();
		protected abstract void CreateFrontLayer();
		protected abstract void CreateMainLayer();

		public void AddLocationToWorld(GameObject world)
		{
			this.locationGameObject.transform.parent = world.transform;
		}

		protected void CreateItem(GameObject parent, ref GameObject item, string name, Sprite sprite, Vector3 position, Quaternion rotation)
		{
			item = new GameObject(name);
			SpriteRenderer spriteRenderer = item.AddComponent<SpriteRenderer>();
			spriteRenderer.sprite = sprite;

			item.transform.parent = parent.transform;
			item.transform.position = position;
			item.transform.rotation = rotation;
		}

		protected void CreateItem(GameObject parent, ref GameObject item, Vector2Int itemSize, string name, Vector3 position)
		{
			item = new GameObject(name);
			item.transform.parent = parent.transform;
			BoxCollider2D boxCollider2D = item.AddComponent<BoxCollider2D>();

			boxCollider2D.size = new Vector2(itemSize.x / 100f, itemSize.y / 100f);
			item.transform.position = position;
		}

		protected void AddBackgroundToLocation()
		{
			CreateItem(this.locationGameObject, ref this.background, "Background", this.backgroundSprite, this.backgroundOffset, Quaternion.identity);
		}

		protected void AddFrontLayerToLocation()
		{
			CreateItem(this.locationGameObject, ref this.frontLayer, "Front Layer", this.frontLayerSprite, this.frontLayerOffset, Quaternion.identity);
		}

		protected void AddMainLayerToLocation()
		{
			this.mainLayer.transform.parent = this.locationGameObject.transform;
		}

	}
}