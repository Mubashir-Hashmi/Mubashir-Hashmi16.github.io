using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Model
{
    public class Parent
    {
        public String name
        {
            get;
            set;
        } = "";
        public Double totalTime
        {
            get;
            set;
        } = 0.00;

        public List<Child> childSeries = new List<Child>();
    }

    public class Child
    {
        public String name
        {
            get;
            set;
        } = "";
        public Double time
        {
            get;
            set;
        } = 0.00;

        public List<GrandChild> grandChildSeries = new List<GrandChild>();
    }

    public class GrandChild
    {
        public String name
        {
            get;
            set;
        } = "";
        public Double time
        {
            get;
            set;
        } = 0.00;
    }
}
