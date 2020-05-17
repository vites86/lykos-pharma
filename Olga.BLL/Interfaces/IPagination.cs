using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olga.BLL.Interfaces
{
    public interface IPagination<T> where T:class
    {
        IEnumerable<T> GetPaginated(int? countryId, string searchValue, string sortOrder, int initialPage, int pageSize, out int totalRecords, out int recordsFiltered);
    }
}
