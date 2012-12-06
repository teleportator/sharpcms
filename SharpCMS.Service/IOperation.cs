using System;

namespace SharpCMS.BusinessLogic
{
	public interface IOperation : IDisposable
	{
		void Execute();
	}
}