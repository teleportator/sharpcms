namespace SharpCMS.Domain
{
	public interface IEntityWithKey<T>
	{
		T Id { get; set; }
	}
}