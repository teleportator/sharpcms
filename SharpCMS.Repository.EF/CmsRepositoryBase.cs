using System;
using System.Data.Entity;
using SharpCMS.Domain;

namespace SharpCMS.Repository.EF
{
    public class CmsRepositoryBase<T, TK> : CmsDaoBase<T, CmsContext, TK>
        where T : class, IEntityWithKey<TK>
        where TK : IEquatable<TK>
    {
        public CmsRepositoryBase(CmsContext context)
            : base(context)
        {
        }

        protected override DbContext GetDbContext()
        {
            return Context;
        }
    }
}