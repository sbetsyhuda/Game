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

			CreateMainLayer();

			CreateBackground();
			CreateFrontLayer();
		}

		protected Vector2 GetRandomBlockFromBlocksOffset(ref Vector2Int[] blocksOffset, Vector2Int size)
		{
			float probability = Random.Range(0f, 1f);
			float summaryProbability = 0f;
			int index = 0;

			while (summaryProbability < probability && index < blocksOffset.Length)
			{
				summaryProbability += this.availableBlocksProbability[index];
				++index;
			}

			return blocksOffset[index - 1];
		}

		protected bool BackgroundFill(Texture2D texture, Vector2Int[] blocksOffset, Vector2Int size)
		{
			for (int x = 0; x < size.x; x += this.blockSize.x)
			{
				for (int y = 0; y < size.y; y += this.blockSize.y)
				{
					texture.ReadPixels(new Rect(GetRandomBlockFromBlocksOffset(ref blocksOffset, size), this.blockSize), x, y);
				}
			}

			return true;
		}

		protected bool FrontLayerFill(Texture2D texture, Vector2Int[] blocksOffset, Vector2Int size)
		{
			for (int x = 0; x < size.x; x += this.blockSize.x)
			{
				texture.ReadPixels(new Rect(GetRandomBlockFromBlocksOffset(ref blocksOffset, size), this.blockSize), x, 0);
				texture.ReadPixels(new Rect(GetRandomBlockFromBlocksOffset(ref blocksOffset, size), this.blockSize), x, size.y - blockSize.y);
			}

			for (int y = this.blockSize.y; y < size.y - this.blockSize.y; y += this.blockSize.y)
			{
				texture.ReadPixels(new Rect(GetRandomBlockFromBlocksOffset(ref blocksOffset, size), this.blockSize), 0, y);
				texture.ReadPixels(new Rect(GetRandomBlockFromBlocksOffset(ref blocksOffset, size), this.blockSize), size.x - blockSize.x, y);
			}

			return true;
		}

		protected void CreateSprite(ref Texture2D texture2D, ref Sprite sprite, Vector2Int size, System.Func<Texture2D, Vector2Int[], Vector2Int, bool> Fill)
		{
			texture2D = new Texture2D(size.x, size.y, TextureFormat.RGBA32, false);

			RenderTexture currentRenderTexture = RenderTexture.active;
			RenderTexture blocksRenderTexture = RenderTexture.GetTemporary(this.blocksTexture.width, this.blocksTexture.height, 32);
			Graphics.Blit(this.blocksTexture, blocksRenderTexture);

			RenderTexture.active = blocksRenderTexture;

			Vector2Int[] blocksOffset = this.availableBlocks.Clone() as Vector2Int[];
			for (int i = 0; i < blocksOffset.Length; ++i)
			{
				blocksOffset[i].x *= this.blockSize.x;
				blocksOffset[i].y *= this.blockSize.y;
			}

			Color mainColor = Color.clear;
			Color[] colors = texture2D.GetPixels();

			for (int i = 0; i < colors.Length; ++i)
			{
				colors[i] = mainColor;
			}

			texture2D.SetPixels(colors);


			Fill(texture2D, blocksOffset, size);

			texture2D.Apply();

			RenderTexture.active = currentRenderTexture;
			RenderTexture.ReleaseTemporary(blocksRenderTexture);

			sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
		}

		protected override void CreateBackground()
		{
			this.backgroundSize = this.size - this.blockSize * 2;

			CreateSprite(ref this.backgroundTexture, ref this.backgroundSprite, this.backgroundSize, BackgroundFill);

			AddBackgroundToLocation();
		}

		protected override void CreateFrontLayer()
		{
			this.frontLayerSize = this.size + this.blockSize * 4;
			this.frontLayerInteriorSize = this.size + this.blockSize * 2;

			CreateSprite(ref this.frontLayerTexture, ref this.frontLayerSprite, this.frontLayerSize, FrontLayerFill);

			AddFrontLayerToLocation();
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