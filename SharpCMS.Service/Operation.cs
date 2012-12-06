using SharpCMS.Repository.EF;

namespace SharpCMS.BusinessLogic
{
	public abstract class Operation : BusinessClass, IOperation
	{
		protected Operation()
		{
		}

		protected Operation(IRepository repository) : base(repository)
		{
		}

		#region IOperation Members

		public void Execute()
		{
			Perform();
		}

		#endregion

		protected abstract void Perform();
	}
}