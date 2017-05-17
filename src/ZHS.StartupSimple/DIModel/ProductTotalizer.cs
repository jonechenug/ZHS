using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZHS.StartupSimple.DIModel
{
    public class ProductTotalizer
    {
        private IRepository _repo;
        public ProductTotalizer(IRepository repo)
        {
            _repo = repo;
        }
        public  String ToString()
        {
            return _repo.ToString();
        }
    }
}
