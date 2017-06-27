using System.Collections.Generic;

namespace Cron
{
    class DatabaseFake : IDatabaseFake
    {
        public List<object> GetSomeEntities()
        {
            return new List<object>{new {A = "A", B = "B"}};
        }
    }
}