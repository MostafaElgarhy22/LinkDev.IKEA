namespace LinkDev.IKEA.BLL.Models.Departments
{
    public record CreateDepartmentDto(string Name, string Code, string? Description, DateOnly CreationDate);
}
