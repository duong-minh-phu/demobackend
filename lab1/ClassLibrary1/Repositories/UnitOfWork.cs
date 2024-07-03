using ClassLibrary1.Interface;
using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private lab1_prn231Context _context = new lab1_prn231Context();
        private GenericRepository<Category> _CategoryRepository;
        private GenericRepository<Product> _ProductRepository;
       
        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (this._CategoryRepository == null)
                {
                    this._CategoryRepository = new GenericRepository<Category>(_context);
                }
                return this._CategoryRepository;
            }
        }
        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (this._ProductRepository == null)
                {
                    this._ProductRepository = new GenericRepository<Product>(_context);
                }
                return this._ProductRepository;
            }
        }

        

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
