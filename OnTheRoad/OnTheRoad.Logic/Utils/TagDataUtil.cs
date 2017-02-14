using OnTheRoad.Logic.Contracts;
using System;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Domain.Contracts;
using System.Collections.Generic;

namespace OnTheRoad.Logic.Utils
{
    public class TagDataUtil : ITagDataUtil
    {
        private readonly ITagRepository tagRepository;
        private readonly IUnitOfWork unitOfWork;

        public TagDataUtil(ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            if (tagRepository == null)
            {
                throw new ArgumentNullException("tagRepository can not be null!");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork can not be null!");
            }

            this.tagRepository = tagRepository;
            this.unitOfWork = unitOfWork;
        }

        public void AddTag(ITag tag)
        {
            this.tagRepository.Add(tag);
            this.unitOfWork.Commit();
        }

        public ITag GetTagByName(string name)
        {
            var entity = this.tagRepository.GetTagByName(name);

            return entity;
        }

        public IEnumerable<ITag> GetTagsByNamePrefix(string prefix, int take)
        {
            var tags = this.tagRepository.GetTagsByNamePrefix(prefix, take);

            return tags;
        }
    }
}
