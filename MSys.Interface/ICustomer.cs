using MSys.Model.Data;
using System.Collections.Generic;

namespace MSys.Interface
{
    public interface ICustomer
    {
        IEnumerable<Customer> getCustomer();
        Customer getCustomerByUserName(string username);

        string Register(Customer customer);

        bool Update(Customer customer);
    }
}
