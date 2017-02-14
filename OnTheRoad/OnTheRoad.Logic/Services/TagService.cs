using OnTheRoad.Logic.Contracts;
using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Factories;

namespace OnTheRoad.Logic.Services
{
    public class TagService : ITagService
    {
        private readonly ITagDataUtil tagDataUtil;
        private readonly ITagFactory tagFactory;

        public TagService(ITagDataUtil tagDataUtil, ITagFactory tagFactory)
        {
            if (tagDataUtil == null)
            {
                throw new ArgumentNullException("tagDataUtil can not be null!");
            }

            if (tagFactory == null)
            {
                throw new ArgumentNullException("tagFactory can not be null!");
            }

            this.tagDataUtil = tagDataUtil;
            this.tagFactory = tagFactory;
        }

        public IEnumerable<ITag> GetOrCreateTags(IEnumerable<string> tagNames)
        {
            var result = new List<ITag>();
            foreach (var tagName in tagNames)
            {
                var tag = this.tagDataUtil.GetTagByName(tagName);
                if (tag == null)
                {
                    var tagModel = this.tagFactory.CreateTag(tagName); 
                    this.tagDataUtil.AddTag(tagModel);

                    tag = this.tagDataUtil.GetTagByName(tagModel.Name);
                }

                result.Add(tag);
            }

            return result;
        }

        public IEnumerable<ITag> GetTagsByNamePrefix(string prefix, int take)
        {
            var tags = this.tagDataUtil.GetTagsByNamePrefix(prefix, take);

            return tags;
        }
    }
}
