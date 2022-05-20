using Bogus;
using DiplomaProject.Models;

namespace DiplomaProject.Fakers;

public class ProjectFaker : Faker<Project>
{
    public ProjectFaker()
    {
        RuleFor(project => project.Title, faker => faker.Commerce.ProductName());
        RuleFor(project => project.Description, faker => faker.Company.CatchPhrase());
        RuleFor(project => project.Code, faker => faker.Person.FirstName);
        RuleFor(project => project.Access, "all");
    }
}
