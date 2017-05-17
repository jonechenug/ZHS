using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZHS.StartupSimple.DIModel
{
    public class MemoryRepository:IRepository
    {
        private String guid = System.Guid.NewGuid().ToString();
        public override String ToString()
        {
            return guid;
        }
    }
}
