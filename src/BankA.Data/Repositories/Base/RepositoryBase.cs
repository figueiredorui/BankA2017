using BankA.Data.Contexts;
using BankA.Data.Models;
using RefactorThis.GraphDiff;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> where TEntity : class, new()
    {
        BankAContext ctx = new BankAContext();
        private IDbSet<TEntity> _entities;
        public IQueryable<TEntity> Table
        {
            get
            {
                if (_entities == null)
                    _entities = ctx.Set<TEntity>();
                return _entities;
            }
        }

        //public TEntity Get(Expression<Func<TEntity, bool>> where)
        //{
        //    TEntity entity = default(TEntity);
        //    using (var ctx = new BankAContext())
        //    {
        //        entity = ctx.Set<TEntity>().Where(where).FirstOrDefault();
        //    }

        //    return entity;
        //}

        //public TEntity Get(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    TEntity entity = default(TEntity);
        //    using (var ctx = new BankAContext())
        //    {
        //        var entities = ctx.Set<TEntity>().Where(where);

        //        if (includes != null)
        //        {
        //            entities = ApplyIncludesToQuery<TEntity>(entities, includes);
        //        }

        //        entity = entities.FirstOrDefault();

        //    }

        //    return entity;
        //}

        //public List<TEntity> GetList()
        //{
        //    List<TEntity> entity = new List<TEntity>();
        //    using (var ctx = new BankAContext())
        //    {
        //        entity = ctx.Set<TEntity>().ToList();
        //    }

        //    return entity;
        //}

        //public List<TEntity> GetList(Expression<Func<TEntity, bool>> where)
        //{
        //    List<TEntity> entities = null;
        //    using (var ctx = new BankAContext())
        //    {
        //        entities = ctx.Set<TEntity>().Where(where).ToList();
        //    }
        //    return entities;
        //}

        //public List<TEntity> GetList(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    List<TEntity> entities = null;
        //    using (var ctx = new BankAContext())
        //    {
        //        var result = ctx.Set<TEntity>().Where(where);

        //        if (includes != null)
        //        {
        //            result = ApplyIncludesToQuery<TEntity>(result, includes);
        //        }

        //        entities = result.ToList();
        //    }

        //    return entities;
        //}

        public TEntity Find(params object[] keyValues)
        {
            TEntity entity = default(TEntity);
            using (var ctx = new BankAContext())
            {
                entity = ctx.Set<TEntity>().Find(keyValues);
            }

            return entity;
        }

        public TEntity Add(TEntity entity)
        {
             using (var ctx = new BankAContext())
            {
                ctx.Set<TEntity>().Add(entity);
                ctx.SaveChanges();
            }

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            using (var ctx = new BankAContext())
            {
                ctx.Set<TEntity>().Attach(entity);
               // SetOriginalRowVersion(ctx, entity);
                ctx.Entry<TEntity>(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }

            return entity;
        }

        private static void SetOriginalRowVersion(BankAContext ctx, dynamic entity)
        {
            entity.RowVersion = ctx.Entry(entity).Property("RowVersion").OriginalValue;
        }

        //public TEntity Update(TEntity entity)
        //{
        //    using (var ctx = new BankAContext())
        //    {
        //        ctx.UpdateGraph<TEntity>(entity);

        //        ctx.SaveChanges();
        //    }

        //    return entity;
        //}

        //public TEntity Update<T2>(TEntity entity, Expression<Func<TEntity, System.Collections.Generic.ICollection<T2>>> details)
        //{
        //    Expression<Func<IUpdateConfiguration<TEntity>, object>> template = _ => _.OwnedCollection(details);
        //    var map = Expression.Parameter(typeof(IUpdateConfiguration<TEntity>), "map");
        //    var graphDetails = Expression.Lambda<Func<IUpdateConfiguration<TEntity>, object>>(Expression.Call(((MethodCallExpression)template.Body).Method, map, Expression.Quote(details)), map);

        //    using (var ctx = new BankAContext())
        //    {
        //        ctx.UpdateGraph<TEntity>(entity, graphDetails);
        //        ctx.SaveChanges();
        //    }

        //    return entity;
        //}

        public void Delete(TEntity entity)
        {
            using (var ctx = new BankAContext())
            {
                ctx.Set<TEntity>().Attach(entity);
                ctx.Set<TEntity>().Remove(entity);
                ctx.SaveChanges();
            }

        }

        public List<TEntity2> ExecuteSqlQuery<TEntity2>(string sql, params object[] parameters)
        {
            using (var ctx = new BankAContext())
            {
                return ctx.Database.SqlQuery<TEntity2>(sql, parameters).ToList();

                
            }
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            using (var ctx = new BankAContext())
            {
                return ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        private IQueryable<R> ApplyIncludesToQuery<R>(IQueryable<R> entities, Expression<Func<R, object>>[] includes) where R : class
        {
            if (includes != null)
                entities = includes.Aggregate(entities, (current, include) => current.Include(include));

            return entities;
        }
    }
}
