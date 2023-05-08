using System.Net;
using API.Controllers;
using API.Models;
using APITests.Common;
using DAL.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace APITests;

public class UserControllerTests : TestControllerBase
{
    private readonly UsersController usersController;

    public UserControllerTests()
    {
        usersController = new UsersController(new UserRepository(Context), Mapper);
    }

    [Fact]
    public async Task Create_ShouldSuccess()
    {
        var user = new UserCreateDto
        {
            Login = "NewUser",
            Password = "Password",
            UserGroupId = 1
        };

        var result = await usersController.Create(user);

        Assert.NotNull(await Context.Users.FirstOrDefaultAsync(x =>
            x.Login == "NewUser" && x.Password == "Password" && x.UserGroupId == 1));
        Assert.Equal(HttpStatusCode.OK, GetHttpStatusCode(result));
    }

    [Fact]
    public async Task Create_Should_Return_BadRequest_When_LoginIsExist()
    {
        var user = new UserCreateDto
        {
            Login = "Admin",
            Password = "Password",
            UserGroupId = 1
        };

        var result = await usersController.Create(user);

        Assert.Equal(HttpStatusCode.BadRequest, GetHttpStatusCode(result));
    }

    [Fact]
    public async Task Create_Should_Return_BadRequest_When_AdminIsExist()
    {
        var user = new UserCreateDto
        {
            Login = "NewUser",
            Password = "Password",
            UserGroupId = 2
        };

        var result = await usersController.Create(user);

        Assert.Equal(HttpStatusCode.BadRequest, GetHttpStatusCode(result));
    }

    [Fact]
    public async Task Users_ShouldSuccess()
    {
        var expected = Context.Users
            .Include(x => x.UserGroup)
            .Include(x => x.UserState)
            .ToList();

        var result = await usersController.Users();

        Assert.Equal(HttpStatusCode.OK, GetHttpStatusCode(result));
        Assert.Equal(expected, (result as OkObjectResult).Value as List<User>);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 1)]
    [InlineData(3, 4)]
    public async Task UsersWithParameters_ShouldSuccess(int pageNumber, int pageSize)
    {
        var parameters = new PaginationParameters { PageNumber = pageNumber, PageSize = pageSize };
        var expected = Context.Users
            .Include(x => x.UserGroup).Include(x => x.UserState)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToList();

        var result = await usersController.Users(parameters);

        Assert.Equal(HttpStatusCode.OK, GetHttpStatusCode(result));
        Assert.Equal(expected, (result as OkObjectResult).Value as List<User>);
    }

    [Fact]
    public async Task GetById_ShouldSuccess()
    {
        var id = ApplicationDbContextFactory.AdminId;

        var result = await usersController.GetUser(id);

        Assert.Equal(HttpStatusCode.OK, GetHttpStatusCode(result));
        Assert.Equal(Context.Users.FirstOrDefault(x => x.Id == id), (result as OkObjectResult).Value as User);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        var id = Guid.NewGuid();

        var result = await usersController.GetUser(id);

        Assert.Equal(HttpStatusCode.NotFound, GetHttpStatusCode(result));
    }

    [Fact]
    public async Task Delete_ShouldSuccess()
    {
        var id = ApplicationDbContextFactory.UserIdForDelete;

        var result = await usersController.Delete(id);

        Assert.Equal(HttpStatusCode.OK, GetHttpStatusCode(result));
        Assert.Equal(Context.Users.FirstOrDefault(x => x.Id == id)!.UserStateId, 2);
    }

    [Fact]
    public async Task Delete_ShouldReturn_NotFound_When_UserDoesNotExist()
    {
        var id = Guid.NewGuid();

        var result = await usersController.Delete(id);

        Assert.Equal(HttpStatusCode.NotFound, GetHttpStatusCode(result));
    }

    private HttpStatusCode GetHttpStatusCode(IActionResult functionResult)
    {
        try
        {
            return (HttpStatusCode)functionResult
                .GetType()
                .GetProperty("StatusCode")
                .GetValue(functionResult, null);
        }
        catch
        {
            return HttpStatusCode.InternalServerError;
        }
    }
}