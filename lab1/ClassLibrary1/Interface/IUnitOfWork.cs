using ClassLibrary1.Models;
using ClassLibrary1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        public GenericRepository<Category> CategoryRepository { get; }
        public GenericRepository<Product> ProductRepository { get; }
        
        public void Save();
    }
}
