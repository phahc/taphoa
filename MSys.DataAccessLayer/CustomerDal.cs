using MSys.Core;
using MSys.Interface;
using MSys.Model.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace MSys.DataAccessLayer
{
    public class CustomerDal :ICustomer
    {
        private BooksEntities _dataContext;

        public CustomerDal()
        {
            _dataContext = new BooksEntities();
        }

        public IEnumerable<Customer> getCustomer()
        {
            IList<Customer> customers = new List<Customer>();
            customers = _dataContext.Customers.ToList();
            return customers.ToList();
        }

        public Customer getCustomerByUserName(string username)
        {
            Customer customers = new Customer();
            customers = _dataContext.Customers.Where(us => us.username.Equals(username))?.FirstOrDefault();
            return customers;
        }

        /// <summary>
        /// Đăng ký tài khoản khách hàng.
        /// </summary>
        /// <param name="customer">Thông tin khách hàng</param>
        /// <returns>
        /// Nếu thành công : true
        /// Nếu thất bại: false
        /// </returns>
        public string Register(Customer customer)
        {
            string ret = ConstStatus.Success;
            if (_dataContext.Customers.Any(e => e.username == customer.username))
            {
                // Exist
                ret = ConstStatus.Existed;
            }
            else
            {
                try
                {
                    //_dataContext.Entry(customer).State = EntityState.Added;
                    _dataContext.Customers.Add(customer);
                    _dataContext.Configuration.AutoDetectChangesEnabled = true;
                    _dataContext.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {

                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    //// Throw a new DbEntityValidationException with the improved exception message.
                    //throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                    ret = ConstStatus.Failed;
                }
              
            }
            
            return ret;
        }

        public bool Update(Customer customer)
        {
            var data = _dataContext.Customers.Where(p => p.userid == customer.userid).FirstOrDefault();
            data.fullname = customer.fullname;
            data.address = customer.address;
            data.email = customer.email;
            data.phone = customer.phone;
            _dataContext.Configuration.AutoDetectChangesEnabled = true;
            _dataContext.SaveChanges();
            return true;
        }
    }
}
