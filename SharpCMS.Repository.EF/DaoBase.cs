namespace SharpCMS.Repository.EF
{
    public class DaoBase<T>
    {
        public DaoBase(T context)
        {
            Context = context;
        }

        protected T Context { get; private set; }
    }
}