using System.Collections.Generic;
using UserModel;

namespace BookStoreManager.Interface
{
    public interface IFeedBackManager
    {
        string AddFeedBack(FeedBackModel feedBack);
        IEnumerable<FeedBackModel> GetFeedBackList(int BookId);
    }
}