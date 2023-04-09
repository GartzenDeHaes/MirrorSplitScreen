using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Portland.Unity
{
	public interface INetInputSetup
	{
		void INetInputSetup_Setup(Camera camera, PlayerContext context);
	}
}
