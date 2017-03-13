using NPoco;
using NPoco.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ZHS.NPOCO
{
    public static class DbHelper
    {
        public static TEntity FindByProperties<TEntity>(this IDatabase db, Expression<Func<TEntity, bool>> where)
        {
            var query = db.Query<TEntity>().Where(where).Limit(1);
            return query.ToList().FirstOrDefault();
        }

        public static Object Insert<TEntity>(this IDatabase db, TEntity entity)
        {
            return db.Insert<TEntity>(entity);
        }

        public static IEnumerable<TEntity> QueryList<TEntity>(this IDatabase db, Expression<Func<TEntity, bool>> where)
        {
            var query = db.Query<TEntity>().Where(where);
            return query.ToList();
        }

        public static IQueryProvider<TEntity> QueryPagedProvider<TEntity>(this IDatabase db, Expression<Func<TEntity, bool>> where,
           int index = 1,
           int size = 10)
        {
            return  db.Query<TEntity>().Where(where).Limit((index - 1) * size, size);
        }

        public static IEnumerable<TEntity> QueryPaged<TEntity> (this IDatabase db, IQueryProvider<TEntity> provider)
        {
            return provider.ToList();
        }

        public static Int32 Delete<TEntity>(this IDatabase db,
            Expression<Func<TEntity, bool>> where)
        {
            return db.DeleteMany<TEntity>().Where(where).Execute();
        }

        public static Int32 Modify<TEntity>(this IDatabase db, TEntity entity,
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, object>> onlyFields)
        {
            return db.UpdateMany<TEntity>().Where(where).OnlyFields(onlyFields).Execute(entity);
        }

        public static Int32 GetTotalCount<TEntity>(this IDatabase db, Expression<Func<TEntity, bool>> where)
        {
            return db.Query<TEntity>().Where(where).Count();
        }


    }
}
