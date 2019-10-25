/*---------------------------------------------------------------------------
 * File: Renderer.cs
 * Utente: lucabonotto
 * Date: 05/04/2009
 *-------------------------------------------------------------------------*/

using System;
using System.Drawing;

namespace HyTestRTDataService.Controls.Base
{
	/// <summary>
	/// Renderer interface for all
    /// LBSoft.IndustrialCtrls renderer
	/// </summary>
    public interface IHTRenderer : IDisposable
	{
		object Control
		{
			set;
			get;
		}
		bool Update	();
		void Draw	( Graphics Gr );
	}
}
