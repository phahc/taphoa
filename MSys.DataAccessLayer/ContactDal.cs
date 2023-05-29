using MSys.Interface;
using MSys.Model.Data;

namespace MSys.DataAccessLayer
{
    public class ContactDal : IContact
    {
        private BooksEntities _dataContext;

        public ContactDal()
        {
            _dataContext = new BooksEntities();
        }

        public bool Register(Contact contact)
        {
            _dataContext.Contacts.Add(contact);
            _dataContext.Configuration.AutoDetectChangesEnabled = true;
            _dataContext.SaveChanges();
            return true;
        }
    }
}
