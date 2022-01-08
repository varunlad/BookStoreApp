using BookStoreManager.Interface;
using BookstoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using UserModel;

namespace BookStoreManager.Manager
{
    public class FeedBackManager : IFeedBackManager
    {
        private readonly IFeedBackRepo repository;
        public FeedBackManager(IFeedBackRepo repository)
        {
            this.repository = repository;
        }
        public string AddFeedBack(FeedBackModel feedBack)
        {
            try
            {
                return this.repository.AddFeedback(feedBack);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<FeedBackModel> GetFeedBackList(int BookId)
        {
            try
            {
                return this.repository.GetFeedBackList(BookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
