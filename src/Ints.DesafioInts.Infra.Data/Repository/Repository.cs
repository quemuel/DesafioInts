using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Ints.DesafioInts.Domain.Interfaces.Repository;
using Ints.DesafioInts.Infra.Data.Context;

namespace Ints.DesafioInts.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DesafioIntsContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository()
        {
            Db = new DesafioIntsContext();
            DbSet = Db.Set<TEntity>();
        }

        public TEntity Adicionar(TEntity obj)
        {
            var objAdd = DbSet.Add(obj);
            SaveChanges();
            return objAdd;
        }

        public TEntity Atualizar(TEntity obj)
        {
            var entry = Db.Entry(obj);
            DbSet.Attach(obj);
            //obj JA EXISTE NO BANCO E VAI SER ATUALIZADO
            entry.State = EntityState.Modified;
            SaveChanges();
            return obj;
        }

        public IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public void Dispose()
        {
            //FINALIZAR O PROCESSO MAIS RAPIDO
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public TEntity ObterPorId(Guid id)
        {
            return DbSet.Find(id);
        }

        public TEntity ObterPorId(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<TEntity> ObterTodos(int t, int s)
        {
            //PAGINACAO
            return DbSet.Skip(s).Take(t).ToList();
        }

        public IEnumerable<TEntity> ObterTodos()
        {
            return DbSet.ToList();
        }

        public void Remover(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
            SaveChanges();
        }

        public void Remover(int id)
        {
            DbSet.Remove(DbSet.Find(id));
            SaveChanges();
        }


        public int SaveChanges()
        {
            return Db.SaveChanges();
        }
    }
}