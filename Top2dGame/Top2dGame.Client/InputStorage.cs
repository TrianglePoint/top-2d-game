using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top2dGame.InputMaster
{
	// TODO Maybe, it is not used
    public class InputStorage
    {
		private bool Up { get; set; }

		private bool Down { get; set; }

		private bool Right { get; set; }

		private bool Left { get; set; }

		private IList<object> InputList { get; set; }
		private int MaxLength { get; set; }

		public InputStorage(int maxLength)
		{
			InputList = new List<object>();
			MaxLength = maxLength;
		}

		public void KeyDown(object keyDown)
		{
			if (InputList.Count < MaxLength)
			{
				InputList.Add(keyDown);
			}
		}

		public void KeyUp(object keyUp)
		{
			InputList.Remove(keyUp);
		}
    }
}
