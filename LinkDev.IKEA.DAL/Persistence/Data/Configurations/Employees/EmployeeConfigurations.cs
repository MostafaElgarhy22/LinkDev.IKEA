using LinkDev.IKEA.DAL.Common.Enumss;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistence.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.IKEA.DAL.Persistence.Data.Configurations.Employees
{
    internal class EmployeeConfigurations : BaseAuditableEntityConfiguration<int, Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);

            builder.Property(E => E.Id).UseIdentityColumn(1, 1);
            builder.Property(E => E.FirstName).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.LastName).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Email).HasColumnType("varchar(100)");

            builder.Property(E => E.Gender)
                .HasConversion(

                (gender) => gender.ToString(),
                (gender) => Enum.Parse<Gender>(gender)
                );

            builder.Property(E => E.EmployeeType)
                .HasConversion(
                (employeeType) => employeeType.ToString(),
                (employeeType) => Enum.Parse<EmployeeType>(employeeType)
                );

            builder.HasOne(E => E.Department)
                .WithMany()
                .HasForeignKey(E => E.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);



        }
    }
}
