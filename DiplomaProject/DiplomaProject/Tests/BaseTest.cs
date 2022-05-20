using Bogus;
using DiplomaProject.Fakers;
using DiplomaProject.Models;

namespace DiplomaProject.Tests;

public class BaseTest
{
    protected static Faker<Project> FakeProject => new ProjectFaker();
}
