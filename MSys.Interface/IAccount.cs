using MSys.Model.Data;
using System.Collections.Generic;

namespace MSys.Interface
{
    public interface IAccount
    {
        IEnumerable<Account> getUsers();
        Account getUsersByUserName(string username);
    }
}
