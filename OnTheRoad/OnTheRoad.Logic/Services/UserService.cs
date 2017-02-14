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

        public IUser GetUserInfo(string username)
        {
            var user = this.userRepository.GetByUserName(username);

            return user;
        }

        public void UpdateUserInfo(string username, string firstName, string lastName, string phoneNumber, string info, ICity city)
        {
            var user = this.userRepository.GetByUserName(username);
            user.FirstName = firstName;
            user.LastName = lastName;
            user.PhoneNumber = phoneNumber;
            user.Info = info;
            user.City = city;
            
            this.userRepository.Update(user);
            this.uniOfWork.Commit();
        }

        public void RemoveFavouriteUser(string username, string userToRemoveUsername)
        {
            var userId = this.userRepository.GetByUserName(username).Id;  
            this.userRepository.RemoveFavouriteUser(userId, userToRemoveUsername);

            this.uniOfWork.Commit();
        }

        public void AddFavouriteUser(string username, string userToAddUsername)
        {
            var userId = this.userRepository.GetByUserName(username).Id;
            this.userRepository.AddFavouriteUser(userId, userToAddUsername);

            this.uniOfWork.Commit();
        }

        public void UpdateImage(byte[] image, string username)
        {
            this.userRepository.UpdateImage(image, username);
            this.uniOfWork.Commit();
        }
    }
}
