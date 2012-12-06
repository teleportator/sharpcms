using System;

namespace SharpCMS.BusinessLogic
{
	public class BusinessShell
	{
		public static void Run(Func<IOperation> operationFactory)
		{
			using (IOperation operation = operationFactory())
				operation.Execute();
		}

		public static TResult RunWithResult<TResult>(Func<IOperation<TResult>> operationFactory)
		{
			using (IOperation<TResult> operation = operationFactory())
				return operation.Execute();
		}
	}
}