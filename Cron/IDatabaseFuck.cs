using System.Collections.Generic;

namespace Cron
{
    public interface IDatabaseFake
    {
        List<object> GetSomeEntities();
    }
}