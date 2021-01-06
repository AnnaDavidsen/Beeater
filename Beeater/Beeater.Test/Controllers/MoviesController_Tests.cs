using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Beeater.Api.Controllers;
using Beeater.Contracts;
using Beeater.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Beeater.Test.Controllers
{
    public class MoviesController_Tests
    {
        private readonly Mock<IRepositoryWrapper> _repositoryMock = new Mock<IRepositoryWrapper>();
        private MoviesController _sut;
        public MoviesController_Tests()
        {            
            _repositoryMock.Setup(x => x.Movies.GetMovieByTitle(It.IsAny<string>()))
                .ReturnsAsync((string s) => 
                { 
                    return data.Where(x => x.Title == s).FirstOrDefault(); 
                });

            _repositoryMock.Setup(x => x.Movies.GetMovieDetailed(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    return data.FirstOrDefault(x => x.Id == id);
                });

            _sut = new MoviesController(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetMovieByTitleReturnsOk()
        {
            var result = await _sut.GetMovieByTitle("test 1");
            var response = result.Result as OkObjectResult;

            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task GetMovieByTitleReturnsNotFoundIfMovieDoesNotExist()
        {
            var result = await _sut.GetMovieByTitle("does not exist");
            var response = result.Result as NotFoundResult;

            Assert.NotNull(response);
            Assert.Equal(404, response.StatusCode);
        }
        
        [Fact]
        public async Task GetMovieByTitleReturnsMovie()
        {
            var result = await _sut.GetMovieByTitle("test 2");
            var response = result.Result as OkObjectResult;
            var value = response.Value as Movie;

            Assert.Equal("test 2", value.Title);
            Assert.Equal(2, value.Id);
        }

        [Fact]
        public async Task GetMovieByIdDetailedReturnsOk()
        {
            var result = await _sut.GetMovieByIdDetailed(1);
            var response = result.Result as OkObjectResult;

            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task GetMovieByIdDetailedReturnsNotFoundIfMovieDoesNotExist()
        {
            var result = await _sut.GetMovieByIdDetailed(25);
            var response = result.Result as NotFoundResult;

            Assert.NotNull(response);
            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async Task GetMovieByIdDetailedReturnsMovie()
        {
            var result = await _sut.GetMovieByIdDetailed(3);
            var response = result.Result as OkObjectResult;
            var value = response.Value as Movie;

            Assert.Equal(3, value.Id);
        }

        private IQueryable<Movie> data = new List<Movie>()
        {
            new Movie()
            {
                Id = 1,
                Title = "test 1",
            },
            new Movie()
            {
                Id = 2,
                Title = "test 2",
            },
            new Movie()
            {
                Id = 3,
                Title = "test 3",
            }
        }.AsQueryable();
    }
}

