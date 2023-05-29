using MSys.Interface;
using MSys.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace MSys.DataAccessLayer
{
    public class AccountDal: IAccount
    {
        private BooksEntities _dataContext;

        public AccountDal()
        {
            _dataContext = new BooksEntities();
        }

        public IEnumerable<Account> getUsers()
        {
            IList<Account> users = new List<Account>();
            users = _dataContext.Accounts.ToList();
            return users.ToList();
        }

        public Account getUsersByUserName(string username)
        {
            Account users = new Account();
            users = _dataContext.Accounts.Where(us => us.acc.Equals(username))?.FirstOrDefault();
            return users;
        }
    }
}
