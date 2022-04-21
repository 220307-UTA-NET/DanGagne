using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectZero;
using ProjectZero.Classes;

namespace ProjectZero.Database
{
    public interface IRepo
    {
        void AddNewCustomer(string fname, string lname);
    }
}
