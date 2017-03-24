using System;
using System.Linq;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Contracts;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Logic.Contracts;

namespace OnTheRoad.Logic.Services
{
    public class UserService : IUserService, IUpdateUserService, IUserGetService
    {
        private readonly IUnitOfWork uniOfWork;
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository, IUnitOfWork uniOfWork)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository cannot be null!");
            }

            if (uniOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork cannot be null!");
            }

            this.userRepository = userRepository;
            this.uniOfWork = uniOfWork;
        }

        public IUser GetUserInfo(string username)
        {
            var user = this.userRepository.GetByUserName(username);
            return user;
        }

        public void UpdateUserInfo(string username, string firstName, string lastName, string phoneNumber, string info, byte[] image, ICity city)
        {
            var user = this.userRepository.GetByUserName(username);
            user.FirstName = firstName;
            user.LastName = lastName;
            user.PhoneNumber = phoneNumber;
            user.Info = info;
            user.City = city;
            user.Image = image;

            this.userRepository.Update(user);
            this.uniOfWork.Commit();
        }

        public void RemoveFavouriteUser(string username, string userToRemoveUsername)
        {
            var user = this.userRepository.GetByUserName(username);
            var userToRemove = user.FavouriteUsers.Where(x => x.Username == userToRemoveUsername).Single();
            user.FavouriteUsers.Remove(userToRemove);

            this.userRepository.Update(user);
            this.uniOfWork.Commit();
        }

        public void AddFavouriteUser(string username, string userToAddUsername)
        {
            var user = this.userRepository.GetByUserName(username);
            var userToAdd = this.userRepository.GetByUserName(userToAddUsername);
            user.FavouriteUsers.Add(userToAdd);

            this.userRepository.Update(user);
            this.uniOfWork.Commit();
        }

        public void UpdateImage(byte[] image, string username)
        {
            var user = this.userRepository.GetByUserName(username);
            user.Image = image;
            this.userRepository.Update(user);
            this.uniOfWork.Commit();
        }

        public int GetAllUsersCount()
        {
            return this.userRepository.GetAllUsersCount();
        }
    }
}
