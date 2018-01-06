using Lazeez.Repository.Infrastructure;
using Lazeez.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace Lazeez.Repository.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private EFContext _context;
        private Dictionary<string, object> _repositories;
        private Dictionary<string, object> _services;

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        public UnitOfWork()
        {
            _context = new EFContext();
        }

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="context">The object context</param>
        public UnitOfWork(EFContext context)
        {
            _context = context;
        }
        
        public S Service<S>() where S : class
        {
            if (_services == null)
            {
                _services = new Dictionary<string, object>();
            }

            var type = typeof(S).Name;

            if (!_services.ContainsKey(type))
            {
                var repositoryInstance = Activator.CreateInstance(typeof(S), this);
                _services.Add(type, repositoryInstance);
            }
            return (S)_services[type];
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (IRepository<T>)_repositories[type];
        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public int Save()
        {
            try
            {
                // Save changes with the default options
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (DbEntityEntry entry in ex.Entries)
                {
                    // Update the values of the entity that failed to save from the store 
                    entry.Reload();
                }
                return Save();
            }
        }
    }
}