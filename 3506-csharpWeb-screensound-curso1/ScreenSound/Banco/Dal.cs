using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco
{
    public class Dal<T> where T : class
    {
        protected readonly DbContextBase _dbContext;

        public Dal(DbContextBase db)
        {
            this._dbContext = db;
        }

        public IEnumerable<T> Listar()
        {
            return _dbContext.Set<T>().ToList();
        }

        public void Adicionar(T objeto)
        {
            _dbContext.Set<T>().Add(objeto);
            _dbContext.SaveChanges();
        }

        public void Deletar(T objeto)
        {
            _dbContext.Set<T>().Remove(objeto);
            _dbContext.SaveChanges();
        }

        
        public void Atualizar(T objeto)
        {
            _dbContext.Set<T>().Update(objeto);
            _dbContext.SaveChanges();
        }

        public T? RecuperarPor(Func<T, bool> condicao)
        {
            return _dbContext.Set<T>().FirstOrDefault(condicao);

        }

    }
}
