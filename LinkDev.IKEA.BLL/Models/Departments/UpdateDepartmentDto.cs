namespace LinkDev.IKEA.BLL.Models.Departments
{
    public record UpdateDepartmentDto(int Id, string Code, string Name, string? Description, DateOnly CreationDate);
}
