using MSys.Interface;
using MSys.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace MSys.DataAccessLayer
{
    public class TopicsDal :ITopics
    {
        private BooksEntities _dataContext;

        public TopicsDal()
        {
            _dataContext = new BooksEntities();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Topic> GetMainTopicInfo(int pos)
        {
            return _dataContext.Topics.Where(t => t.showmain == pos).OrderBy(t => t.id).ToList();
        }
    }
}
