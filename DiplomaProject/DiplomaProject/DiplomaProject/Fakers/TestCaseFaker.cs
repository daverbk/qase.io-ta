using Bogus;
using DiplomaProject.Models;

namespace DiplomaProject.Fakers;

public class TestCaseFaker : Faker<TestCase>
{
    public TestCaseFaker()
    {
        RuleFor(project => project.Title, faker => faker.Commerce.ProductName());
    }
}
