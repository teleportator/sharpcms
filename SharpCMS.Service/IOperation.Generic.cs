using System;

namespace SharpCMS.BusinessLogic
{
	public interface IOperation<out TResult> : IDisposable
	{
		TResult Execute();
	}
}