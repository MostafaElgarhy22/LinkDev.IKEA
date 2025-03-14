namespace LinkDev.IKEA.BLL.Models.Departments
{
    public record CreateDepartmentDto(string Code, string Name, string? Description, DateOnly CreationDate);
}
