using AutoMapper;
using JinjiProject.BusinessLayer.Constants;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Enums;
using JinjiProject.Core.Utilities.Results.Concrete;
using JinjiProject.DataAccess.EFCore.Repositories;
using JinjiProject.DataAccess.Interface.Repositories;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Products;
using JinjiProject.Dtos.Subscribers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JinjiProject.BusinessLayer.Managers.Concrete
{
    public class SubscriberManager : ISubscriberService
    {
        private readonly ISubscriberRepository subscriberRepository;
        private readonly IMapper mapper;

        public SubscriberManager(ISubscriberRepository subscriberRepository,IMapper mapper)
        {
            this.subscriberRepository = subscriberRepository;
            this.mapper = mapper;
        }

        public async Task<DataResult<Subscriber>> CreateSubscriberAsync(CreateSubscriberDto createSubscriberDto)
        {
            if (createSubscriberDto == null)
            {
                return new ErrorDataResult<Subscriber>(Messages.CreateSubscriberError);
            }
            else
            {
                Subscriber subscriber = mapper.Map<Subscriber>(createSubscriberDto);
                bool result = await subscriberRepository.Create(subscriber);
                if (result)
                    return new SuccessDataResult<Subscriber>(subscriber, Messages.CreateSubscriberSuccess);
                else
                    return new ErrorDataResult<Subscriber>(subscriber, Messages.CreateSubscriberRepoError);

            }
        }

        public async Task<DataResult<List<ListSubscriberDto>>> GetAllSubscriber()
        {
            var subscribers = await subscriberRepository.GetAllAsync();
            return new SuccessDataResult<List<ListSubscriberDto>>(mapper.Map<List<ListSubscriberDto>>(subscribers), Messages.SubscriberListedSuccess);
        }

        public async Task<DataResult<GetSubscriberDto>> GetSubscriberById(int id)
        {
            if (id <= 0)
                return new ErrorDataResult<GetSubscriberDto>(Messages.BrandNotFound);
            else
            {
                GetSubscriberDto getSubscriberDto = mapper.Map<GetSubscriberDto>(await subscriberRepository.GetByIdAsync(id));
                return new SuccessDataResult<GetSubscriberDto>(getSubscriberDto, Messages.SubscriberFoundSuccess);
            }
        }

        public async Task<DataResult<Subscriber>> HardDeleteSubscriberAsync(int id)
        {
            var subscriberDto = await subscriberRepository.GetByIdAsync(id);
            if (subscriberDto == null)
            {
                return new ErrorDataResult<Subscriber>(Messages.SubscriberNotFound);
            }
            else
            {
                bool result = await subscriberRepository.HardDelete(subscriberDto);
                if (result)
                    return new SuccessDataResult<Subscriber>(Messages.SubscriberDeletedSuccess);
                else
                    return new ErrorDataResult<Subscriber>(Messages.SubscriberDeletedRepoError);
            }
        }

        public async Task<DataResult<Subscriber>> SoftDeleteSubscriberAsync(int id)
        {
            var subscriberDto = await subscriberRepository.GetByIdAsync(id);
            if (subscriberDto == null)
            {
                return new ErrorDataResult<Subscriber>(Messages.SubscriberNotFound);
            }
            else
            {
                bool result = await subscriberRepository.SoftDelete(subscriberDto);
                if (result)
                    return new SuccessDataResult<Subscriber>(Messages.SubscriberDeletedSuccess);
                else
                    return new ErrorDataResult<Subscriber>(Messages.SubscriberDeletedRepoError);
            }
        }

        public async Task<DataResult<List<ListSubscriberDto>>> GetAllByExpression(Expression<Func<Subscriber, bool>> expression)
        {
            var subscribers = await subscriberRepository.GetAllByExpression(expression);
            if(subscribers.Count() <= 0)
                return new ErrorDataResult<List<ListSubscriberDto>>(mapper.Map<List<ListSubscriberDto>>(subscribers), Messages.SubscriberListedError);
            else
                return new SuccessDataResult<List<ListSubscriberDto>>(mapper.Map<List<ListSubscriberDto>>(subscribers), Messages.SubscriberListedSuccess);
        }

        public async Task<DataResult<Subscriber>> UpdateSubscriberAsync(UpdateSubscriberDto updateSubscriberDto)
        {
            if (updateSubscriberDto == null)
            {
                return new ErrorDataResult<Subscriber>(Messages.UpdateBrandError);
            }
            else
            {
                Subscriber subscriber = await subscriberRepository.GetByIdAsync(updateSubscriberDto.Id);
                mapper.Map(updateSubscriberDto, subscriber);
                bool result = await subscriberRepository.Update(subscriber);
                if (result)
                    return new SuccessDataResult<Subscriber>(subscriber, Messages.UpdateSubscriberSuccess);
                else
                    return new ErrorDataResult<Subscriber>(subscriber, Messages.UpdateSubscriberRepoError);
            }
        }

        public async Task<DataResult<List<ListSubscriberDto>>> GetSubscribersBySearchValues(string? fullName, string? email, string? createdDate)
        {
            int nullParamCount = new[] { fullName, email }.Count(param => param != null);

            if (DateTime.Parse(createdDate).Year.ToString() != "1")
            {
                nullParamCount++;
            }

            var subscribersByName = await subscriberRepository.GetAllByExpression(subscriber => subscriber.Status != Status.Deleted && subscriber.FullName.Contains(fullName));

            var subscribersByEmail = await subscriberRepository.GetAllByExpression(subscriber => subscriber.Status != Status.Deleted && subscriber.Email.Contains(email));

            var subscribersByCreatedYear = await subscriberRepository.GetAllByExpression(subscriber => subscriber.Status != Status.Deleted && EF.Functions.DateDiffDay(subscriber.CreatedDate, DateTime.Parse(createdDate)) == 0);

            var filteredSubsricers = IntersectNonEmpty(nullParamCount, subscribersByName, subscribersByEmail, subscribersByCreatedYear);

            return filteredSubsricers.Any()
            ? new SuccessDataResult<List<ListSubscriberDto>>(mapper.Map<List<ListSubscriberDto>>(filteredSubsricers), Messages.ProductListedSuccess)
            : new ErrorDataResult<List<ListSubscriberDto>>(Messages.ProductNotFound);
        }

        private static IEnumerable<T> IntersectNonEmpty<T>(int _nullParamCount, params IEnumerable<T>[] lists)
        {
            var nonEmptyLists = lists.Where(list => list != null && list.Any()).ToList();

            if (nonEmptyLists.Count == 0)
            {
                return new List<T>();
            }

            IEnumerable<T> result = nonEmptyLists[0];
            if (_nullParamCount == nonEmptyLists.Count)
            {
                for (int i = 1; i < nonEmptyLists.Count; i++)
                {
                    result = result.Intersect(nonEmptyLists[i]).ToList();

                    if (!result.Any())
                    {
                        break;
                    }
                }
                return result;
            }
            else
            {

                return result = new List<T>();
            }
        }
    }
}
