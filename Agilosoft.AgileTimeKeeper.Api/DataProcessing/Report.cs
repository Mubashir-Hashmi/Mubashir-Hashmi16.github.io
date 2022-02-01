using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agilosoft.AgileTimeKeeper.Api.Util;
using Agilosoft.AgileTimeKeeper.Api.Model;
using System.Data;

namespace Agilosoft.AgileTimeKeeper.Api.DataProcessing
{
    public static class Report
    {
        public static List<Parent> generateSummaryList(DataTable dataTable, String[] order)
        {
            List<Parent> reportList = new List<Parent>();
            foreach (DataRow row in dataTable.Rows)
            {

                Parent p = new Parent();
                Child c = new Child();
                GrandChild gc = new GrandChild();
                p.name = row[order[0]].ToString();
                if (order.Length >= 2)
                {
                    try
                    {
                        c.name = row[order[1]].ToString();
                    }
                    catch (ArgumentException e)
                    {
                        c.name = "";
                    }
                }
                else
                {
                    c.name = "";
                }
                if (order.Length == 3)
                {
                    try
                    {
                        gc.name = row[order[2]].ToString();
                    }
                    catch (ArgumentException e)
                    {
                        gc.name = "";
                    }
                }
                else
                {
                    gc.name = "";
                }
                gc.time = Utils.convertTimeToNum(Double.Parse(row["TotalTime"].ToString()));
                Parent pp = reportList.Find(parent => parent.name.Equals(p.name));
                if (pp != null)
                {
                    Child cc = pp.childSeries.Find(child => child.name.Equals(c.name));
                    if (cc != null)
                    {
                        GrandChild gg = cc.grandChildSeries.Find(grandChild => grandChild.name.Equals(gc.name));
                        if (gg != null)
                        {
                            gg.time += gc.time;
                            cc.time += gc.time;
                            pp.totalTime += gc.time;
                        }
                        else
                        {
                            cc.grandChildSeries.Add(gc);
                            cc.time += gc.time;
                            pp.totalTime += gc.time;
                        }

                    }
                    else
                    {
                        c.grandChildSeries.Add(gc);
                        c.time += gc.time;
                        pp.childSeries.Add(c);
                        pp.totalTime += gc.time;
                    }

                }
                else
                {
                    c.grandChildSeries.Add(gc);
                    c.time = gc.time;
                    p.childSeries.Add(c);
                    p.totalTime += gc.time;
                    reportList.Add(p);
                }

            }
            return reportList;
        }

        public static List<Parent> generateWeeklyList(DataTable dataTable, String[] order, String startDate, String endDate)
        {
            List<Parent> reportList = new List<Parent>();

            List<String> dates = _generateDates(startDate, endDate);
            foreach (DataRow row in dataTable.Rows)
            {

                Parent p = new Parent();
                Child c = new Child();
                GrandChild gc = new GrandChild();

                p.name = row[order[0]].ToString();
                c.name = row[order[1]].ToString();
                gc.name = row[order[2]].ToString();

                gc.time = Utils.convertTimeToNum(Double.Parse(row["TotalTime"].ToString()));
                Parent pp = reportList.Find(parent => parent.name.Equals(p.name));
                if (pp != null)
                {
                    Child cc = pp.childSeries.Find(child => child.name.Equals(c.name));
                    if (cc != null)
                    {
                        GrandChild gg = cc.grandChildSeries.Find(grandChild => grandChild.name.Equals(gc.name));
                        if (gg != null)
                        {
                            gg.time += gc.time;
                            cc.time += gc.time;
                            pp.totalTime += gc.time;
                        }
                        else
                        {
                            cc.grandChildSeries.Add(gc);
                            cc.time += gc.time;
                            pp.totalTime += gc.time;
                        }

                    }
                    else
                    {
                        c.grandChildSeries.Add(gc);
                        c.time += gc.time;
                        pp.childSeries.Add(c);
                        pp.totalTime += gc.time;
                    }

                }
                else
                {
                    c.grandChildSeries.Add(gc);
                    c.time = gc.time;
                    p.childSeries.Add(c);
                    p.totalTime += gc.time;
                    reportList.Add(p);
                }

            }
            foreach(Parent p in reportList)
            {
                foreach(Child c in p.childSeries)
                {
                    foreach(String date in dates)
                    {
                        GrandChild gc = c.grandChildSeries.Find(grandChild => Convert.ToDateTime(grandChild.name) == Convert.ToDateTime(date));
                        if (gc == null)
                        {
                            GrandChild gg = new GrandChild();
                            gg.name = date;
                            gg.time = 0;
                            c.grandChildSeries.Add(gg);
                        }
                    }
                    c.grandChildSeries = c.grandChildSeries.OrderBy(o => Convert.ToDateTime(o.name)).ToList();
                }
            }
            return reportList;
        }

        
        private static List<String> _generateDates(String startDate, String endDate)
        {
            DateTime curr;
            List<String> dates = new List<string>();
            DateTime dateObj;
            int month;
            int date;
            int year;

            curr = Convert.ToDateTime(startDate);
            //dateObj = new Date(curr);
            month = curr.Month;
            date = curr.Day;
            year = curr.Year;
            dates.Add(year.ToString() + "-" + month.ToString() + "-" + date.ToString());
            for (var i = 1; i < 7; i++)
            {
                curr = curr.AddDays(1);
                month = curr.Month;
                date = curr.Day;
                year = curr.Year;
                dates.Add(year.ToString() + "-" + month.ToString() + "-" + date.ToString());
            }
            return dates;
        }

    }
}
