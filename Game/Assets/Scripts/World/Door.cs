using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
	public abstract class Door : WorldObject
	{
		protected Location firstLocation;
		protected Location secondLocation;
		protected DoorStatus status;
		protected DoorType type;

		public Location FirstLocation { get => firstLocation; }
		public Location SecondLocation { get => secondLocation; }
		public DoorStatus Status { get => status; }
		public DoorType Type { get => type; }

		public enum DoorStatus
		{
			Close,
			OpenToFirstLocation,
			OpenToSecondLocation
		}

		public enum DoorType
		{
			FrontDoor,
			BackDoor
		}

		public Door(string name, Vector2Int size, Vector2Int position) : base(name, size, position)
		{

		}
	}
}