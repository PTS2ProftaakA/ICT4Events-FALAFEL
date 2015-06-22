using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Events.Database
{
    interface IDatabase
    {
        void Aanpassen(Database database);

        void Verwijderen(Database database);
    }
}
