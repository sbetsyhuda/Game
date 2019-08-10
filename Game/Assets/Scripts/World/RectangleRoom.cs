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

		protected GameObject floorCollision;
		protected Vector2Int floorCollisionSize;
		protected GameObject ceilingCollision;
		protected Vector2Int ceilingCollisionSize;
		protected GameObject leftWallCollision;
		protected Vector2Int leftWallCollisionSize;
		protected GameObject rightWallCollision;
		protected Vector2Int rightWallCollisionSize;

		protected GameObject floor;
		protected Sprite floorSprite;
		protected Vector2Int floorSize;
		protected GameObject ceiling;
		protected Sprite ceilingSprite;
		protected Vector2Int ceilingSize;
		protected GameObject leftWall;
		protected Sprite leftWallSprite;
		protected Vector2Int leftWallSize;
		protected GameObject rightWall;
		protected Sprite rightWallSprite;
		protected Vector2Int rightWallSize;

		public ReactangleRoom(string name, Vector2Int size, int locationDepth, Vector2Int position, Texture blocksTexture, Vector2Int blockSize, Vector2Int[] availableBlocks, float[] availableBlocksProbability) : base(name, size, locationDepth, position)
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

		protected bool FullFilling(Texture2D texture, Vector2Int[] blocksOffset, Vector2Int size)
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

		protected void CreateSprite(ref Sprite sprite, Vector2Int size, System.Func<Texture2D, Vector2Int[], Vector2Int, bool> Fill)
		{
			Texture2D texture2D = new Texture2D(size.x, size.y, TextureFormat.RGBA32, false);

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
			this.backgroundSize = this.size;

			CreateSprite(ref this.backgroundSprite, this.backgroundSize, FullFilling);

			AddBackgroundToLocation();
		}

		protected override void CreateFrontLayer()
		{
			this.frontLayerSize = this.size + this.blockSize * 2;
			this.frontLayerInteriorSize = this.size;

			CreateSprite(ref this.frontLayerSprite, this.frontLayerSize, FrontLayerFill);

			AddFrontLayerToLocation();
		}

		protected override void CreateMainLayer()
		{
			this.mainLayer = new GameObject("Main Layer");

			CreateFloorCollision();
			CreateCeilingCollision();
			CreateLeftWallCollision();
			CreateRightWallCollision();

			CreateFloor();
			CreateCeiling();
			CreateLeftWall();
			CreateRightWall();

			AddMainLayerToLocation();
		}

		protected void CreateFloorCollision()
		{
			this.floorCollisionSize = new Vector2Int(this.size.x + this.blockSize.x * 2, this.blockSize.y);

			CreateItem(this.mainLayer, ref this.floorCollision, this.floorCollisionSize, "Floor Collision", new Vector3(0f, this.size.y / -200f - floorCollisionSize.y / 200f, 0f));

			this.floorCollision.tag = "floor";
			this.floorCollision.layer = 10;
		}

		protected void CreateCeilingCollision()
		{
			this.ceilingCollisionSize = new Vector2Int(this.size.x + this.blockSize.x * 2, this.blockSize.y);

			CreateItem(this.mainLayer, ref this.ceilingCollision, this.ceilingCollisionSize, "Ceiling Collision", new Vector3(0f, this.size.y / 200f + floorCollisionSize.y / 200f, 0f));
		}

		protected void CreateLeftWallCollision()
		{
			this.leftWallCollisionSize = new Vector2Int(this.blockSize.x, this.size.y);

			CreateItem(this.mainLayer, ref this.leftWallCollision, this.leftWallCollisionSize, "Left Wall Collision", new Vector3(this.size.x / -200f - leftWallCollisionSize.x / 200f, 0f));
		}

		protected void CreateRightWallCollision()
		{
			this.rightWallCollisionSize = new Vector2Int(this.blockSize.x, this.size.y);

			CreateItem(this.mainLayer, ref this.rightWallCollision, this.rightWallCollisionSize, "Right Wall Collision", new Vector3(this.size.x / 200f + rightWallCollisionSize.x / 200f, 0f));
		}


		protected void CreateFloor()
		{
			this.floorSize = new Vector2Int(this.size.x, this.locationDepth);

			CreateSprite(ref this.floorSprite, this.floorSize, FullFilling);

			CreateItem(this.mainLayer, ref this.floor, "Floor", this.floorSprite, new Vector3(0f, this.size.y / -200f, 0f), Quaternion.Euler(90f, 0f, 0f));
		}

		protected void CreateCeiling()
		{
			this.ceilingSize = new Vector2Int(this.size.x, this.locationDepth);

			CreateSprite(ref this.ceilingSprite, this.ceilingSize, FullFilling);

			CreateItem(this.mainLayer, ref this.ceiling, "Ceiling", this.ceilingSprite, new Vector3(0f, this.size.y / 200f, 0f), Quaternion.Euler(-90f, 0f, 0f));
		}

		protected void CreateLeftWall()
		{
			this.leftWallSize = new Vector2Int(this.locationDepth, this.size.y);

			CreateSprite(ref this.leftWallSprite, this.leftWallSize, FullFilling);

			CreateItem(this.mainLayer, ref this.leftWall, "LeftWall", this.leftWallSprite, new Vector3(this.size.x / -200f, 0f, 0f), Quaternion.Euler(0f, 90f, 0f));
		}

		protected void CreateRightWall()
		{
			this.rightWallSize = new Vector2Int(this.locationDepth, this.size.y);

			CreateSprite(ref this.rightWallSprite, this.rightWallSize, FullFilling);

			CreateItem(this.mainLayer, ref this.rightWall, "RightWall", this.rightWallSprite, new Vector3(this.size.x / 200f, 0f, 0f), Quaternion.Euler(0f, -90f, 0f));
		}

	}
}