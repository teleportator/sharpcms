using SharpCMS.Repository.EF;

namespace SharpCMS.BusinessLogic
{
	public abstract class Operation<TResult> : Operation, IOperation<TResult>
	{
		private TResult _operationResult;

		protected Operation()
		{
		}

		protected Operation(IRepository repository) : base(repository)
		{
		}

		#region IOperation<TResult> Members

		public new TResult Execute()
		{
			base.Execute();
			return _operationResult;
		}

		#endregion

		protected override void Perform()
		{
			_operationResult = PerformWithResult();
		}

		protected abstract TResult PerformWithResult();
	}
}