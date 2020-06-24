using System;
using System.Collections.Generic;
using Top2dGame.Client.Common;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.GameObjects.Terrain;
using Top2dGame.Client.Master;
using Top2dGame.Client.Scripts.Base;
using Top2dGame.Model.Const;

namespace Top2dGame.Client.Scripts.Character
{
	public class CharacterDetectScript : GameScript
	{
		/// <summary>
		/// Sight range
		/// </summary>
		public int SightRange { get; set; }

		/// <summary>
		/// Detected location list
		/// </summary>
		public IList<Position> DetectedLocationList { get; set; }

		// TODO Need save this?
		/// <summary>
		/// Not Detected location list
		/// </summary>
		private IList<Position> NotDetectedLocationList { get; set; }

		public CharacterDetectScript() : base()
		{
			SightRange = 0;
			DetectedLocationList = new List<Position>();
			NotDetectedLocationList = new List<Position>();
		}

		protected override void Start()
		{

		}
		
		public override void Update()
		{
			// TODO It is fine on performance?(frame process)
			DetectedLocationList = new List<Position>();
			NotDetectedLocationList = new List<Position>();
			SeeAround(DetectedLocationList, NotDetectedLocationList, GameObject.Position.X, GameObject.Position.Y);

			RemoveInvalidDetectedList(DetectedLocationList, NotDetectedLocationList);
		}

		public override void UpdateEveryTurn()
		{

		}

		/// <summary>
		/// See around.
		/// </summary>
		/// <param name="detectedLocationList">Detected list</param>
		/// <param name="notDetectedLocationList">Not detected list</param>
		/// <param name="x">Center X</param>
		/// <param name="y">Center Y</param>
		private void SeeAround(IList<Position> detectedLocationList, IList<Position> notDetectedLocationList, int x, int y)
		{
			// See upper
			See(detectedLocationList, notDetectedLocationList, x - 1, y + 1);
			See(detectedLocationList, notDetectedLocationList, x, y + 1);
			See(detectedLocationList, notDetectedLocationList, x + 1, y + 1);
			// See side
			See(detectedLocationList, notDetectedLocationList, x - 1, y);
			See(detectedLocationList, notDetectedLocationList, x + 1, y);
			// See lower
			See(detectedLocationList, notDetectedLocationList, x - 1, y - 1);
			See(detectedLocationList, notDetectedLocationList, x, y - 1);
			See(detectedLocationList, notDetectedLocationList, x + 1, y - 1);
		}

		/// <summary>
		/// See specified location.
		/// </summary>
		/// <param name="detectedLocationList">Detected list</param>
		/// <param name="notDetectedLocationList">Not detected list</param>
		/// <param name="x">Location X</param>
		/// <param name="y">Location Y</param>
		private void See(IList<Position> detectedLocationList, IList<Position> notDetectedLocationList, int x, int y)
		{
			GameMaster gameMaster = GameMaster.GetInstance();
			IList<GameObject> gameObjects;
			GameObject terrainGameObject;

			// Out of sight
			if (GetRange(x, y) > SightRange)
			{
				return;
			}

			// This location is already end. 
			if (detectedLocationList.Contains(new Position { X = x, Y = y }) || notDetectedLocationList.Contains(new Position { X = x, Y = y }))
			{
				return;
			}

			gameObjects = gameMaster.GetGameObjects(x, y);

			// There is nothing. so cannot see.
			if (gameObjects.Count == 0)
			{
				notDetectedLocationList.Add(new Position
				{
					X = x,
					Y = y
				});

				return;
			}

			// Can see
			detectedLocationList.Add(new Position
			{
				X = x,
				Y = y
			});

			terrainGameObject = gameMaster.FindGameObjectAsTag(gameObjects, TagConst.TERRAIN);
			// There is wall. so cannot see to beyond.
			if (terrainGameObject != null && terrainGameObject is WallGameObject)
			{
				AddBeyondWall(notDetectedLocationList, x, y, GetDirection(x, y));

				return;
			}

			SeeAround(detectedLocationList, notDetectedLocationList, x, y);
		}

		/// <summary>
		/// Get range from game object.
		/// </summary>
		/// <param name="x">location x</param>
		/// <param name="y">location y</param>
		/// <returns>Range</returns>
		private int GetRange(int x, int y)
		{
			int rangeX = Math.Abs(GameObject.Position.X - x);
			int rangeY = Math.Abs(GameObject.Position.Y - y);

			return Math.Max(rangeX, rangeY);
		}

