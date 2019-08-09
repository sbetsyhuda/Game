using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
	public class ReactangleRoom : Room
	{
		protected Texture blocksTexture;
		protected Vector2Int blockSize;
		protected Vector2Int[] availableBlocks;
		protected float[] availableBlocksProbability;

		protected GameObject floor;
		protected Vector2Int floorSize;
		protected GameObject ceiling;
		protected Vector2Int ceilingSize;
		protected GameObject leftWall;
		protected Vector2Int leftWallSize;
		protected GameObject rightWall;
		protected Vector2Int rightWallSize;

		public ReactangleRoom(string name, Vector2Int size, Vector2Int position, Texture blocksTexture, Vector2Int blockSize, Vector2Int[] availableBlocks, float[] availableBlocksProbability) : base(name, size, position)
		{
			this.blockSize = blockSize;
			this.availableBlocks = availableBlocks;
			this.availableBlocksProbability = availableBlocksProbability;
			this.blocksTexture = blocksTexture;

			CreateBackground();

			CreateMainLayer();
		}

		protected override void CreateBackground()
		{
			this.backgroundSize = this.size - this.blockSize * 2;
			this.backgroundTexture = new Texture2D(this.backgroundSize.x, this.backgroundSize.y, TextureFormat.RGBA32, false);

			RenderTexture currentRenderTexture = RenderTexture.active;
			RenderTexture backgroundRenderTexture = RenderTexture.GetTemporary(this.blocksTexture.width, this.blocksTexture.height, 32);
			Graphics.Blit(this.blocksTexture, backgroundRenderTexture);

			RenderTexture.active = backgroundRenderTexture;

			Vector2Int[] blocksOffset = this.availableBlocks;
			for (int i = 0; i < blocksOffset.Length; ++i)
			{
				blocksOffset[i].x *= this.blockSize.x;
				blocksOffset[i].y *= this.blockSize.y;
			}

			for (int x = 0; x < this.backgroundSize.x; x += this.blockSize.x)
			{
				for (int y = 0; y < this.backgroundSize.y; y += this.blockSize.y)
				{
					float probability = Random.Range(0f, 1f);
					float summaryProbability = 0f;
					int index = 0;

					while (summaryProbability < probability && index < blocksOffset.Length)
					{
						summaryProbability += this.availableBlocksProbability[index];
						++index;
					}

					this.backgroundTexture.ReadPixels(new Rect(blocksOffset[index - 1].x, blocksOffset[index - 1].y, this.blockSize.x, this.blockSize.y), x, y);
				}
			}


			this.backgroundTexture.Apply();


			RenderTexture.active = currentRenderTexture;
			RenderTexture.ReleaseTemporary(backgroundRenderTexture);

			this.backgroundSprite = Sprite.Create(this.backgroundTexture, new Rect(0f, 0f, this.backgroundTexture.width, this.backgroundTexture.height), new Vector2(0.5f, 0.5f));

			AddBackgroundToLocation();
		}

		protected override void CreateMainLayer()
		{
			this.mainLayer = new GameObject("Main Layer");

			CreateFloor();
			CreateCeiling();
			CreateLeftWall();
			CreateRightWall();

			AddMainLayerToLocation();
		}

		protected void CreateMainLayerItem(ref GameObject mainLayerItem, Vector2Int itemSize,  string name, Vector3 position)
		{
			mainLayerItem = new GameObject(name);
			mainLayerItem.transform.parent = this.mainLayer.transform;
			BoxCollider2D boxCollider2D = mainLayerItem.AddComponent<BoxCollider2D>();

			boxCollider2D.size = new Vector2(itemSize.x / 100f, itemSize.y / 100f);
			mainLayerItem.transform.position = position;
		}

		protected void CreateFloor()
		{
			this.floorSize = new Vector2Int(this.size.x + this.blockSize.x * 2, this.blockSize.y);

			CreateMainLayerItem(ref this.floor, this.floorSize, "Floor", new Vector3(0f, this.size.y / -200f - floorSize.y / 200f, 0f));

			this.floor.tag = "floor";
		}

		protected void CreateCeiling()
		{
			this.ceilingSize = new Vector2Int(this.size.x + this.blockSize.x * 2, this.blockSize.y);

			CreateMainLayerItem(ref this.ceiling, this.ceilingSize, "Ceiling", new Vector3(0f, this.size.y / 200f + floorSize.y / 200f, 0f));
		}

		protected void CreateLeftWall()
		{
			this.leftWallSize = new Vector2Int(this.blockSize.x, this.size.y);

			CreateMainLayerItem(ref this.leftWall, this.leftWallSize, "Left Wall", new Vector3(this.size.x / -200f - leftWallSize.x / 200f, 0f));
		}

		protected void CreateRightWall()
		{
			this.rightWallSize = new Vector2Int(this.blockSize.x, this.size.y);

			CreateMainLayerItem(ref this.rightWall, this.rightWallSize, "Right Wall", new Vector3(this.size.x / 200f + rightWallSize.x / 200f, 0f));
		}
	}
}