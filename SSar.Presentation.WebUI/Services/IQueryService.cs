using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSar.Presentation.WebUI.Services
{
    public interface IQueryService
    { 
        Task<T> Query<T>(string sqlQuery, object param);
        Task<List<T>> ListQuery<T>(string sqlQuery);
    }
}
