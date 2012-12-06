using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SharpCMS.Domain;

namespace SharpCMS.Repository.EF
{
	public abstract class CmsDaoBase<T, TC> : DaoBase<TC>, IDao<T>
		where T : class
	{
		protected CmsDaoBase(TC context)
			: base(context)
		{
		}

		protected IDbSet<T> ObjectSet
		{
			get { return GetDbContext().Set<T>(); }
		}

		#region IDao<T> Members

		public void Add(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException("entity");

			ObjectSet.Add(entity);
		}

		public void Delete(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException("entity");

			ObjectSet.Remove(entity);
		}

		public IEnumerable<T> GetAll()
		{
			return ObjectSet.AsEnumerable();
		}

		public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
		{
			return ObjectSet.Where(predicate);
		}

		public int Count(Expression<Func<T, bool>> predicate)
		{
			return ObjectSet.Count(predicate);
		}

		#endregion

		protected abstract DbContext GetDbContext();
	}

	public abstract class CmsDaoBase<T, TE, TK> : CmsDaoBase<T, TE>, IDao<T, TK>
		where T : class, IEntityWithKey<TK>
		where TK : IEquatable<TK>
	{
		protected CmsDaoBase(TE context)
			: base(context)
		{
		}

		#region IDao<T,TK> Members

		public T GetById(TK id)
		{
			return ObjectSet.FirstOrDefault(e => id.Equals(e.Id));
		}

		public T FirstOrDefault(Expression<Func<T, bool>> predicate)
		{
			return ObjectSet.FirstOrDefault(predicate);
		}

		#endregion
	}
}