using System;
using System.Collections.Generic;
using Top2dGame.Client.Master;
using Top2dGame.Client.Scripts.Base;

namespace Top2dGame.Client.GameObjects.Base
{
	public abstract class GameObject
	{
		/// <summary>
		/// Current map name
		/// </summary>
		public string MapName { get; set; }
		/// <summary>
		/// Location X
		/// </summary>
		public int X { get; set; }
		/// <summary>
		/// Location Y
		/// </summary>
		public int Y { get; set; }

		/// <summary>
		/// Identify tag
		/// </summary>
		public long Tag { get; set; }

		/// <summary>
		/// Game object name (displayed on screen)
		/// </summary>
		public virtual string Name { get; }

		/// <summary>
		/// Sprite displayed on the screen (Use SpriteEnum or specified text)
		/// </summary>
		public virtual IList<string> Sprite { get; }

		/// <summary>
		/// Scripts
		/// </summary>
		public IList<GameScript> Scripts { get; set; }

		protected GameObject(string currentMapName)
		{
			MapName = currentMapName;
			Scripts = new List<GameScript>();
			AddScript();
		}

		/// <summary>
		/// Create game object
		/// </summary>
		public void Create()
		{
			GameMaster.GetInstance().AddGameObject(this, MapName);
		}

		/// <summary>
		/// Destroy game object
		/// </summary>
		public void Destroy()
		{
			GameMaster.GetInstance().RemoveGameObject(this, MapName);
		}

		/// <summary>
		/// Get bool value that game object has specified tag.
		/// </summary>
		/// <param name="tag">Tag enum</param>
		/// <returns>Has tag?</returns>
		public bool HasTag(int tag)
		{
			// No tag
			if (Tag == 0)
			{
				return false;
			}

			long shiftedValue = Tag >> tag - 1;

			return shiftedValue % 2 == 1;
		}

		/// <summary>
		/// Set tag
		/// </summary>
		/// <param name="tag">Tag to set</param>
		/// <param name="enabled">Enabled bool value</param>
		public void SetTag(int tag, bool enabled)
		{
			int value = 1 << tag - 1;

			if (HasTag(tag))
			{
				// Remove tag
				if (!enabled)
				{
					Tag -= value;
				}
			}
			else
			{
				// Add tag
				if (enabled)
				{
					Tag += value;
				}
			}
		}

		/// <summary>
		/// Add script
		/// </summary>
		protected abstract void AddScript();

		/// <summary>
		/// Get script
		/// </summary>
		/// <param name="findScriptType">Find script type</param>
		/// <returns>Script</returns>
		public GameScript GetScript(Type findScriptType)
		{
			foreach (GameScript script in Scripts)
			{
				if (script.GetType() == findScriptType)
				{
					return script;
				}
			}

			return null;
		}
	}
}
