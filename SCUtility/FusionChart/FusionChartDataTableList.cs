using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCUtility.FusionChart
{
    public class FusionChartDataTableList
    {
        public IList<DataTable> GetChartDataTableList(DataTable dt, IList<ChartDataTable> cdtList)
        {

            IList<DataTable> dtlist = new List<DataTable>();

            foreach (var cdt in cdtList)
            {
                DataTable newDT = new DataTable();
                newDT.TableName = cdt.Name;
                foreach (var column in cdt.Columnlist)
                {

                    newDT.Columns.Add(column);
                    if (column == "CPC" || column == "CPM" || column == "CPA" || column == "Cost")
                    {
                        newDT.Columns[column].DataType = System.Type.GetType("System.Decimal");

                    }
                }
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dr1 = newDT.NewRow();
                    foreach (var column in cdt.Columnlist)
                    {
                        if (column == "")
                        {



                            dr1["Column1"] = 1;
                        }
                        else
                        {



                            dr1[column] = dr[column];
                        }
                    }
                    newDT.Rows.Add(dr1);
                }
                dtlist.Add(newDT);
            }
            return dtlist;
        }
    }
}
