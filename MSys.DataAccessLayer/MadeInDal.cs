using MSys.Interface;
using MSys.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSys.DataAccessLayer
{
    public class MadeInDal : IMadeIn
    {
        private BooksEntities _dataContext;

        public MadeInDal()
        {
            _dataContext = new BooksEntities();
        }

        public string GetMadeInNm(long id)
        {
            return _dataContext.MadeIns.Where(m => m.id == id).Select(m => m.name).ToString();
        }
    }
}
