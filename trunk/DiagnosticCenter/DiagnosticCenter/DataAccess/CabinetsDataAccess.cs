using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenter.Models;
using System.Data;

namespace DiagnosticCenter
{
    public class CabinetsDataAccess
    {

        public static PagedList<Cabinet> GetPagedCabinets(int skip, int take)
        {
            using (var context = new DiagnosticsDBModelContainer())
            {

                //refactor consideration...make this a pre-compiled query
                var query = context.Cabinets
                            .Where(c => c.ID_Cabinet > 0)
                            .OrderBy(c => c.Number);

                //getting this count every time in case others are adding or deleting customers
                //decide for yourself in your app
                var customerCount = query.Count();

                var customers = query.Skip(skip).Take(take).ToList();

                return new PagedList<Cabinet>
                {
                    Entities = customers,
//                                  !!!!!
                    HasNext = (skip + 10 < customerCount),
                    HasPrevious = (skip > 0)
                };
            }
        }

        public static Cabinet GetCabinetById(int id)
        {
            using (var context = new DiagnosticsDBModelContainer())
            {
                //refactor consideration...make this a pre-compiled query
                var query = context.Cabinets
                            .Where (c => c.ID_Cabinet == id);

                return query.SingleOrDefault(); //non-demo app should consider exception handling
            }
        }
    }

    public class PagedList<T>
    {
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public List<T> Entities { get; set; }
    }
}