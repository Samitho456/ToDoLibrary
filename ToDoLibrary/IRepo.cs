using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoLibrary
{
    public interface IRepo<T>
    {
        void Add(T obj);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllSorted(string sortBy, bool decending = false);
        T GetById(int id);
        T Remove(int id);
        T GetByTitle(string title);
        T Update(int id, T obj);

    }
}
