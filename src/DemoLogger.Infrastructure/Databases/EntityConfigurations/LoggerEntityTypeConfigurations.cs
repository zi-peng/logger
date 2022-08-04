using Logger.Domain.Aggregates.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoLogger.Infrastructure.Databases.EntityConfigurations;

 class LoggerEntityTypeConfigurations : IEntityTypeConfiguration<LoggerInfo>
{
    public void Configure(EntityTypeBuilder<LoggerInfo> builder)
    {
        builder.HasKey(p => p.Id);
        //ToTable 需要引用 Microsoft.EntityFrameworkCore.Relational 这包
        builder.ToTable("logger");
        builder.Property(p => p.Title).HasMaxLength(20);
        builder.Property(p => p.Content).HasMaxLength(30);
    }
}