using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walking_Dinner__asp.net_.Data
{
    public static class DbStub
    {
        public static DataTable GenerateDataTable<T>(int rows)
        {
            var datatable = new DataTable(typeof(T).Name);
            typeof(T).GetProperties().ToList().ForEach(
                x => datatable.Columns.Add(x.Name));
            Builder<T>.CreateListOfSize(rows).Build()
                .ToList().ForEach(
                    x => datatable.LoadDataRow(x.GetType().GetProperties().Select(
                        y => y.GetValue(x, null)).ToArray(), true));
            return datatable;
        }

    }
}
