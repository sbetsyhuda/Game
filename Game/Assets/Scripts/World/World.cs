using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
	public class World
	{
		private List<Location> locations;
		private Vector2Int size;
		private Location selectedLocation;
		private Dictionary<int, Vector2Int> characterPositions;

		public List<Location> Locations { get => locations; }
		public Vector2Int Size { get => size; }
		public Location SelectedLocation { get => selectedLocation; }
		public Dictionary<int, Vector2Int> CharacterPositions { get => characterPositions; }
	}
}
