/*---------------------------------------------------------------------------
 * File: LBLed.cs
 * Utente: lucabonotto
 * Date: 05/04/2009
 *-------------------------------------------------------------------------*/

using System;

namespace HyTestRTDataService.Controls.Utils
{
	/// <summary>
	/// Mathematic Functions
	/// </summary>
	public class HTMath : Object
	{
		public static float GetRadian ( float val )
		{
			return (float)(val * Math.PI / 180);
		}
	}
}
