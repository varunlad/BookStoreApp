using System.Collections.Generic;
using UserModel;

namespace BookstoreRepository.Interface
{
    public interface IFeedBackRepo
    {
        string connectionString { get; set; }

        string AddFeedback(FeedBackModel feedBackModel);
        IEnumerable<FeedBackModel> GetFeedBackList(int BookId);
    }
}