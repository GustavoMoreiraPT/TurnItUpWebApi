﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repository.Configuration;
using Domain.Core;
using Domain.Core.RepositoryInterfaces;
using Domain.Model.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.ImplementedRepositories
{
	//THIS IS A TEMPORARY SOLUTION AND IT SHOULD BE MOVED TO A BASE ENTITY IN REGARDS 
	public class EfRepository<T> : IRepository<T> where T : Customer
	{
		protected readonly ApplicationDbContext _appDbContext;

		public EfRepository(ApplicationDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public virtual async Task<T> GetById(int id)
		{
			return await _appDbContext.Set<T>().FindAsync(id);
		}

		public async Task<List<T>> ListAll()
		{
			return await _appDbContext.Set<T>().ToListAsync();
		}

		public async Task<T> GetSingleBySpec(ISpecification<T> spec)
		{
			var result = await List(spec);
			return result.FirstOrDefault();
		}

		public async Task<List<T>> List(ISpecification<T> spec)
		{
			// fetch a Queryable that includes all expression-based includes
			var queryableResultWithIncludes = spec.Includes
				.Aggregate(_appDbContext.Set<T>().AsQueryable(),
					(current, include) => current.Include(include));

			// modify the IQueryable to include any string-based include statements
			var secondaryResult = spec.IncludeStrings
				.Aggregate(queryableResultWithIncludes,
					(current, include) => current.Include(include));

			// return the result of the query using the specification's criteria expression
			return await secondaryResult
							.Where(spec.Criteria)
							.ToListAsync();
		}


		public async Task<T> Add(T entity)
		{
			_appDbContext.Set<T>().Add(entity);
			await _appDbContext.SaveChangesAsync();
			return entity;
		}

		public async Task Update(T entity)
		{
			_appDbContext.Entry(entity).State = EntityState.Modified;
			await _appDbContext.SaveChangesAsync();
		}

		public async Task Delete(T entity)
		{
			_appDbContext.Set<T>().Remove(entity);
			await _appDbContext.SaveChangesAsync();
		}
	}
}
