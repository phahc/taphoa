using MSys.Model.Data;
using System.Collections.Generic;
namespace MSys.Interface
{
    public interface ITopics
    {
        List<Topic> GetMainTopicInfo(int pos);
    }
}
