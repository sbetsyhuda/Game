﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
	public abstract class Room : Location
	{
		public Room(string name, Vector2Int size, int locationDepth, Vector2Int position) : base(name, size, locationDepth, position)
		{

		}
	}
}