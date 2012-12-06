using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SharpCMS.Domain;

namespace SharpCMS.Repository.EF
{
	public interface IDao<T> where T : class
	{
		void Add(T entity);
		void Delete(T entity);
		IEnumerable<T> GetAll();
		IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
	    int Count(Expression<Func<T, bool>> predicate);
	}

	public interface IDao<T, in TK> : IDao<T> where T : class, IEntityWithKey<TK>
	{
		T GetById(TK id);
		T FirstOrDefault(Expression<Func<T, bool>> predicate);
	}
}