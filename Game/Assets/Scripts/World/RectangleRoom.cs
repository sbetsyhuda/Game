﻿using System.Collections;
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

		public ReactangleRoom(string name, Vector2Int size, Vector2Int position, Texture blocksTexture, Vector2Int blockSize, Vector2Int[] availableBlocks, float[] availableBlocksProbability) : base(name, size, position)
		{
			this.blockSize = blockSize;
			this.availableBlocks = availableBlocks;
			this.availableBlocksProbability = availableBlocksProbability;
			this.blocksTexture = blocksTexture;

			CreateBackground();
		}

		private void CreateBackground()
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

			this.backgroundSprite = Sprite.Create(this.backgroundTexture, new Rect(0f, 0f, this.backgroundTexture.width, this.backgroundTexture.height), Vector2.zero);
		}
	}
}