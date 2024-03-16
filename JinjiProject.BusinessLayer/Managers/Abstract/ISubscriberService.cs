using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Subscribers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Managers.Abstract
{
    public interface ISubscriberService
    {
        public Task<DataResult<Subscriber>> CreateSubscriberAsync(CreateSubscriberDto createSubscriberDto);
        public Task<DataResult<Subscriber>> UpdateSubscriberAsync(UpdateSubscriberDto updateSubscriberDto);
        public Task<DataResult<Subscriber>> SoftDeleteSubscriberAsync(int id);
        public Task<DataResult<Subscriber>> HardDeleteSubscriberAsync(int id);
        public Task<DataResult<List<ListSubscriberDto>>> GetAllByExpression(Expression<Func<Subscriber, bool>> expression);
        public Task<DataResult<List<ListSubscriberDto>>> GetAllSubscriber();
        public Task<DataResult<GetSubscriberDto>> GetSubscriberById(int id);
    }
}