		/// <summary>
		/// Get direction from game object.
		/// </summary>
		/// <param name="x">location x</param>
		/// <param name="y">location y</param>
		/// <returns>Direction</returns>
		private double GetDirection(int x, int y)
		{
			int differenceX = x - GameObject.Position.X;
			/* 
			 * Direction up is 
			 * -1 (in game)
			 * 1 (in atan2)
			 * So, reverse the y value
			 */
			int differenceY = (y - GameObject.Position.Y) * -1;
			double targetDirection = Math.Atan2(differenceY, differenceX);

			if (IsIncludeInDirection(targetDirection, DirectionConst.RIGHT, DirectionConst.VALID_ANGLE))
			{
				return DirectionConst.RIGHT;
			}
			else if (IsIncludeInDirection(targetDirection, DirectionConst.UP_RIGHT, DirectionConst.VALID_ANGLE))
			{
				return DirectionConst.UP_RIGHT;
			}
			else if (IsIncludeInDirection(targetDirection, DirectionConst.UP, DirectionConst.VALID_ANGLE))
			{
				return DirectionConst.UP;
			}
			else if (IsIncludeInDirection(targetDirection, DirectionConst.LEFT_UP, DirectionConst.VALID_ANGLE))
			{
				return DirectionConst.LEFT_UP;
			}
			else if (IsIncludeInDirection(targetDirection, DirectionConst.LEFT, DirectionConst.VALID_ANGLE))
			{
				return DirectionConst.LEFT;
			}
			else if (IsIncludeInDirection(targetDirection, DirectionConst.DOWN_LEFT, DirectionConst.VALID_ANGLE))
			{
				return DirectionConst.DOWN_LEFT;
			}
			else if (IsIncludeInDirection(targetDirection, DirectionConst.DOWN, DirectionConst.VALID_ANGLE))
			{
				return DirectionConst.DOWN;
			}
			else
			{
				return DirectionConst.RIGHT_DOWN;
			}
		}

		/// <summary>
		/// Validate that target is include in valid direction.
		/// </summary>
		/// <param name="targetDirection">target</param>
		/// <param name="direction">Valid direction</param>
		/// <param name="validAngle">Valid angle</param>
		/// <returns>Is include?</returns>
		private bool IsIncludeInDirection(double targetDirection, double direction, double validAngle)
		{
			double min = direction - validAngle;
			double max = direction + validAngle;

			// Target is bigger than Pi.
			if (targetDirection < -(Math.PI) + validAngle)
			{
				targetDirection = Math.PI + Math.PI - Math.Abs(targetDirection);
			}

			return targetDirection >= min && targetDirection < max;
		}

		/// <summary>
		/// Add beyond wall to not detected list
		/// </summary>
		/// <param name="notDetectedLocationList">Not detected list</param>
		/// <param name="x">Wall x</param>
		/// <param name="y">Wall y</param>
		/// <param name="direction">Direction</param>
		private void AddBeyondWall(IList<Position> notDetectedLocationList, int x, int y, double direction)
		{
			Position position = GetPositionFromDirection(direction);

			for (int sight = GetRange(x, y) + 1, fromWall = 1; sight <= SightRange; sight++, fromWall++)
			{
				notDetectedLocationList.Add(new Position
				{
					X = x + position.X * fromWall,
					Y = y + position.Y * fromWall
				});
			}
		}

		/// <summary>
		/// Get position from direction
		/// </summary>
		/// <param name="direction">Direction</param>
		/// <returns>Position</returns>
		private Position GetPositionFromDirection(double direction)
		{
			Position position = new Position();

			if (DirectionConst.RIGHT.Equals(direction))
			{
				position.X = 1;
				position.Y = 0;
			}
			else if (DirectionConst.UP_RIGHT.Equals(direction))
			{
				position.X = 1;
				position.Y = -1;
			}
			else if (DirectionConst.UP.Equals(direction))
			{
				position.X = 0;
				position.Y = -1;
			}
			else if (DirectionConst.LEFT_UP.Equals(direction))
			{
				position.X = -1;
				position.Y = -1;
			}
			else if (DirectionConst.LEFT.Equals(direction))
			{
				position.X = -1;
				position.Y = 0;
			}
			else if (DirectionConst.DOWN_LEFT.Equals(direction))
			{
				position.X = -1;
				position.Y = 1;
			}
			else if (DirectionConst.DOWN.Equals(direction))
			{
				position.X = 0;
				position.Y = 1;
			}
			else if (DirectionConst.RIGHT_DOWN.Equals(direction))
			{
				position.X = 1;
				position.Y = 1;
			}

			return position;
		}

		/// <summary>
		/// Remove invalid detected list using not detected list
		/// </summary>
		/// <param name="detectedLocationList">Detected list</param>
		/// <param name="notDetectedLocationList">Not detected list</param>
		private void RemoveInvalidDetectedList(IList<Position> detectedLocationList, IList<Position> notDetectedLocationList)
		{
			foreach (Position notDetected in notDetectedLocationList)
			{
				detectedLocationList.Remove(notDetected);
			}
		}
	}
}
