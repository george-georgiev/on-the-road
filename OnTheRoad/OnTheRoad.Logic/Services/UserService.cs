using System;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Contracts;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Logic.Contracts;

namespace OnTheRoad.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uniOfWork;
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository, IUnitOfWork uniOfWork)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("repository can not be null!");
            }

            if (uniOfWork == null)
            {
                throw new ArgumentNullException("uniOfWork can not be null!");
            }

            this.userRepository = userRepository;
            this.uniOfWork = uniOfWork;
        }

        public IUser GetUserInfo(string userName)
        {
            var user = this.userRepository.GetByUserName(userName);

            return user;
        }

        public void UpdateUserInfo()
        {
            throw new NotImplementedException();
        }
    }
}
